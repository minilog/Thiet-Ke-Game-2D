using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public float AttackDamage;

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
    }

    // Update is called once per frame
    void Update()
    {

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

            pushForceX *= 10f;
            playerInteraction.CheckTakeDamage(AttackDamage, pushForceX);
        }
    }
}
