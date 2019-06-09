using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingMoney : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField] Rigidbody2D rb2D;
    [Space]
    public float Number;
    public float DisappearTime;
    public float SpeedChangeColor;
    public float StartYVelocity;

    private float increCount;

    Color zeroColor = new Color(1, 1, 1, 0);

    private void OnValidate()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2D.velocity = new Vector2(0, StartYVelocity);
        if (Number != 0)
            text.text = "+" + Number.ToString();
        increCount = 0;

        ObjectsInGame.SoundManager.PlayCoinCLip();
    }

    private void Update()
    {
        increCount += Time.deltaTime;
        if (increCount >= DisappearTime)
        {
            ChangeColorOverTime();
            
            if (text.color.a == 0)
            {
                Destroy(gameObject);
            }
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0;
        }
    }

    void ChangeColorOverTime()
    {
        Color color = text.color;
        color.a -= SpeedChangeColor * Time.deltaTime;
        if (color.a < 0)
            color.a = 0;

        text.color = color;

        Color color2 = image.color;
        color2.a = color.a;
        image.color = color2;
    }
}
