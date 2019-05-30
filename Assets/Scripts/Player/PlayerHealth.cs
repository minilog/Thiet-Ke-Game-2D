using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
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
    [SerializeField] Slider healthSlider;

    public float cantTakeDamageTime;
    float cantTakeDamageCounter = 0;

    public Vector2 HurtDirection { get; private set; }

    private void Awake()
    {
        ObjectsInGame.PlayerHealth = this;
    }

    private void Start()
    {
        healthSlider.maxValue = MaxHealth;
        Health = MaxHealth;
    }

    private void Update()
    {
        // Update -> Animation -> TriggerEnter

        if (cantTakeDamageCounter > 0)
        {
            cantTakeDamageCounter -= Time.deltaTime;
        }

        if (Health <= 0)
            // At the end this Behavior, destroy gameObject
            animator.Play("Player Die");
        else
            // Change to animation Hurt
            animator.SetFloat("CanNotTakeDamage", cantTakeDamageCounter);
    }

    public void CheckTakeDamage(float damage, bool isFromTheRightSide)
    {
        if (CanTakeDamage())
        {
            // set Animation Player Hurt
            animator.SetTrigger("TakeDamage");
            // Set Time for animation Player Flicker
            cantTakeDamageCounter = cantTakeDamageTime;
            // Lost Health
            Health -= damage;

            if (isFromTheRightSide)
                HurtDirection = new Vector2(-10f, 5f);
            else
                HurtDirection = new Vector2(10f, 5f);
        }
    }

    public bool CanTakeDamage()
    {
        return (cantTakeDamageCounter <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash"));
    }

    public void PlayerDieAndRestartLevel()
    {
        gameObject.SetActive(false);
        ObjectsInGame.ChangeSceneManager.ChangeToNextScene(SceneManager.GetActiveScene().name);
    }

    public void CheckSpawnPlayer()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Health = MaxHealth;
        }
    }
}
