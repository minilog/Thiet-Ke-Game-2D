using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject FloatingNumberPrefab;
    [Space]
    public float MaxHealth = 100;
    float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthSlider.value = _health;
            healthText.text = ((int)value).ToString() + "/" + ((int)MaxHealth).ToString();
        }
    }

    [SerializeField] Animator animator;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;
    [SerializeField] GameObject floatingPostionPrefab;

    public float cantTakeDamageTime;
    float cantTakeDamageCounter = 0;

    public KeyCode DeathKeyCode = KeyCode.P;

    public Vector2 HurtDirection { get; private set; }

    //private void OnValidate()
    //{
    //    FloatingNumberPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Effect/Player Floating Number.prefab", typeof(GameObject)) as GameObject;
    //}

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

        if (Input.GetKeyDown(KeyCode.F10))
        {
            CheckTakeDamage(100, true);
        }

        if (cantTakeDamageCounter > 0)
        {
            cantTakeDamageCounter -= Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    Health = MaxHealth;
        //}

        if (Input.GetKeyDown(DeathKeyCode))
        {
            animator.Play("Player Die");
        }

        if (Health <= 0)
        // At the end this Behavior, destroy gameObject
        {
            if (ObjectsInGame.CanvasController.HavePotion)
            {
                Health = MaxHealth;
                ObjectsInGame.CanvasController.HavePotion = false;
                Instantiate(floatingPostionPrefab, transform.position + new Vector3(0f, -0.4f, 0f), Quaternion.identity);
            }
            else
                animator.Play("Player Die");
        }

        else
            // Change to animation Hurt
            animator.SetFloat("CanNotTakeDamage", cantTakeDamageCounter);   
    }

    public void CheckTakeDamage(float damage, bool isFromTheRightSide)
    {
        if (CanTakeDamage())
        {
            //damage = (int)Random.Range(damage - 1, damage + 2);

            // set Animation Player Hurt
            animator.SetTrigger("TakeDamage");
            // Set Time for animation Player Flicker
            cantTakeDamageCounter = cantTakeDamageTime;
            // Lost Health
            Health -= damage;

            GameObject GO = Instantiate(FloatingNumberPrefab, transform.position + new Vector3(0f, -0.45f, 0f), Quaternion.identity);
            GO.GetComponent<FloatingNumber>().RightDirection = !isFromTheRightSide;
            GO.GetComponent<FloatingNumber>().Number = damage;

            if (isFromTheRightSide) 
                HurtDirection = new Vector2(-7f, 5f);
            else
                HurtDirection = new Vector2(7f, 5f);
        }
    }

    public bool CanTakeDamage()
    {
        return (cantTakeDamageCounter <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Strike") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Dash") &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Player Die"));
    }

    public void PlayerDieAndRestartLevel()
    {
        gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name != "Final Level")
            ObjectsInGame.ChangeSceneManager.ChangeToNextScene(SceneManager.GetActiveScene().name);
        else
        {
            ObjectsInGame.ChangeSceneManager.ChangeToNextScene("Lv.07");
            PositionInNewScene.SetValueForNewScene(0, Vector3.zero);
        }
    }

    public void CheckSpawnPlayer()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Health = MaxHealth;
            PlayerStamina playerStamina = GetComponent<PlayerStamina>();
            playerStamina.Stamina = playerStamina.MaxStamina;

        }
    }

}
