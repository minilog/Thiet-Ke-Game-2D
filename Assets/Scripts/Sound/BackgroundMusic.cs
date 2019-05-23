using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    AudioClip currentAudioClip;
    float currentAudioClipCounter;

    void Update()
    {
        // count the music, if it end, play another music
        currentAudioClipCounter -= Time.deltaTime;
        if (currentAudioClipCounter <= 0)
            PlayerAudioClipRandom();
    }

    void PlayerAudioClipRandom()
    {
        int rand = Random.Range(0, audioClips.Length);

        while (currentAudioClip == audioClips[rand])
        {
            rand = Random.Range(0, audioClips.Length);
        }
        currentAudioClip = audioClips[rand];

        audioSource.PlayOneShot(currentAudioClip);
        currentAudioClipCounter = currentAudioClip.length;
    }
}
