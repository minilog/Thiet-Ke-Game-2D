using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarsChaseZone : MonoBehaviour
{
    public bool PlayerInChaseZone { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInChaseZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInChaseZone = false;
        }
    }
}
