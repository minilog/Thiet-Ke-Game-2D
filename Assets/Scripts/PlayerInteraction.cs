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
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
        if (rb2D == null)
            rb2D = gameObject.GetComponent<Rigidbody2D>();
        if (healthSlider == null)
        {
            healthSlider.maxValue = Health;
            healthSlider.value = Health;
        }
        if (playerController == null)
            playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
        if (rb2D == null)
            rb2D = gameObject.GetComponent<Rigidbody2D>();
        if (healthSlider == null)
        {
            healthSlider.maxValue = Health;
            healthSlider.value = Health;
        }
        if (playerController == null)
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
            // Change to animation Hurt
            animator.SetFloat("CanNotTakeDamage", canNotTakeDamageTime);
    }

    public void CheckTakeDamage(float damage, float pushForeX)
    {
        if (CanTakeDamage())
        {
            // Push Force use for Player Hurt Behavior
            pushForceXWhenTakeDamage = pushForeX;
            // set Animation Player Hurt
            animator.SetTrigger("TakeDamage");
            // Set Time for animation Player Flicker
            canNotTakeDamageTime = TimeBtwTakeDamages;
            // Lost Health
            Health -= damage;
            healthSlider.value = Health;
        }
    }

    public bool CanTakeDamage()
    {
        return (canNotTakeDamageTime <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"));
    }
}
