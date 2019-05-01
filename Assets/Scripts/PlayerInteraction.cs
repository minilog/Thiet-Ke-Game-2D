using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float Health = 100;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerController playerController;

    public float TimeBtwTakeDamages;
    float canNotTakeDamageTime = 0;
    public float pushForceXWhenTakeDamage;

    private void OnValidate()
    {
        animator = gameObject.GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        healthSlider.maxValue = Health;
        healthSlider.value = Health;
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Update -> Animation -> TriggerEnter

        if (canNotTakeDamageTime > 0)
        {
            canNotTakeDamageTime -= Time.deltaTime;
        }

        if (Health <= 0)
            // At the end this Behavior, destroy gameObject
            animator.Play("Player Die");
        else
            animator.SetFloat("CanNotTakeDamage", canNotTakeDamageTime);
    }

    public void CheckTakeDamage(float damage, float pushForeX)
    {
        if (CanTakeDamage())
        {
            pushForceXWhenTakeDamage = pushForeX;
            animator.SetTrigger("TakeDamage");
            canNotTakeDamageTime = TimeBtwTakeDamages;
            Health -= damage;
            healthSlider.value = Health;
        }
    }

    public bool CanTakeDamage()
    {
        return (canNotTakeDamageTime <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike"));
            //&&
            //playerController.DashStateCount < 0);
    }
}
