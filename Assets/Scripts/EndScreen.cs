using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;



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

        //Variable Creation for converstion from seconds into clock style time
        float PlayScoreTime = scoreManager.TimeElpased;
        float TimeToBeatScoreTime = scoreManager.TimeToBeat;

        int PlayTimeMinutes = 0;
        int TimeToBeatMinutes = 0;

        int PlayTimeSeconds = 0;
        int TimeToBeatSeconds = 0;

        //Time formatting from seconds to clock style i.e 1:05 for 65 seconds
        PlayTimeMinutes = (int)(PlayScoreTime / 60);
        PlayTimeSeconds = (int)(PlayScoreTime % 60);

        TimeToBeatMinutes = (int)(TimeToBeatScoreTime / 60);
        TimeToBeatSeconds = (int)(TimeToBeatScoreTime % 60);


        string playTimeFormatted = string.Format("{0}:{1:D2}", PlayTimeMinutes, PlayTimeSeconds);
        string timeToBeatFormatted = string.Format("{0}:{1:D2}", TimeToBeatMinutes, TimeToBeatSeconds);

        PlayerTime.text = playTimeFormatted;
        TimeToBeat.text = timeToBeatFormatted;

        //Color change based off of if the player got the time score point or not
        if(scoreManager.TimeElpased >= scoreManager.TimeToBeat)
        {
            PlayerTime.color = Color.grey;
        }
        else if(scoreManager.TimeElpased < scoreManager.TimeToBeat)
        {
            PlayerTime.color = Color.white;
            scoreManager.Score++;
        }

        char ScoreLetter = ConvertScoreToLetter(scoreManager.Score);

        Score.text = ScoreLetter.ToString();


    }

    public static char ConvertScoreToLetter(int score)
    {
        char ScoreLetter = 'D';

        if (score == 0)
        {
            ScoreLetter = 'D';
        }
        else if (score == 1)
        {
            ScoreLetter = 'C';
        }
        else if (score == 2)
        {
            ScoreLetter = 'B';
        }
        else if (score == 3)
        {
            ScoreLetter = 'A';
        }
        else if (score == 4)
        {
            ScoreLetter = 'S';
        }

        return ScoreLetter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 20)
        {
            sceneHandler.LoadLevelAt(0);
            return;
        }

        sceneHandler.LoadNextLevel();
    }

    public void Reset()
    {
        sceneHandler.LoadLevelAt(sceneHandler.LevelID);
    }

    public void ReturnMainMenu()
    {
        sceneHandler.LoadLevelAt(0);
    }
}
