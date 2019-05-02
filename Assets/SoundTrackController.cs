using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public AudioClip[] audioClips;
    AudioClip currentAudioClip;
    float CurrentAudioClipCount;

    void Update()
    {
        CurrentAudioClipCount -= Time.deltaTime;
        if (CurrentAudioClipCount <= 0)
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
        CurrentAudioClipCount = currentAudioClip.length + 1;
    }
}
