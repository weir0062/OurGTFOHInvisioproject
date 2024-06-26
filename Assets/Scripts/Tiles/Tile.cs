using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum TileState
{
    Solid,
    SmallDamage,
    MidDamage,
    VeryDamaged,
    MaxDamaged,
    SuperSolid,
    Finish,
    NonActive,
    Start
};
public class Tile : MonoBehaviour
{
    public TileState state;
    public MeshRenderer MatRenderer;
    int StepsTaken = 0;
    Material currentMat;
    public Material SolidMat;
    public Material SmallMat;
    public Material MidMat;
    public Material VeryMat;
    public Material MaxMat;
    public Material NonactiveMat;
    public Material SuperSolidMat;
    public GameObject DialogueBoxObject;
    float moveDistance = 0.25f;
    public Vector3 initialPosition;
    Vector3 LowerPosition;
    Player player;
    public bool IsActive = false;
    Vector2 position;
    public SceneHandler m_SceneHandler;
    public TextMeshPro num;
    public GameObject[] StartObjects;
    [HideInInspector] public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.tag = "Tile";
        InitializeDefaults();
        UpdateState();

        if (m_SceneHandler == null)
        {
            m_SceneHandler = GameObject.FindObjectOfType<SceneHandler>();
        }

        // initialPosition = transform.localPosition;

        if (DialogueBoxObject != null)
        {
            DialogueBoxObject.SetActive(false);
        } 
        



        if (StartObjects.Length > 0)
        {

            for (int i = 0; i < StartObjects.Length; i++)
            {

                StartObjects[i].SetActive(true);
            }
        }


    }
    private void Update()
    {
        if (IsActive == true)
        {
            SetActive();
        }
    }
    public void Pressed()
    {
        if (state == TileState.NonActive)
        {

            Debug.Log("NonActiveTile");
            return;
        }
        if (player == null)
        {
            InitializePlayer();
        }
        if (DialogueBoxObject != null)
        {
            DialogueBoxObject.SetActive(true);
        }
        player?.TakeStep(this);
    }

    void InitializePlayer()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }

    public void SetPosition(int x, int y)
    {
        position = new Vector2(x, y);
    }

    public Vector2 GetPosition()
    {
        return position;

    }
    public void StepAnimation()
    {
        MoveTileDown();
    }

    void MoveTileDown()
    {
        LowerPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + moveDistance);
        transform.localPosition = LowerPosition;
    }

    void MoveTileUp()
    {
        initialPosition.z = 0.0f;
        transform.localPosition = initialPosition;
    }

    public void StepTaken()
    {
        if (state == TileState.Finish) // level end, level passed. Here we will add moving on to the next level 
        {
            //SceneHandler handler = GameObject.FindObjectOfType<SceneHandler>();
            //handler.LoadLevelAt(0);
            //SceneManager.LoadScene("MainMenu");
           FinishedLevel();
            
            m_SceneHandler.LoadNextLevel();


            return;
        }

        if (state == TileState.NonActive)
        {
            return;
        }

        if (state == TileState.SuperSolid || state == TileState.Start)
        {
            SetActive();
            MoveTileDown();
            return;
        }

        StepsTaken++;
        MoveTileDown();
        UpdateState();
        SetActive();

    }


    public void FinishedLevel()
    {
        ScoreManager scoreManager = player.scoreManager;

        if (scoreManager != null) // This check means "If we are in Arcade mode do X"
        {
            EndScreen endScreen = player.scoreManager.endScreen.GetComponent<EndScreen>();
            ProgressTracker highscores = FindObjectOfType<ProgressTracker>();


            if (endScreen == null)
                return;

            endScreen.gameObject.SetActive(true);
            endScreen.DisplayScore();

            if (highscores != null)
                highscores.UpdateScore(scoreManager.Score);

            return;
        }
    }
    public void StepEnded()
    {
        MoveTileUp();

        if (state != TileState.NonActive)
        {
            SetNotActive();
        }


        if (StartObjects.Length > 0)
        {

            for(int i = 0; i < StartObjects.Length; i++)
            {

            StartObjects[i].SetActive(false);
            }
        }


    }

    public void SetActive()
    {
        IsActive = true;

    }
    public void SetNotActive()
    {
        IsActive = false;
    }
    public int GetStepsTaken()
    {
        return StepsTaken;
    }



    void InitializeDefaults()
    {
        switch (state)
        {
            case TileState.Solid:
                currentMat = SolidMat;
                StepsTaken = 0; break;
            case TileState.SmallDamage:
                currentMat = SmallMat;
                StepsTaken = 1; break;
            case TileState.MidDamage:
                currentMat = MidMat;
                StepsTaken = 2; break;
            case TileState.VeryDamaged:
                currentMat = VeryMat;

                StepsTaken = 3; break;
            case TileState.MaxDamaged:
                currentMat = MaxMat;
                StepsTaken = 4; break;
            case TileState.NonActive:
                currentMat = NonactiveMat;
                StepsTaken = 69;
                break;
            case TileState.SuperSolid:
                currentMat = SuperSolidMat;
                StepsTaken = 69;
                break;
            case TileState.Finish:
                currentMat = SuperSolidMat;
                StepsTaken = 69;
                break;
        }

        //   hp = state == TileState.NonActive ? 0 : 5 - StepsTaken;
    }


    void UpdateState()
    {
        switch (StepsTaken)
        {
            case 0:
                state = TileState.Solid;
                currentMat = SolidMat;
                break;

            case 1:
                state = TileState.SmallDamage;
                currentMat = SmallMat;
                break;
            case 2:
                state = TileState.MidDamage;
                currentMat = MidMat;
                break;
            case 3:
                state = TileState.VeryDamaged;
                currentMat = VeryMat;
                break;
            case 4:
                state = TileState.MaxDamaged;
                currentMat = MaxMat;
                break;
            case 5:
                Death();
                break;
            default:
                break;
        }


        // hp = state == TileState.NonActive ? 0 : 5 - StepsTaken;
        MatRenderer.material = currentMat;

    }

    private void Death()
    {

        if(Camera == null) {
        Camera = GameObject.FindObjectOfType<CameraController>().gameObject;
        }
        GameOverScreen GOScreen = Camera.GetComponentInChildren<GameOverScreen>();
        if (GOScreen != null)
        {
            GOScreen = GameObject.FindObjectOfType<GameOverScreen>();
        }
        FinishedLevel();
        GOScreen.TurnOnDeathMenu();
        
        return;
    }
}


