using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BuyItem : MonoBehaviour
{
    [SerializeField] GameObject Canvas1;
    [SerializeField] GameObject Canvas2;
    [SerializeField] GameObject Canvas3;
    [SerializeField] HelperScript helperScript;
    [SerializeField] Text moneyText;
    [Space]
    [SerializeField] KeyCode PressKeyCode = KeyCode.E;
    public float Money;

    bool playerInRange;

    public enum ItemType
    {
        Health,
        FireBall
    }

    public ItemType Type;


    private void OnValidate()
    {
        moneyText = GetComponentInChildren<Text>();
        moneyText.text = Money.ToString();
    }

    void Start()
    {
        Canvas2.gameObject.SetActive(true);
        Canvas2.gameObject.SetActive(false);
        Canvas3.gameObject.SetActive(false);

        if (Type == ItemType.FireBall)
        {
            GameObject FireBall = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Ball.prefab", typeof(GameObject)) as GameObject;
            if (ObjectsInGame.PlayerDamage.Projectile == FireBall)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        else if (Type == ItemType.Health)
        {
            if (ObjectsInGame.CanvasController.HavePotion == true)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(PressKeyCode))
            {
                if (ObjectsInGame.CanvasController.Money >= Money)
                {
                    Canvas1.gameObject.SetActive(false);
                    Canvas3.gameObject.SetActive(true);
                    ObjectsInGame.CanvasController.Money -= Money;
                    Destroy(gameObject);
                    helperScript.DestroyWhenPlayerLelf();

                    if (Type == ItemType.FireBall)
                    {
                        GameObject FireBall = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fire Ball.prefab", typeof(GameObject)) as GameObject;
                        ObjectsInGame.PlayerDamage.Projectile = FireBall;
                    }
                    else if (Type == ItemType.Health)
                    {
                        ObjectsInGame.CanvasController.HavePotion = true;
                    }
                }
                else
                {
                    Canvas1.gameObject.SetActive(false);
                    Canvas2.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Canvas1.gameObject.SetActive(true);
            Canvas2.gameObject.SetActive(false);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
    