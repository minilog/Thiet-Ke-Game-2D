using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIblast : MonoBehaviour
{
    public float Speed;
    public float MinDamage = 8;
    public float MaxDamage = 12;
    public GameObject ExplosionFXPrefab;
    public Transform ExplosionTransfrom;
    public float TimeAlive;

    private void Start()
    {
        //Invoke("DestroyGameObject", TimeAlive);
        Destroy(gameObject, TimeAlive);
        ObjectsInGame.SoundManager.PlayPlayerAttackAudioClip();
        PlayerStamina playerStamina = ObjectsInGame.PlayerController.gameObject.GetComponent<PlayerStamina>();
        playerStamina.Stamina -= playerStamina.FireStamina;
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
            bool RigtToLeft;
            if (collision.transform.position.x > transform.position.x)
                RigtToLeft = false;
            else
                RigtToLeft = true;

            float damage = (int)Random.Range(MinDamage, MaxDamage + 1);
            enemyHealth.TakeDamage(damage, RigtToLeft);
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
