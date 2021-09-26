using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    //variables exposed to the editor
    [SerializeField]
    private float fadeDurationSeconds = 5.0f;

    //private even to editor
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        //we have one audio source for each music track
        audioSources = GetComponents<AudioSource>();
    }

    //fads in the audio clip specified by track num over a time frame
    public void FadeInTrack(int trackNum)
    {
        StartCoroutine(DoFadeIn(trackNum));
    }

    //coroutine to allow for smooth fading
    private IEnumerator DoFadeIn(int trackNum)
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
