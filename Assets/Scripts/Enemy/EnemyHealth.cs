using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    public float MaxHealth;
 
    private float _health;
    public float Health
    {
        set
        {
            _health = value;
            healthSlider.value = _health;
        }
        get
        {
            return _health;
        }
    }

    private void Start()
    {
        healthSlider.maxValue = MaxHealth;
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
           transform.parent.gameObject.SetActive(false);
    }
}
