using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public enum TileState
{
    Solid,
    SmallDamage,
    MidDamage,
    VeryDamaged,
    MaxDamaged,
    SuperSolid,
    NonActive
};
public class Tile : MonoBehaviour
{
    public TileState state = TileState.Solid;
    public SpriteRenderer spriteRenderer;
    int StepsTaken = 0;

    int hp = 5;

    Sprite currentSprite;
    public Sprite SolidSprite;
    public Sprite SmallSprite;
    public Sprite MidSprite;
    public Sprite VerySprite;
    public Sprite MaxSprite;
    public Sprite NonactiveSprite;
    public Sprite SuperSolidSprite;

    public GameObject PositionIndicator;

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

    }

    public void Pressed()
    {
        if(state == TileState.NonActive)
        {

        Debug.Log("NonActiveTile");
            return;
        }
        Debug.Log("MouseDown");
        if (player == null)
        {
            InitializePlayer();
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
     
    public void StepTaken()
    {
        if (state == TileState.NonActive)
        {
            return;
        }

        if(state == TileState.SuperSolid)
        {
            SetActive();
            return;

        }
            StepsTaken++;
            UpdateState();
            SetActive();
        
    }

    public void StepEnded()
    {

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
                currentSprite = SolidSprite;
                StepsTaken = 0; break;
            case TileState.SmallDamage:
                currentSprite = SmallSprite;
                StepsTaken = 1; break;
            case TileState.MidDamage:
                currentSprite = MidSprite;
                StepsTaken = 2; break;
            case TileState.VeryDamaged:
                currentSprite = VerySprite;
               // spriteRenderer.material.color = Color.red;
                spriteRenderer.material.color = new Color(255.0f, 0, 0, 0.1f);

                StepsTaken = 3; break;
            case TileState.MaxDamaged:
                currentSprite = MaxSprite;
                StepsTaken = 4; break;
            case TileState.NonActive:
                currentSprite = NonactiveSprite;
                StepsTaken = 69;
                break;
            case TileState.SuperSolid:
                currentSprite = SuperSolidSprite;
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
                currentSprite = SolidSprite;
                break;

            case 1:
                state = TileState.SmallDamage;
                currentSprite = SmallSprite;
                break;
            case 2:
                state = TileState.MidDamage;
                currentSprite = MidSprite;
                break;
            case 3:
                state = TileState.VeryDamaged;
                currentSprite = VerySprite;
                break;
            case 4:
                state = TileState.MaxDamaged;
                currentSprite = MaxSprite;
                break;
            case 5:
                Destroy(this);
                break;
            default:
                break;
        }


        hp = state == TileState.NonActive ? 0 : 5 - StepsTaken;
        spriteRenderer.sprite = currentSprite;

    }

}


