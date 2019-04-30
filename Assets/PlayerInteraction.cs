using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float Health = 100;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;

    public float TimeBtwTakeDamages;
    float canNotTakeDamageTime = 0;
    public float pushForceXWhenTakeDamage;

    private void OnValidate()
    {
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update -> Animation -> TriggerEnter

        if (canNotTakeDamageTime > 0)
        {
            canNotTakeDamageTime -= Time.deltaTime;
        }

        animator.SetFloat("CanNotTakeDamage", canNotTakeDamageTime);

        if (Health <= 0)
            Destroy(gameObject);
    }

    public void CheckTakeDamage(float damage, float pushForeX)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            canNotTakeDamageTime <= 0)
        {
            pushForceXWhenTakeDamage = pushForeX;
            animator.SetTrigger("TakeDamage");
            canNotTakeDamageTime = TimeBtwTakeDamages;
            Health -= damage;
        }
    }

    public bool CanTakeDamage()
    {
        return (canNotTakeDamageTime <= 0);
    }
}
