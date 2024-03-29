using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private GameObject interactBox;

    [SerializeField]
    private TextMeshProUGUI dialogueBox;

    [SerializeField]
    private string[] dialogueText;

    [SerializeField]
    private KeyCode interactKey;

    [SerializeField]
    private KeyCode advanceKey;

    [SerializeField]
    private GameObject letterToGive;

    [SerializeField]
    private GameObject player;

    //private even to editor
    private Queue<string> internalDialogueText;
    private Movement playerMovement;
    private AudioSource npcAudio;
    private bool bCanTalk;
    private bool bIsTalking;
    private bool bIsForcedDialogue;

    // Start is called before the first frame update
    void Start()
    {
        internalDialogueText = new Queue<string>(dialogueText);
        Debug.Log("Number of lines of dialogue in Start: " + internalDialogueText.Count);
        playerMovement = player.GetComponent<Movement>();
        npcAudio = GetComponent<AudioSource>();
        bCanTalk = false;
        bIsTalking = false;
        bIsForcedDialogue = false;
        interactBox.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        //starts dialogue when conditions are good, prevent restart while already talking
        //skips interaction completely in triggers if this is forced dialogue
        if(bCanTalk && Input.GetKeyDown(interactKey) && !bIsTalking)
        {
            Debug.Log("Calling DoDialogueCoroutine from NPCDialogue");
            StartCoroutine("DoDialogue");
        }
    }

    //player in range of NPC
    //not applicable during forced dialogue
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !bIsForcedDialogue)
        {
            bCanTalk = true;
            interactBox.SetActive(true);
        }
    }

    //player out of range of NPC
    //not applicable during forced dialogue
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !bIsForcedDialogue)
        {
            bCanTalk = false;
            interactBox.SetActive(false);
        }
    }

    //wrapper for the coroutine that can be called from another script
    public void StartDialogue()
    {
        Debug.Log("Calling DoDialogue Coroutine from EndingBlock");
        StartCoroutine("DoDialogue");
    }

    //starts the NPC's dialogue
    private IEnumerator DoDialogue()
    {
        Debug.Log("Called DoDialogue Coroutine");
        Debug.Log("Number of lines of dialogue in DoDialogue: " + internalDialogueText.Count);
        //disables player movement during dialogue duration
        playerMovement.DisableMovement();
        //sets the correct variables to be active
        dialogueBox.gameObject.SetActive(true);
        interactBox.SetActive(false);
        bIsTalking = true;
        //handles each dialogue string and waits for user input before moving on
        while(internalDialogueText.Count != 0)
        {
            //plays the attached sound effect each time new text is rendered
            npcAudio.Play();
            string s = internalDialogueText.Dequeue();
            Debug.Log("Displaying text: " + s);
            dialogueBox.text = s;
            yield return StartCoroutine("WaitForUserInput");
        }
        //resets the queue in case we talk to this NPC again
        internalDialogueText = new Queue<string>(dialogueText);
        bIsTalking = false;
        dialogueBox.gameObject.SetActive(false);
        //no letter is given during forced dialogue, uses an empty gameobject
        letterToGive.SetActive(true);
        //reenables player movement
        playerMovement.EnableMovement();
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

    //sets this NPC dialogue to forced, removing player interactivity completely
    public void SetAsForcedDialogue()
    {
        bIsForcedDialogue = true;
    }
}
