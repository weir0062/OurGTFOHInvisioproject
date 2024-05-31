using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject levelsMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject MusicContObject;
    private MusicController MusicController;

    void Start()
    {
        OpenMainMenu();
    }

    //Fix to Arcade
    public void OpenLevels()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void ButtonClick()
    {
        MusicController.PlaySFXSound();

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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level1");
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene fully loads
        }
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        MusicController = MusicContObject.GetComponent<MusicController>();
        MusicController.SetMusicClip(MusicController.MenuMusicClip);
        MusicController.SetSFXClip(MusicController.MenuSFXClip);
    }

    public void OpenLevel1()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadSceneAsync("Level2");
    }

    public void OpenLevel3()
    {
        SceneManager.LoadSceneAsync("Level3");
    }
    public void OpenLevel4()
    {
        SceneManager.LoadSceneAsync("Level4");
    }
    public void OpenLevel5()
    {
        SceneManager.LoadSceneAsync("Level5");
    }
    public void OpenLevel6()
    {
        SceneManager.LoadSceneAsync("Level6");
    }
    public void OpenLevel7()
    {
        SceneManager.LoadSceneAsync("Level7");
    }
    public void OpenLevel8()
    {
        SceneManager.LoadSceneAsync("Level8");
    }
    public void OpenLevel9()
    {
        SceneManager.LoadSceneAsync("Level69");
    }
    public void OpenLevel10()
    {
        SceneManager.LoadSceneAsync("Level10");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
