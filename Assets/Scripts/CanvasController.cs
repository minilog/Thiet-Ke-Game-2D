using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Image image;

    private float a;
    public float speedChangeColor;

    private bool Blacking;

    private void Update()
    {
        if (Blacking)
        {


            if (a < 255)
                a += speedChangeColor * Time.deltaTime;
            else
                a = 255;
        }
        else
        {
            if (a > 0)
                a -= speedChangeColor * Time.deltaTime;
            else
                a = 0;  
        }

        Color c = image.color;
        c.a = a;
        image.color = c;
    }

    public void ImageBlacking()
    {
        Blacking = true;
    }

    public void ImageWhiting()
    {
        Blacking = false;
    }
}
