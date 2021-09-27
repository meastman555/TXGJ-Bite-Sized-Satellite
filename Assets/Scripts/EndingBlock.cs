using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBlock : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject guide;

    //forced dialogue if you try to move past sage without all the letters
    //private even to editor
    private BoxCollider2D bc;
    private NPCDialogue dialogue;
    private bool bAllChildrenActive;
    private bool bCalledDialogue;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        dialogue = guide.GetComponent<NPCDialogue>();
        //sets this NPC to have forced dialogue, removing player interactions
        dialogue.SetAsForcedDialogue();
        //we assume we have all the children on collision -- if we are missing one it is immediately set to false
        bAllChildrenActive = true;
    }


    //checked each collision to determine if the player has met exit conditions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //loops through all the children
            //even if camera is childed, that will not affect the logic because it will never be inactive
            for(int i = 0; i < player.transform.childCount; i++)
            {
                Transform child = player.transform.GetChild(i);
                if(!child.gameObject.activeInHierarchy && !bCalledDialogue)
                {
                    bAllChildrenActive = false;
                    bCalledDialogue = true;
                    //inactive child, start dialogue
                    dialogue.StartDialogue();
                }
            }
            //every child is active, set this to a trigger so the player can walk past
            if(bAllChildrenActive)
            {
                bc.isTrigger = true;
            }
        }
    }

    //resets all children active when we exit the collision so we can start fresh on next collision
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bAllChildrenActive = true;
            bCalledDialogue = false;
        }
    }
}
