using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActiveZone : MonoBehaviour
{
    [SerializeField] GameObject inactiveEnemy;

    private void Start()
    {
        inactiveEnemy.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inactiveEnemy.SetActive(true);
        }
    }
}
