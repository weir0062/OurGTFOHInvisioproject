using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject ArcadeMenu;
    public GameObject levelsMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject creditsMenu2;
    public GameObject supportMenu;
    public GameObject MusicContObject;
    private MusicController MusicController;

    SceneHandler sceneHandler;
    public SoundManager soundManager;

    public List<TextMeshProUGUI> LevelScoreTexts;
    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();

        if(soundManager == null )
            soundManager = FindObjectOfType<SoundManager>();

        OpenMainMenu();


    } 
    //Make this open Arcade mode
    public void OpenArcadeMenu()
    {
        mainMenu.SetActive(false);
        ArcadeMenu.SetActive(true);
    }

    public void OpenSupportMenu()
    {
        mainMenu.SetActive(false);
        supportMenu.SetActive(true);
    }

    public void OpenLevels()
    {
        mainMenu.SetActive(false);
        ArcadeMenu.SetActive(false);
        levelsMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu2.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void OpenCredits2()
    {
        creditsMenu.SetActive(false);
        creditsMenu2.SetActive(true);
    }
    public void ButtonClick()
    {
        //MusicController.PlaySFXSound();
        soundManager.PlayClickSound();

    }
    //public void CloseShoes() FIRST CODE OF ALPACIN ON C#
    //{
    //    ShoesMenu.SetActive(false);
    //    mainMenu.SetActive(true);
    //}

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenGameScene()
    {
        MusicController.SetSFXClip(MusicController.GameSFXClip);
        MusicController.SetMusicClip(MusicController.GameMusicClip);
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f); // Ensure any pending operations complete.


        //sceneHandler.LoadNextLevel(); -- not using save

        //using save
        sceneHandler.LoadLevelAt(sceneHandler.LevelID);
        
        
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");
        //while (!asyncLoad.isDone)
        //{
        //    yield return null; // Wait until the scene fully loads
        //}

        Time.timeScale = 1f;
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        ArcadeMenu.SetActive(false);
        levelsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        creditsMenu2.SetActive(false);
        supportMenu.SetActive(false);

        MusicController = MusicContObject.GetComponent<MusicController>();
        MusicController.SetMusicClip(MusicController.MenuMusicClip);
        MusicController.SetSFXClip(MusicController.MenuSFXClip);

        //sceneHandler.Load(sceneHandler.SaveFileName);
    }


    public void PlayArcade()
    {
        sceneHandler.LoadLevelAt(11);
    }
    public void LevelsToArcadeMenu()
    {
        levelsMenu.SetActive(false);
        ArcadeMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        //So this doesnt fucking with testing
        //using save
        if (sceneHandler.LevelID <= 0 || sceneHandler.LevelID > 10)
            sceneHandler.LoadLevelAt(1);
        else
            sceneHandler.LoadLevelAt(sceneHandler.LevelID);
    }


    public void OpenLevel1()
    {
        //SceneManager.LoadSceneAsync("Level1");
        sceneHandler.LoadLevelAt(1);

        Time.timeScale = 1f;
    }

    public void OpenLevel2()
    {
        //SceneManager.LoadSceneAsync("Level2");
        sceneHandler.LoadLevelAt(2);

    }

    public void OpenLevel3()
    {
        //SceneManager.LoadSceneAsync("Level3");
        sceneHandler.LoadLevelAt(3);

    }
    public void OpenLevel4()
    {
        //SceneManager.LoadSceneAsync("Level4");
        sceneHandler.LoadLevelAt(4);

    }
    public void OpenLevel5()
    {
        //SceneManager.LoadSceneAsync("Level5");
        sceneHandler.LoadLevelAt(5);

    }
    public void OpenLevel6()
    {
        //SceneManager.LoadSceneAsync("Level6");
        sceneHandler.LoadLevelAt(6);

    }
    public void OpenLevel7()
    {
        //SceneManager.LoadSceneAsync("Level7");
        sceneHandler.LoadLevelAt(7);

    }
    public void OpenLevel8()
    {
        //SceneManager.LoadSceneAsync("Level8");
        sceneHandler.LoadLevelAt(8);

    }
    public void OpenLevel9()
    {
        //SceneManager.LoadSceneAsync("Level69");
        sceneHandler.LoadLevelAt(9);

    }
    public void OpenLevel10()
    {
        //SceneManager.LoadSceneAsync("Level10");
        sceneHandler.LoadLevelAt(10);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
