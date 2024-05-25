using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
       
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
