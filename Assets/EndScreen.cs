using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public List<RawImage> CoinImages;
    public TextMeshProUGUI PlayerTime;
    public TextMeshProUGUI TimeToBeat;
    public TextMeshProUGUI Score;

    ScoreManager scoreManager;
    SceneHandler sceneHandler;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        sceneHandler= FindObjectOfType<SceneHandler>();
    }

    public void DisplayScore()
    {
        //Nomrally start will never be called because this object starts as inactive

        //Ensuring we have references 
        if (scoreManager == null) scoreManager = FindObjectOfType<ScoreManager>();

        //Coins
        for(int i = 0; i < 3; i++) 
        {
            if (scoreManager.Coins[i].Collected == false)
            {
                CoinImages[i].color = Color.gray;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        sceneHandler.LoadNextLevel();
    }

    public void Reset()
    {
        sceneHandler.ReloadLevel();
    }
}
