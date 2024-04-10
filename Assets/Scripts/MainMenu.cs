using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject ShoesMenu;

    public void OpenShoes()
    {
        mainMenu.SetActive(false);
        ShoesMenu.SetActive(true);
    }

    //public void CloseShoes() FIRST CODE OF ALPACIN ON C#
    //{
    //    ShoesMenu.SetActive(false);
    //    mainMenu.SetActive(true);
    //}

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        ShoesMenu.SetActive(false);

    }
    void Start()
    {
        OpenMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
