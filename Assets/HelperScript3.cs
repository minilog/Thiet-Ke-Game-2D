using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript3 : MonoBehaviour
{
    //[SerializeField] GameObject ImageGO;
    Animator animator;
    public KeyCode OpenKeyCode = KeyCode.W;

    bool playerInRange = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //ImageGO.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(OpenKeyCode) && !ObjectsInGame.CanvasController.HaveKey)
            {
                animator.SetBool("PlayerInRange", true);
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

            animator.Play("New State");
            animator.SetBool("PlayerInRange", false);
        }
    }
}
