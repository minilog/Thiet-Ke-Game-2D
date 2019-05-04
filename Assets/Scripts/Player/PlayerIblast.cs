﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIblast : MonoBehaviour
{
    public float Speed;
    public float Damage = 10;
    public GameObject ExplosionFXPrefab;
    public Transform ExplosionTransfrom;
    public float TimeAlive;
    public AudioClip FireAudioCip;
    public float Volume = 1;


    private void Start()
    {
        //Invoke("DestroyGameObject", TimeAlive);
        Destroy(gameObject, TimeAlive);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(FireAudioCip, Volume);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyInteraction"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(Damage);
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
        Instantiate(ExplosionFXPrefab, ExplosionTransfrom.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
