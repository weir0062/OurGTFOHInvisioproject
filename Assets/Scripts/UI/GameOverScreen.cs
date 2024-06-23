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

    }
    public void TurnOnDeathMenu()
    {
        InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
        IGMenu?.Pause();
        Restart.SetActive(true);
        MainMenu.SetActive(true);
    }

    public void RestartButton()
    {
        InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
        IGMenu?.UnPause();
        m_SceneHandler.ReloadLevel();
    }

    public void ExitButton()
    {
        InGameMenu IGMenu = GameObject.FindObjectOfType<InGameMenu>();
        IGMenu?.UnPause();
        m_SceneHandler.LoadLevelAt(0);
    }

}

