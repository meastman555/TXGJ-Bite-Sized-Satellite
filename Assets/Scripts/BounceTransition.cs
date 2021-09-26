using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTransition : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float bounceForce = 50.0f;

    //private even to editor
    private Movement playerMovement;
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<Movement>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //every frame we check if the player has begun their descent
        //once they have player movement is immediately allowed
        if(playerRB.velocity.y < 0.0f)
        {
            playerMovement.EnableMovement();
        }
    }

    //player comes into contact with trigger, not collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //disable player movement
            playerMovement.DisableMovement();
            //launch player upwards
            Vector2 bounceVelocity = new Vector2(0, bounceForce);
            playerRB.velocity = bounceVelocity;
        }
    }
}
