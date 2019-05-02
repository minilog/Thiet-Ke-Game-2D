using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip audioClip;
    public float Volume = 1f;

    void Start()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, Volume);
    }
}
