using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumber : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Text text;
    [Space]
    public bool RightDirection;
    public float MaxFloatingSpeed;
    public float AliveTime;
    [Range(0, 1)]
    public float ThresholdToChangeColor;
    //public float MaxOffsetWhenStart;
    public float MaxXVelocity_State1;
    public float MinXVelocity_State1;
    [Range(-10, 0)]
    public float MinYVelocity_State1;
    [Range(0, 10)]
    public float MaxYVelocity_State1;
    [Range(-2, 0)]
    public float MinYOffset_State1;
    [Range(0, 2)]
    public float MaxYOffset_State1;
    [Range(0, 10)]
    public float MinGravityScale_State2;
    [Range(0, 10)]
    public float MaxGravityScale_State2;
    public float GraviryLessThan0Percent;
    public float GraviryGreaterThan0Percent;
    public float GraviryEqual0Percent;
    public float LerpSpeedVelocity_State2;

    private float changeColorTime;
    private float speedChangeColor;
    private float increCount;

    private void OnValidate()
    {
        rb2D = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        changeColorTime = AliveTime * ThresholdToChangeColor;
        speedChangeColor = 1 / (AliveTime - changeColorTime);
        increCount = 0;
    }

    private void Update()
    {
        if (rb2D.velocity.y > MaxFloatingSpeed)
            rb2D.velocity = new Vector2(rb2D.velocity.x, MaxFloatingSpeed);
        else if (rb2D.velocity.y < -MaxFloatingSpeed)
            rb2D.velocity = new Vector2(rb2D.velocity.x, -MaxFloatingSpeed);

        increCount += Time.deltaTime;

        if (increCount >= changeColorTime)
            ChangeTextColorOverTime();

        if (increCount >= AliveTime)
            Destroy(gameObject);
    }



    void ChangeTextColorOverTime()
    {
        Color color = text.color;
        color.a -= speedChangeColor * Time.deltaTime;
        text.color = color;
    }
}
