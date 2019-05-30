using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float Damage = 10;

    bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
            bool isFromTheRightSide;

            if (ObjectsInGame.PlayerHealth.transform.position.x < transform.position.x)
                isFromTheRightSide = true;
            else
                isFromTheRightSide = false;

            ObjectsInGame.PlayerHealth.CheckTakeDamage(Damage, isFromTheRightSide);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;

            bool isFromTheRightSide;

            if (ObjectsInGame.PlayerHealth.transform.position.x < transform.position.x)
                isFromTheRightSide = true;
            else
                isFromTheRightSide = false;

            ObjectsInGame.PlayerHealth.CheckTakeDamage(Damage, isFromTheRightSide);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
