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

    //private even to editor
    private Animator playerAnimator;
    private Rigidbody2D rb;
    private bool bCanMove;
    private bool bCanJump;
    private bool bCanDoubleJump;
    private bool bFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        bCanMove = true;
        bCanJump = true;
        bCanDoubleJump = true;
        bFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //determines if player can move or not, set by NPC dialogue during interactions
        if(bCanMove)
        {
            //should really be doing input checks in update and physics in fixedupdate but it's ok for now :)
            //also should be using AddForce but that was giving weird physics bugs and wasn't working for every situation
            //HorizontalMovement();
            CheckForJump();
        }

        if(rb.velocity.x < 0 && bFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            bFacingRight = false;
        }
        else if(rb.velocity.x > 0 && !bFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            bFacingRight = true;
        }
        
        if(rb.velocity.x != 0)
        {
            playerAnimator.Play("running");
        }
        else
        {
            playerAnimator.Play("idle");
        }


        if(bCanJump)
        {
            playerAnimator.StopPlayback();
        }
        else
        {
            playerAnimator.StartPlayback();
        }

    }

    private void FixedUpdate()
    {
        if(bCanMove)
        {
            HorizontalMovement();
        }
    }

    //determines the horizontal movement direction based on input
    private void HorizontalMovement()
    {
        //gets input on X axis, moves player in that direction by a certain movespeed while axis is held
        float xMovement = Input.GetAxisRaw("Horizontal");
        Vector2 movementVector = new Vector2(xMovement * moveSpeed, rb.velocity.y);
        rb.velocity = movementVector;
    }

    //checks if the player wants to jump, and if the character is able to do so
    private void CheckForJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (bCanJump || bCanDoubleJump))
        {
            Debug.Log("Jumping!");
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVelocity;
            //determines the booleans for if this was a single or double
            //on first jump, double jump is set to true before jump is set to false
            //on double jump, can jump is false, so they both get set to false until reset
            bCanDoubleJump = bCanJump;
            bCanJump = false;
        }
    }

    //character can jump again after coming into contact with something
    void OnCollisionEnter2D(Collision2D other)
    {
        bCanJump = true;
        bCanDoubleJump = true;
    }

    //enables or disables movement, called in NPC dialogue during interactions
    public void EnableMovement()
    {
        bCanMove = true;
    }

    public void DisableMovement()
    {
        bCanMove = false;
    }
}
