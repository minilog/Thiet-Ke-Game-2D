using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator animator;

    public float StartTime = 0f;
    public float waitingToUp = 1f;
    public float waitingToDown = 1f;

    float count;
    private void Start()
    {
        animator = GetComponent<Animator>();

        count = waitingToUp - count;
    }

    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spike Hold Down"))
            {
                animator.SetTrigger("Up");
                count = waitingToDown;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spike Hold Up"))
            {
                animator.SetTrigger("Down");
                count = waitingToUp;
            }
        }
    }
}
