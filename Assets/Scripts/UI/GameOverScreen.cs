using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOverScreen : MonoBehaviour
{
    public SceneHandler m_SceneHandler;
    public GameObject Restart;
    public GameObject MainMenu;
    public GameObject Text;


    bool Clicked = false;
    float ClickTimer = 0.0f;
    float ResetTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (m_SceneHandler == null)
        {
            m_SceneHandler = GameObject.FindObjectOfType<SceneHandler>();
        }



    }
    public void ToggleMenu()
    {
        bool isActive = !this.gameObject.activeSelf;
        this.gameObject.SetActive(isActive);

        if (isActive)
        {
            TurnOnDeathMenu();
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        if (Clicked == true)
        {
            if (ClickTimer < ResetTime)
            {
                ClickTimer += Time.deltaTime;
            }
            else
            {
                ClickTimer = 0.0f;
                Clicked = false;
            }
        }
    }
    public void TurnOnDeathMenu()
    {
        //if (m_SceneHandler.LevelID > 10)
        //    return;


         InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
         IGMenu?.Pause();
         Restart.SetActive(true);
         MainMenu.SetActive(true);
         Text.SetActive(true);

    }

    public void RestartButton()
    {
        if(Clicked == false)
        {
            InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
            m_SceneHandler.LoadLevelAt(m_SceneHandler.LevelID);
            //    IGMenu?.UnPause();

            Time.timeScale = 1f;
            IGMenu.Cam.SetIsPaused(false);
            Clicked = true;
        }
    }

    public void ExitButton()
    {
        if (Clicked == false)
        {
            InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
            m_SceneHandler.LevelReturnedFrom = m_SceneHandler.LevelID;
            m_SceneHandler.Returning = true;
            m_SceneHandler.LoadLevelAt(0);
            //   IGMenu?.UnPause();

            Time.timeScale = 1f;
            IGMenu.Cam.SetIsPaused(false);
            Clicked = true;
        }
    }

}

