using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCollidersFromTrigger : MonoBehaviour
{
    //variables exposed to the editor
    [SerializeField]
    private GameObject[] colliders;

    [SerializeField]
    private bool bDisableGround;

    [SerializeField]
    private GameObject groundColliderToDisable;

    //when the player enteres this trigger, activate all the linked collider game objects
    //this prevents the player from going back and retracing steps
    //also disables the ground collider specified
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach(GameObject g in colliders)
            {
                g.SetActive(true);
            }
            if(bDisableGround)
            {
                groundColliderToDisable.SetActive(false);
            }
        }
    }
}
