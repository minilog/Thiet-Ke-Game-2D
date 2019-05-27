﻿using System.Collections;
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

    private void Update()
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