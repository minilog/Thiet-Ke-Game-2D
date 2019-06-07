using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject DeathFX;
    [SerializeField] Object FloatingNumerPrefab;
    [SerializeField] Transform FloatingNumberTransform;
    [SerializeField] List<GameObject> ItemGameObjects;
    public int ItemDropPack = 0;
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
        FloatingNumberTransform = transform.GetChild(0);

        healthSlider = transform.root.GetComponentInChildren<Slider>();

        ItemGameObjects.Clear();
        GameObject Money1 = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Money/Money 1.prefab", typeof(GameObject)) as GameObject;
        GameObject Money2 = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Money/Money 2.prefab", typeof(GameObject)) as GameObject;
        ItemGameObjects.Add(Money1);
        ItemGameObjects.Add(Money2);
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

        GameObject fGO = Instantiate(FloatingNumerPrefab, FloatingNumberTransform.position, Quaternion.identity) as GameObject;
        FloatingNumber fNum = fGO.GetComponent<FloatingNumber>();
        fNum.RightDirection = !fromRightSide;
        fNum.Number = damage;

        if (Health <= 0)
        {
            //transform.parent.gameObject.SetActive(false);
            Destroy(transform.root.gameObject);
            if (DeathFX != null)
                Instantiate(DeathFX, transform.position, DeathFX.transform.rotation);

            if (Random.Range(0, 100) <= 66)
                DropItem();
        }
        healthSlider.gameObject.SetActive(true);
        //Invoke("HideHealthSlider", 1f);
    }

    void HideHealthSlider()
    {
        //healthSlider.gameObject.SetActive(false);
    }


    void DropItem()
    {
        if (ItemDropPack == 0)
        {
            int ran = Random.Range(0, 4);
            // Drop 1 Gold
            if (ran <= 1)
            {
                Instantiate(ItemGameObjects[0], transform.position, Quaternion.identity);

            }
            // Drop 2 Gold
            else if (ran == 2)
            {
                Instantiate(ItemGameObjects[0], transform.position, Quaternion.identity);
                Instantiate(ItemGameObjects[0], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ItemGameObjects[1], transform.position, Quaternion.identity);
            }
        }
        else if (ItemDropPack == 1)
        {
            int ran = Random.Range(0, 2);
            // Drop 3 Gold
            if (ran == 0)
            {
                Instantiate(ItemGameObjects[0], transform.position, Quaternion.identity);
                Instantiate(ItemGameObjects[1], transform.position, Quaternion.identity);
            }
            // Drop 4 Gold
            else
            {
                Instantiate(ItemGameObjects[1], transform.position, Quaternion.identity);
                Instantiate(ItemGameObjects[1], transform.position, Quaternion.identity);
            }
        }
    }
}
