using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private GameObject interactBox;

    [SerializeField]
    private Text dialogueBox;

    [SerializeField]
    private string[] dialogueText;

    [SerializeField]
    private KeyCode interactKey;

    [SerializeField]
    private KeyCode advanceKey;

    [SerializeField]
    private GameObject player;

    //private even to editor
    private Queue<string> internalDialogueText;
    private Movement playerMovement;
    private bool bCanTalk;
    private bool bIsTalking;


    // Start is called before the first frame update
    void Start()
    {
        internalDialogueText = new Queue<string>(dialogueText);
        playerMovement = player.GetComponent<Movement>();
        bCanTalk = false;
        bIsTalking = false;
        interactBox.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        //starts dialogue when conditions are good
        if(bCanTalk && Input.GetKeyDown(interactKey) && !bIsTalking)
        {
            StartCoroutine("DoDialogue");
        }
    }

    //player in range of NPC
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player in NPC trigger");
            bCanTalk = true;
            interactBox.SetActive(true);
        }
    }

    //player out of range of NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player out of NPC trigger");
            bCanTalk = false;
            interactBox.SetActive(false);
        }
    }

    //starts the NPC's dialogue
    private IEnumerator DoDialogue()
    {
        Debug.Log("Starting Dialogue");
        bIsTalking = true;
        //disables player movement during dialogue duration
        playerMovement.DisableMovement();

        //handles each dialogue string and waits for user input before moving on
        do
        {
            string s = internalDialogueText.Dequeue();
            Debug.Log("Displaying line: " + s);
            dialogueBox.text = s;
            yield return StartCoroutine("WaitForUserInput");
        } while(internalDialogueText.Count != 0);

        //reenables player movement
        playerMovement.EnableMovement();
        //resets the queue in case we talk to this NPC again
        internalDialogueText = new Queue<string>(dialogueText);
        bIsTalking = false;
        Debug.Log("Finishing Dialogue");
    }

    //separate coroutine to infinitely loop until user presses space
    //coroutine so overall unity is not blocked
    private IEnumerator WaitForUserInput()
    {
        //unity forum suggestion for ensuring any lines of dialogue aren't skipped
        do
        {
            yield return null;
        } while(!Input.GetKeyDown(advanceKey));
    }
}
