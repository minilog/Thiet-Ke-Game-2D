using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip NormalMusic;
    [SerializeField] AudioClip BossMusic;
    float count = 0;

    private void Start()
    {
        //audioSource.PlayOneShot(NormalMusic);
    }

    void Update()
    {
    }

    void PlayerAudioClipRandom()
    {
        //int rand = Random.Range(0, audioClips.Length);

        //while (currentAudioClip == audioClips[rand])
        //{
        //    rand = Random.Range(0, audioClips.Length);
        //}
        //currentAudioClip = audioClips[rand];

        //audioSource.PlayOneShot(currentAudioClip);
        //currentAudioClipCounter = currentAudioClip.length;
    }

    public void ChangeMusic()
    {
        if (audioSource.clip == NormalMusic)
        {
            audioSource.clip = BossMusic;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = NormalMusic;
            audioSource.Play();
        }
    }
}
