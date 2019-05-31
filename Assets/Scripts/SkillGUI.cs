using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGUI : MonoBehaviour
{
    [SerializeField] Image cooldownFillImage;
    [SerializeField] Text timeText;

    float coolDownTime;
    void Start()
    {
        coolDownTime = ObjectsInGame.PlayerController.DashCooldown;
        //timeText.text = "";
    }

    void Update()
    {
        float cooldown = ObjectsInGame.PlayerController.DashCooldownCount;

        timeText.text = (Mathf.Round(cooldown * 10f) / 10f).ToString();

        if (cooldown <= 0)
        {
            cooldown = 0;
            timeText.text = "";
        }

        cooldownFillImage.fillAmount = cooldown / coolDownTime;
    }
}
