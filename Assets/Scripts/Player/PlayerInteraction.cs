using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthSlider.value = _health;
        }
    }

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerController playerController;

    public float TimeBtwTakeDamages;
    float canNotTakeDamageTime = 0;
    public float pushForceXWhenTakeDamage;

    private void Start()
    {
        healthSlider.maxValue = 100;
        Health = 100;
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
        }
    }

    public bool CanTakeDamage()
    {
        return (canNotTakeDamageTime <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"));
    }
}
