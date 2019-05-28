using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIblast : MonoBehaviour
{
    public float Speed;
    public float Damage = 10;
    public GameObject ExplosionFXPrefab;
    public Transform ExplosionTransfrom;
    public float TimeAlive;

    private void Start()
    {
        //Invoke("DestroyGameObject", TimeAlive);
        Destroy(gameObject, TimeAlive);
        ObjectsInGame.SoundManager.PlayPlayerAttackAudioClip();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(Damage);
            DestroyGameObject();
        }
        else if (collision.tag == "Ground")
        {
            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
        Instantiate(ExplosionFXPrefab, ExplosionTransfrom.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
