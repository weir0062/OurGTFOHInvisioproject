using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TurnOnMenu()
    {

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Play");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

