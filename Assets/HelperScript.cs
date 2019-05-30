using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    [SerializeField] GameObject ImageGO;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //ImageGO.SetActive(false);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //ImageGO.SetActive(true);
            animator.SetBool("PlayerInRange", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //ImageGO.SetActive(false);
            animator.Play("New State");
            animator.SetBool("PlayerInRange", false);
        }
    }
}

