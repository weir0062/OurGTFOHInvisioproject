using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{


    public GameObject PauseButton;
    public GameObject Resume;
    public GameObject MainMenu;
    public GameObject Restart;
    private void Start()
    {


        // Ensure EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }


    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        bool isActive = !this.gameObject.activeSelf;
        this.gameObject.SetActive(isActive);

        if (isActive)
        {
            TurnOnMenu();
        }
        else
        {
            ResumeGame();
        }
    }

    public void TurnOnMenu()
    {
        Time.timeScale = 0f;

        PauseButton.SetActive(false);
        Resume.SetActive(true);
        MainMenu.SetActive(true);
        Restart.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        PauseButton.SetActive(true);
        Resume.SetActive(false);
        MainMenu.SetActive(false);
        Restart.SetActive(false);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {

        //restart the level through SceneManager
       
    }

}
