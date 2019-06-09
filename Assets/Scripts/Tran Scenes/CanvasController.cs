using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Image image;

    private float a;
    private float timeChangeColor;

    private float speedOfChanging;
    private bool Blacking;

    private bool Changing = false;

    [SerializeField] GameObject keyImage;
    bool _haveKey = false;
    public bool HaveKey
    {
        get { return _haveKey; }
        set { _haveKey = value; keyImage.gameObject.SetActive(_haveKey); }
    }

    [Space]
    [SerializeField] GameObject potionImage;
    bool _havePotion = false;
    public bool HavePotion
    {
        get { return _havePotion; }
        set { _havePotion = value; potionImage.gameObject.SetActive(_havePotion); }
    }


    [Space]
    // Money
    [SerializeField] Text moneyText;
    public float _money;
    public float Money
    {
        get { return _money; }
        set { _money = value; moneyText.text = "X " + _money.ToString(); }
    }


    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        moneyText = texts[1];
        moneyText.text = "X " + _money.ToString();

        //HaveKey = false;
        //HavePotion = false;
    }

    private void Awake()
    {
        ObjectsInGame.CanvasController = this;
    }

    private void Start()
    {
        HaveKey = false;
        HavePotion = false;
    }

    private void Update()
    {
        UpdateScreenColor();
    }

    private void UpdateScreenColor()
    {
        if (Changing)
        {
            if (Blacking)
            {
                if (a < 1)
                    a += speedOfChanging * Time.unscaledDeltaTime;
                else
                {
                    a = 1;
                    Changing = false;
                }
            }
            else
            {
                if (a > 0)
                    a -= speedOfChanging * Time.unscaledDeltaTime;
                else
                {
                    a = 0;
                    Changing = false;
                }
            }

            Color c = image.color;
            c.a = a;
            image.color = c;
        }
    }

    public void ImageBlacking(float time)
    {
        Blacking = true;
        speedOfChanging = 1f / time + 0.05f;
        Changing = true;
    }

    public void ImageWhiting(float time)
    {
        Blacking = false;
        speedOfChanging = 1f / time + 0.05f;
        Changing = true;
    }
}
