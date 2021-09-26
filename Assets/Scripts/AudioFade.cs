using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioFade : MonoBehaviour
{
    //variables exposed to the editor
    [SerializeField]
    private float fadeDurationSeconds;

    //private even to editor
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        //we have one audio source for each music track
        audioSources = GetComponents<AudioSource>();
    }

    //fades out the from region clip, fades in the to region clip
    //smooth transition between tracks when switching zones
    public void FadeInTrack(int trackNum)
    {
        StartCoroutine(DoFade(trackNum));
    }

    //coroutine to allow for smooth fading
    private IEnumerator DoFade(int trackNum)
    {
        AudioSource fadeAudioSource = audioSources[trackNum];
        while(fadeAudioSource.volume < 1.0f)
        {
            //multiple by delta time to do fade in seconds instead of frame ticks
            fadeAudioSource.volume += Time.deltaTime / fadeDurationSeconds;
            yield return null;
        }
    }
}
