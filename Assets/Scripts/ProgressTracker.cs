using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress
{
    public int HighScore;
}

public class ProgressTracker : MonoBehaviour, Saveable
{
    public List<LevelProgress> LevelScores = new List<LevelProgress>();
    public List<TextMeshProUGUI> LevelScoreTexts;
    public MainMenu mainMenu;
    public int NumberOfScenesBeforeArcade = 10;
    public SceneHandler sceneHandler;

    public static ProgressTracker Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            //This tells unity not to delete the object when you load another scene
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (LevelScores.Count <= 0)
        {
            for(int i = 0; i < 10; i++)
            {
                LevelScores.Add(new LevelProgress { HighScore = 0 });
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        mainMenu = FindObjectOfType<MainMenu>();
        sceneHandler= FindObjectOfType<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMenu()
    {
        if(mainMenu == null) 
        {
            mainMenu = FindObjectOfType<MainMenu>();
        }


        if(mainMenu != null) 
        {
            LevelScoreTexts = mainMenu.LevelScoreTexts;
            for(int i = 0; i < 10; i++) 
            {
                char ScoreLetter = EndScreen.ConvertScoreToLetter(LevelScores[i].HighScore);
                LevelScoreTexts[i].text = ScoreLetter.ToString();
            }
        }

    }

    public void UpdateScore(int score)
    {
        if(sceneHandler == null) 
        {
            sceneHandler= FindObjectOfType<SceneHandler>();
        }
        
        if(sceneHandler!= null)
        {
            int AracdeLevelIndex = sceneHandler.LevelID - 11;

            if(score > LevelScores[AracdeLevelIndex].HighScore)
                LevelScores[AracdeLevelIndex].HighScore= score;
        }

        sceneHandler.Save(sceneHandler.SaveFileName);
    }

    public void OnSave(Stream stream, IFormatter formatter)
    {
        for (int i = 0; i < 10; i++)
        {
            formatter.Serialize(stream, LevelScores[i].HighScore);
        }
    }

    public void OnLoad(Stream stream, IFormatter formatter)
    {
        if(LevelScores == null || LevelScores.Count <=0)
        {
            for (int i = 0; i < 10; i++)
            {
                LevelScores.Add(new LevelProgress { HighScore = 0 });
            }
        }

        for (int i = 0; i < 10; i++)
        {
            LevelScores[i].HighScore = (int)formatter.Deserialize(stream);
            DebugUtils.Log("HighScores loaded - Level #" + i + ": " + LevelScores[i].HighScore);
        }

        UpdateMenu();

        //LoadLevelAt(LevelID);

    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            UpdateMenu();
        }
    }
}
