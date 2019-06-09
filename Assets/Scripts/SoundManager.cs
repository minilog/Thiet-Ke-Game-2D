using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //[SerializeField] GameObject theCamera;

    [Space]
    [SerializeField] AudioSource backgroundAS;
    [Range(0, 1)]
    [SerializeField] float backgroundVolume;

    [Space]
    [SerializeField] AudioSource soundEffectAS;
    [Range(0, 1)]
    [SerializeField] float soundEffectVolume;

    // AUDIO CLIP
    [Space]
    public AudioClip PlayerJumpAudioClip;
    [Range(0, 1)]
    public float PlayerJumpVolume;

    public AudioClip PlayerAttackAudioClip;
    [Range(0, 1)]
    public float PlayerAttackVolume;

    public AudioClip ExplosionAudioClip;
    [Range(0, 1)]
    public float ExplosionVolume;

    private void Awake()
    {
        ObjectsInGame.SoundManager = this;
    }

    private void Start()
    {
        backgroundAS.volume = backgroundVolume;
        soundEffectAS.volume = soundEffectVolume;
    }

    private void LateUpdate()
    {
        //transform.position = theCamera.transform.position;
    }

    public void PlayPlayerJumpAudioClip()
    {
        soundEffectAS.PlayOneShot(PlayerJumpAudioClip, PlayerJumpVolume);
    }

    public void PlayPlayerAttackAudioClip()
    {
        soundEffectAS.PlayOneShot(PlayerAttackAudioClip, PlayerAttackVolume);
    }

    public void PlayExplostionAudioClip()
    {
        soundEffectAS.PlayOneShot(ExplosionAudioClip, ExplosionVolume);
    }

    public AudioClip CoinClip;
    [Range(0, 1)]
    public float CoinVolume;
    public void PlayCoinCLip()
    {
        soundEffectAS.PlayOneShot(CoinClip, CoinVolume);
    }

    public AudioClip GameOverClip;
    [Range(0, 1)]
    public float GameOverVolume;
    public void PlayGameOverCLip()
    {
        soundEffectAS.PlayOneShot(GameOverClip, GameOverVolume);
    }
}
