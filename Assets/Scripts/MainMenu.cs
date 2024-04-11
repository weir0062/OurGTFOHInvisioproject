using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject shoesMenu;
    public GameObject shoes2Menu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject MusicContObject;
    private MusicController MusicController;



    void Start()
    {
        OpenMainMenu();
        MusicController = MusicContObject.GetComponent<MusicController>();

    }
    public void OpenShoes()
    {
        mainMenu.SetActive(false);
        shoes2Menu.SetActive(false);
        shoesMenu.SetActive(true);
    }

    public void OpenShoes2()
    {
        shoesMenu.SetActive(false);
        shoes2Menu.SetActive(true);
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
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f); // Ensure any pending operations complete.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CharlieScene");
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene fully loads
        }
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        shoesMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        shoes2Menu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
