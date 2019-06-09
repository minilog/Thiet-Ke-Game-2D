using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    bool playerInRange = false;
    public enum PickupType
    {
        Key,
        Health
    }
    public PickupType Type;

    private void Start()
    {
        if (Type == PickupType.Key)
        {
            if (ObjectsInGame.Key == false)
                gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Type == PickupType.Key)
            {
                ObjectsInGame.CanvasController.HaveKey = true;
                ObjectsInGame.Key = false;
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                ObjectsInGame.PlayerHealth.Health = ObjectsInGame.PlayerHealth.MaxHealth;
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
