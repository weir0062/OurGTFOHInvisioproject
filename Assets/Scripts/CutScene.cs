using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    public List<Texture> CutsceneImages;

    int CurrentImage = 0;

    RawImage DisplayWindow;

    //MUST BE LONGER THAN THE TIME IT TAKES TO FADE
    float TimeBeforeSkip = 3f;

    float Timer = 0.0f;

    bool CanSkip = false;

    Fade FadeObject;

    bool First = false;

    bool Clicked = false;

    public bool Paused = false;

    // Start is called before the first frame update
    void Start()
    {
        DisplayWindow = GetComponentInChildren<RawImage>();

        DisplayWindow.texture = CutsceneImages[CurrentImage];
        FadeObject = GameObject.FindObjectOfType<Fade>();



    }

    // Update is called once per frame
    void Update()
    {

        if (Paused == true)
            return;


        if(First == false)
        {
            FadeObject.FadeIn();
            Timer = 0.0f;
            CanSkip = false;
            First = true;
        }

        if (CanSkip == false)
        {
            if(Timer <= TimeBeforeSkip) 
            {
                Timer += Time.deltaTime;
            }
            else
            {
                CanSkip= true;
                Timer= 0.0f;
            }
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                FadeObject.FadeOutThanIn();

                Clicked = true;

            }

        }


        if (Clicked == true)
        {


            if (FadeObject.FadeAmount >= 1.0f)
            {
                CurrentImage++;
                Clicked = false;
            }

            if (CurrentImage >= CutsceneImages.Count)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                DisplayWindow.texture = CutsceneImages[CurrentImage];
                CanSkip = false;

            }

        }

    }
}
