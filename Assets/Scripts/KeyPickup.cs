using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyCode PickupKeyCode = KeyCode.S;
    bool playerInRange = false;

    private void Start()
    {
        if (ObjectsInGame.Key == false)
            gameObject.transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(PickupKeyCode))
            {
                ObjectsInGame.CanvasController.HaveKey = true;
                ObjectsInGame.Key = false;

                gameObject.transform.parent.gameObject.SetActive(false);
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
