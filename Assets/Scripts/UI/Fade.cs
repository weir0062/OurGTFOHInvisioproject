using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public RawImage Darkness;

    public float Speed;

    public float FadeAmount;

    FadeType fadeType = FadeType.None;


    // Start is called before the first frame update
    void Start()
    {
        Darkness = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F2))
        {
            FadeIn();
        }
        else if(Input.GetKey(KeyCode.F3))
        {
            FadeOut();
        }

        switch(fadeType) 
        {
            default:
            case FadeType.None:
                break;

            case FadeType.FadeIn:
                FadeAmount -= Speed * Time.deltaTime;
                if(FadeAmount <= 0)
                {
                    FadeAmount = 0;
                    Darkness.color = new Color(0, 0, 0, FadeAmount);
                    fadeType = FadeType.None;
                    Darkness.enabled = false;
                }
                else
                    Darkness.color = new Color(0, 0, 0, FadeAmount);
                break;

            case FadeType.FadeOut:
                FadeAmount += Speed * Time.deltaTime;
                if (FadeAmount >= 1)
                {
                    Darkness.enabled = true;
                    FadeAmount = 255;
                    Darkness.color = new Color(0, 0, 0, FadeAmount);
                    fadeType = FadeType.None;
                }
                else
                    Darkness.color = new Color(0, 0, 0, FadeAmount);
                break;
        }
    }

    public void FadeIn()
    {
        Darkness.enabled = true;
        FadeAmount = 1f;
        Darkness.color = new Color(0,0,0, FadeAmount);
        fadeType = FadeType.FadeIn;

        

    }

    public void FadeOut()
    {
        Darkness.enabled = true;
        FadeAmount = 0f;
        Darkness.color = new Color(0, 0, 0, FadeAmount);
        fadeType = FadeType.FadeOut;



    }


    enum FadeType
    {
        None,
        FadeIn,
        FadeOut,
    }
}
