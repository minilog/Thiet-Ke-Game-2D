using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] Slider staminaSlider;
    [SerializeField] Text staminaText;

    public float MaxStamina = 100;
    float _stamina;
    public float Stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;
            staminaSlider.value = _stamina;
            staminaText.text = ((int)value).ToString() + "/" + ((int)MaxStamina).ToString();
        }
    }

    public float DashStamina = 50;
    public float FireStamina = 10f;

    public float StaminaPerSecs = 10f;

    // Start is called before the first frame update
    void Start()
    {
        staminaSlider.maxValue = MaxStamina;
        Stamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (Stamina < MaxStamina)
        {
            Stamina += StaminaPerSecs * Time.deltaTime;
        }
    }

    public bool CheckUseStamina(float stamina)
    {
        if (Stamina >= stamina)
        {
            Stamina -= stamina;
            return true;
        }

        return false;
    }

    public bool CheckDashStamina()
    {
        return CheckUseStamina(DashStamina);
    }

    public bool IsEnoughStaminaForFire()
    {
        if (Stamina >= FireStamina)
            return true;
        else
            return false;
    }
}
