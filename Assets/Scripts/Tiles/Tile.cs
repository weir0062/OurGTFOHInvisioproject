using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    NonActive
};
public class Tile : MonoBehaviour
{
    public TileState state; /*= TileState.Solid;*/
    public MeshRenderer MatRenderer;
    int StepsTaken = 0;

    int hp = 5;

    Material currentMat;
    public Material SolidMat;
    public Material SmallMat;
    public Material MidMat;
    public Material VeryMat;
    public Material MaxMat;
    public Material NonactiveMat;
    public Material SuperSolidMat;
    public GameObject PositionIndicator;
    public GameObject DialogueBoxObject;

    float moveDistance = 0.25f;
    public Vector3 initialPosition;
    Vector3 LowerPosition;
    Player player;
    public bool IsActive = false;
    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.tag = "Tile";
        InitializeDefaults();
        UpdateState();
        PositionIndicator.SetActive(false);
        SpriteRenderer indicatorSprite = PositionIndicator.GetComponent<SpriteRenderer>();

        indicatorSprite.material.color = Color.black;

       // initialPosition = transform.localPosition;

        if (DialogueBoxObject != null)
        {
            DialogueBoxObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (IsActive == true && PositionIndicator.active == false)
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
        transform.localPosition = initialPosition;
    }

    public void StepTaken()
    {
        if (state == TileState.Finish)
        {
            //SceneHandler handler = GameObject.FindObjectOfType<SceneHandler>();
            //handler.LoadLevelAt(0);
            SceneManager.LoadScene("MainMenu");
            return;
        } 
        if (state == TileState.MaxDamaged)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        if (state == TileState.NonActive)
        {
            return;
        }

        if (state == TileState.SuperSolid)
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

    public void StepEnded()
    {
            MoveTileUp();

        if (state != TileState.NonActive)
        {
            SetNotActive();
        }

    }

    public void SetActive()
    {
        IsActive = true;
        PositionIndicator.SetActive(true);

    }
    public void SetNotActive()
    {
        IsActive = false;
        PositionIndicator.SetActive(false);
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
                currentMat= SolidMat;
                StepsTaken = 0; break;
            case TileState.SmallDamage:
                currentMat= SmallMat;
                StepsTaken = 1; break;
            case TileState.MidDamage:
                currentMat= MidMat;
                StepsTaken = 2; break;
            case TileState.VeryDamaged:
                currentMat = VeryMat;

                StepsTaken = 3; break;
            case TileState.MaxDamaged:
                currentMat= MaxMat;
                StepsTaken = 4; break;
            case TileState.NonActive:
                currentMat= NonactiveMat;
                StepsTaken = 69;
                break;
            case TileState.SuperSolid:
                currentMat= SuperSolidMat;
                StepsTaken = 69;
                break;
            case TileState.Finish:
                currentMat = SuperSolidMat;
                StepsTaken = 69;
                break;
        }

        hp = state == TileState.NonActive ? 0 : 5 - StepsTaken;
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
                Destroy(this);
                break;
            default:
                break;
        }


        hp = state == TileState.NonActive ? 0 : 5 - StepsTaken;
        MatRenderer.material = currentMat;

    }

}


