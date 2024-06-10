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
    public GameObject ZoomSlider;
    public GameObject PauseButton;
    public GameObject Resume;
    public GameObject MainMenu;
    public GameObject Restart;

    Slider slider;
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
        slider.value = slider.minValue;
        Cam.GetComponent<Camera>().fieldOfView = Cam.MinZoomIn / 2.69f; 
        

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
        Pause();
        PauseButton.SetActive(false);
        Resume.SetActive(true);
        MainMenu.SetActive(true);
        Restart.SetActive(true);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        Cam.SetIsPaused(true);
    }
    void UnPause()
    {
        Time.timeScale = 1f;
        Cam.SetIsPaused(false);
    }

    public void ResumeGame()
    {
        UnPause();
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
    public void OnZoomSliderChanged()
    {
        slider.minValue = Cam.MinZoomIn/2.69f;
        slider.maxValue = Cam.MaxZoomIn / 4.69f;
        Cam.CameraZoom(slider.value);
    }
}
