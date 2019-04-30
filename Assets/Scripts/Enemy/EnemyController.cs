using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public float AttackDamage;
    bool playerOnTriggerStay = false;

    private void OnValidate()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnTriggerStay && playerInteraction.CanTakeDamage())
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerOnTriggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerOnTriggerStay = false;
        }
    }
}
