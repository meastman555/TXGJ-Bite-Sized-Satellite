using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequence : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private float transitionTime = 3.0f;

    //private even to editor
    private Movement playerMovement;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Movement>();
        playerAnimator = GetComponent<Animator>();
    }

    //checks if we have collied with the period to end the game
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Period") 
        {
            StartCoroutine("End");
        } 
    }

    //plays the final animation, and waits for that to end before returning to the main menu
    private IEnumerator End()
    {
        playerMovement.DisableMovement();
        playerAnimator.Play("sleep");
        yield return new WaitForSeconds(transitionTime);
        //main menu is build index 0
        SceneManager.LoadScene(0);
    }
}
