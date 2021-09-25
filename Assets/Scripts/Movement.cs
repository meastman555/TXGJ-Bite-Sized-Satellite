using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //variables exposed to editor
    //default values are from testing with default scene, square sprite size, and camera size
    //will likely be overwritten in editor, but these are a decent fallback
    //these also were testing using gravity scale of 2 but mass could also be tested
    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float maxSpeed = 20.0f;

    [SerializeField]
    private float jumpForce = 10.0f;

    [SerializeField]
    private float dashForce = 15.0f;

    [SerializeField]
    private float dashCooldown = 2.0f;

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
        float xMovement = Input.GetAxis("Horizontal");
        //if we aren't pressing the button stop character movement by adding force in opposite x direction
        if (xMovement == 0)
        {
            Vector2 stopVector = new Vector2(-rb.velocity.x, rb.velocity.y);
            rb.velocity = stopVector;
        }
        //caps the character at a max speed
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 movementVector = new Vector2(xMovement * moveSpeed, rb.velocity.y);
            rb.velocity = movementVector;
        }
    }

    //checks if the player wants to jump, and if the character is able to do so
    void CheckForJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && bCanJump)
        {
            Debug.Log("Jumping!");
            float jumpX = (rb.velocity.x == 0) ? 0.5f : rb.velocity.x;
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVelocity;
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
        Vector2 dashVelocity = rb.velocity.normalized * dashForce;
        //AddForce doesn't seem to work here, unsure why
        rb.velocity = dashVelocity;
        //rb.velocity = dashVelocity;
        bCanDash = false;
        yield return new WaitForSeconds(dashCooldown);
        bCanDash = true;
    }
}


