using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //variables exposed to editor
    //default values are from testing with default scene, square sprite size, and camera size
    //will likely be overwritten in editor, but these are a decent fallback
    //this was tested with mass of 1 and gravity scale of 4
    [SerializeField]
    private float moveSpeed = 8.0f;

    [SerializeField]
    private float jumpForce = 20.0f;

    [SerializeField]
    private float dashMultiplier = 1.5f;

    [SerializeField]
    private float dashCooldown = 2.0f;

    [SerializeField]
    private float dashDuration = 0.25f;

    //private even to editor
    private Rigidbody2D rb;
    private bool bCanJump;
    private bool bCanDash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bCanJump = true;
        bCanDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //should really be doing input checks in update and physics in fixedupdate but it's ok for now :)
        //also should be using AddForce but that was giving weird physics bugs and wasn't working for every situation
        HorizontalMovement();
        CheckForJump();
        CheckForDash();
    }

    //determines the horizontal movement direction based on input
    void HorizontalMovement()
    {
        //gets input on X axis, moves player in that direction by a certain movespeed while axis is held
        float xMovement = Input.GetAxisRaw("Horizontal");
        Vector2 movementVector = new Vector2(xMovement * moveSpeed, rb.velocity.y);
        rb.velocity = movementVector;
    }

    //checks if the player wants to jump, and if the character is able to do so
    void CheckForJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && bCanJump)
        {
            Debug.Log("Jumping!");
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVelocity;
            //makes the character fall to ground faster by increasing gravity
            bCanJump = false;
        }
    }

    //character can jump again after coming into contact with something
    void OnCollisionEnter2D(Collision2D other)
    {
        bCanJump = true;
    }

    //checks if the player wants to dash, and handles correct direction
    void CheckForDash()
    {
        if(Input.GetKeyDown(KeyCode.RightShift) && bCanDash)
        {
            StartCoroutine("PerformDash");
        }
    }

    //handles the actual dash action, and waits a specified amount of time before allowing the player to dash again 
    IEnumerator PerformDash()
    {
        Debug.Log("Dashing!");
        Vector2 dashDirection = new Vector2(0, 0);
        //disable gravity for the duration of the dash
        float oldGravityScale = rb.gravityScale;
        rb.gravityScale = 0.0f;
        //determine the base direction of the dash based on player input
        if(Input.GetKey(KeyCode.W))
        {
            dashDirection = new Vector2(0, 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            dashDirection = new Vector2(0, -1);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        else if(Input.GetKey(KeyCode.A))
        {
            dashDirection = new Vector2(-1, 0);
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        else if(Input.GetKey(KeyCode.D))
        {
            dashDirection = new Vector2(1, 0);
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
        Vector2 dashVelocity = dashDirection * (dashMultiplier * moveSpeed);
        //AddForce doesn't seem to work here, unsure why
        rb.velocity += dashVelocity;
        bCanDash = false;
        //reenable gravity
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = oldGravityScale;
        yield return new WaitForSeconds(dashCooldown);
        bCanDash = true;
    }
}


