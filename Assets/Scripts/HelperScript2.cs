using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript2 : MonoBehaviour
{
    //[SerializeField] GameObject ImageGO;
    Animator animator;
    public float WaitingTime = 3f;

    bool playerInRange = false;
    float count;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //ImageGO.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                animator.SetBool("PlayerInRange", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            count = WaitingTime;
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
