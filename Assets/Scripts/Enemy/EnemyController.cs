using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public float AttackDamage;
    public float PushForce = 10;

    public float MaxHealth = 10000;
    float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthSlider.value = _health;
        }
    }
    [SerializeField] Slider healthSlider;

    private void OnValidate()
    {
        if (playerInteraction == null)
            playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerInteraction == null)
            playerInteraction = FindObjectOfType<PlayerInteraction>();

        healthSlider.maxValue = MaxHealth;
        Health = MaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            DestroyGameObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float pushForceX = 0;

            if (playerInteraction.transform.position.x < transform.position.x)
                pushForceX = -1;
            else
                pushForceX = 1;

            pushForceX *= 10f;
            playerInteraction.CheckTakeDamage(AttackDamage, pushForceX);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            float pushForceX = 0;
            if (playerInteraction.transform.position.x < transform.position.x)
                pushForceX = -1;
            else
                pushForceX = 1;

            pushForceX *= PushForce;
            playerInteraction.CheckTakeDamage(AttackDamage, pushForceX);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
