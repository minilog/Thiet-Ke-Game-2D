using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float Damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bool isFromTheRightSide;

            if (ObjectsInGame.PlayerHealth.transform.position.x < transform.position.x)
                isFromTheRightSide = true;
            else
                isFromTheRightSide = false;

            ObjectsInGame.PlayerHealth.CheckTakeDamage(Damage, isFromTheRightSide);
        }
    }
}
