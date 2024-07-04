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
    float TimeBeforeSkip = 1.5f;

    float Timer = 0.0f;

    bool CanSkip = false;

    Fade FadeObject;

    bool First = false;

    bool Clicked = false;

    public bool Paused = false;

    public SoundManager soundManager;

    public Fade IndependentFade;


    InGameMenu menu;

    public bool EndCutScene = false;
    public GameObject EndScutSceneImage;


    // Start is called before the first frame update
    void Start()
    {
        DisplayWindow = GetComponentInChildren<RawImage>();

        DisplayWindow.texture = CutsceneImages[CurrentImage];
        FadeObject = GameObject.FindObjectOfType<Fade>();

        if(soundManager ==null)
            soundManager = FindObjectOfType<SoundManager>();
        
        if(menu == null)
            menu = FindObjectOfType<InGameMenu>();

    }
     void Awake()
    {
        menu = GameObject.FindObjectOfType<InGameMenu>();
        menu.PauseButton.SetActive(false);
        menu.ZoomSlider.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

        if (Paused == true)
            return;


        if(First == false)
        {
            if(EndCutScene== false)
            {
                if(FadeObject != null)
                    FadeObject.FadeIn();

            }
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
                if(FadeObject== null)
                    FadeObject = FindObjectOfType<Fade>();

                if(FadeObject != null)
                    FadeObject.FadeOutThanIn();

                soundManager?.PlayClickSound();

                Clicked = true;

            }

        }


        if (Clicked == true)
        {

            if(FadeObject != null)
            {
                if (FadeObject.FadeAmount >= 1.0f)
                {
                    CurrentImage++;
                    Clicked = false;
                }
            }
            else
            {
                CurrentImage++;
                Clicked = false;
            }

            if (CurrentImage >= CutsceneImages.Count)
            {
                if(EndCutScene == true)
                {
                    SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

                    sceneHandler.LevelID = 1;

                    sceneHandler.Save(sceneHandler.SaveFileName);

                    sceneHandler.LoadLevelAt(0);

                    return;
                }

                IndependentFade.FadeIn();
                this.gameObject.SetActive(false);
                menu.PauseButton.SetActive(true);
                menu.ZoomSlider.SetActive(true);

            }
            else
            {
                DisplayWindow.texture = CutsceneImages[CurrentImage];
                CanSkip = false;

            }

        }

    }

}
