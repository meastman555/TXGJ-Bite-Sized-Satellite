using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    //variables exposed to the editor
    [SerializeField]
    private int clipNumberToFadeIn;

    [SerializeField]
    private AudioFade audioFade;

    //private even to editor
    private bool bFading;

    void Start()
    {
        bFading = false;
    }

    //checks when the player collides with this trigger
    //uses the audio fade manager to handle the rest
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !bFading)
        {
            bFading = true;
            audioFade.FadeInTrack(clipNumberToFadeIn);
        }
    }
}
