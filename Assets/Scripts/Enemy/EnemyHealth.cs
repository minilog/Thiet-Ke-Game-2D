﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject DeathFX;
    [SerializeField] Object FloatingNumerPrefab;
    [Space]

    public float MaxHealth;
    public bool StartActive = false;
 
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

    private void OnValidate()
    {
        FloatingNumerPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Effect/Enemy Floating Number.prefab", typeof(GameObject));
    }

    private void Start()
    {
        healthSlider.maxValue = MaxHealth;
        Health = MaxHealth;
        healthSlider.gameObject.SetActive(StartActive);
    }

    public void TakeDamage(float damage, bool fromRightSide = true)
    {
        Health -= damage;
        
        GameObject fGO = Instantiate(FloatingNumerPrefab, transform.position, Quaternion.identity) as GameObject;
        FloatingNumber fNum = fGO.GetComponent<FloatingNumber>();
        fNum.RightDirection = fromRightSide;


        if (Health <= 0)
        {
            //transform.parent.gameObject.SetActive(false);
            Destroy(transform.root.gameObject);
            if (DeathFX != null)
                Instantiate(DeathFX, transform.position, DeathFX.transform.rotation);
        }
        healthSlider.gameObject.SetActive(true);
        //Invoke("HideHealthSlider", 1f);
    }

    void HideHealthSlider()
    {
        //healthSlider.gameObject.SetActive(false);
    }

}
