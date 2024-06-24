using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameMenu : MonoBehaviour
{

    public CameraController Cam;
    public SoundManager soundManager;
    public GameObject ZoomSlider;
    public GameObject PauseButton;
    public GameObject Resume;
    public GameObject MainMenu;
    public GameObject Restart;
    public SceneHandler m_SceneHandler;
    Slider slider;
    private void Awake()
    {
        if (m_SceneHandler == null)
        {
            m_SceneHandler = GameObject.FindObjectOfType<SceneHandler>();
        }
        // Ensure EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        Cam = GetComponentInParent<CameraController>();

        slider = ZoomSlider.GetComponent<Slider>();
        slider.minValue =10;
        slider.maxValue = 30;
        slider.value = 20 ;
        
        Cam.GetComponent<Camera>().fieldOfView = Cam.MinZoomIn;

        if (soundManager == null)
            soundManager = FindObjectOfType<SoundManager>();
    }

    public void ButtonClick()
    {
        //MusicController.PlaySFXSound();
        soundManager.PlayClickSound();

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
        
        Resume.SetActive(true);
        MainMenu.SetActive(true);
        Restart.SetActive(true);
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cam.SetIsPaused(true);
        PauseButton.SetActive(false);
        ZoomSlider.SetActive(false);
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        Cam.SetIsPaused(false);
        PauseButton.SetActive(true);
        ZoomSlider.SetActive(true);
    }

    public void ResumeGame()
    {
        Resume.SetActive(false);
        MainMenu.SetActive(false);
        Restart.SetActive(false);
        UnPause();
    }
    public void OpenMainMenu()
    {
        m_SceneHandler.LoadLevelAt(0);
    }
    public void RestartLevel()
    {
        m_SceneHandler.ReloadLevel(); //restart the level through SceneHandler
    }
    public void OnZoomSliderChanged()
    {
        slider.minValue = Cam.MinZoomIn; 
        slider.maxValue = Cam.MaxZoomIn;
        Cam.CameraZoom(slider.value);
    }
}
