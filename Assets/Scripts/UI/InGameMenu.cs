﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameMenu : MonoBehaviour
{

    public CameraController Cam;
    public GameObject ZoomSlider;
    public GameObject PauseButton;
    public GameObject Resume;
    public GameObject MainMenu;
    public GameObject Restart;

    Slider slider;

    public SceneHandler sceneHandler;
    private void Awake()
    {


        // Ensure EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        Cam = GetComponentInParent<CameraController>();

        slider = ZoomSlider.GetComponent<Slider>();
        
        sceneHandler = FindObjectOfType<SceneHandler>();
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
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        sceneHandler.LoadLevelAt(0);
    }
    public void RestartLevel()
    {

        //restart the level through SceneManager
       
    }
    public void OnZoomSliderChanged()
    {
        slider.minValue = Cam.MinZoomIn/1.69f;
        slider.maxValue = Cam.MaxZoomIn / 3.69f;
        Cam.CameraZoom(slider.value);
    }
}
