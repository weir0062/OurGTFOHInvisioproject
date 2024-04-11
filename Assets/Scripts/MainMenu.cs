using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject ShoesMenu;
    public GameObject settingsMenu;
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
        ShoesMenu.SetActive(true);
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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CharlieScene");

    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        ShoesMenu.SetActive(false);
        settingsMenu.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

    }
}
