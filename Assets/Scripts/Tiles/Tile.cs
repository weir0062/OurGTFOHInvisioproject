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
    MaxDamaged
};
public class Tile : MonoBehaviour
{
    public TileState state = TileState.Solid;
    public SpriteRenderer spriteRenderer;
    int StepsTaken = 0;

    int hp = 5;
    public TextMeshPro text;

    bool isRed = false;
    Sprite currentSprite;

    public Sprite[] SolidSprite;
    public Sprite[] SmallSprite;
    public Sprite[] MidSprite;
    public Sprite[] VerySprite;
    public Sprite[] MaxSprite;

    public GameObject DefaultSprite;
    public GameObject PositionIndicator;
    public GameObject TextObject;
    public GameObject DangerWarning;

    Player player;
    public bool IsActive = false;
    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tile";
        InitializeDefaults();
        UpdateState();
        TextObject.SetActive(false);
        PositionIndicator.SetActive(false);
        SpriteRenderer indicatorSprite = PositionIndicator.GetComponent<SpriteRenderer>();

        indicatorSprite.material.color = Color.black;

    }

    public void Pressed()
    {
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
    public void SetText(string newtext)
    {
        text.text = newtext;
    }



    public void TurnRed()
    {
        isRed = true;
    }
    public void StepTaken()
    {
        StepsTaken++;
        UpdateState();
        SetText(hp.ToString());
        SetActive();

    }

    public void StepEnded()
    {
        SetNotActive();
    }

    public void SetActive()
    {
        IsActive = true;
        if (isRed)
        {
            DangerWarning.SetActive(true);
        }
        PositionIndicator.SetActive(true);

        DefaultSprite.SetActive(false);
        TextObject.SetActive(true);

    }
    public void SetNotActive()
    {
        IsActive = false;
        PositionIndicator.SetActive(false);

        spriteRenderer.material.color = Color.white;
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
                currentSprite = SolidSprite[Random.Range(0, SolidSprite.Length)];
                StepsTaken = 0; break;
            case TileState.SmallDamage:
                currentSprite = SmallSprite[Random.Range(0, SmallSprite.Length)];
                StepsTaken = 1; break;
            case TileState.MidDamage:
                currentSprite = MidSprite[Random.Range(0, MidSprite.Length)];
                StepsTaken = 2; break;
            case TileState.VeryDamaged:
                currentSprite = VerySprite[Random.Range(0, VerySprite.Length)];
                StepsTaken = 3; break;
            case TileState.MaxDamaged:
                currentSprite = MaxSprite[Random.Range(0, MaxSprite.Length)];
                StepsTaken = 4; break;
        }

        hp = 5 - StepsTaken;
        SetText(hp.ToString());
    }



    void UpdateState()
    {
        switch (StepsTaken)
        {
            case 0:
                state = TileState.Solid;
                currentSprite = SolidSprite[Random.Range(0, SolidSprite.Length)];
                break;

            case 1:
                state = TileState.SmallDamage;
                currentSprite = SmallSprite[Random.Range(0, SmallSprite.Length)];
                break;
            case 2:
                state = TileState.MidDamage;
                currentSprite = MidSprite[Random.Range(0, MidSprite.Length)];
                break;
            case 3:
                state = TileState.VeryDamaged;
                currentSprite = VerySprite[Random.Range(0, VerySprite.Length)];
                break;
            case 4:
                state = TileState.MaxDamaged;
                currentSprite = MaxSprite[Random.Range(0, MaxSprite.Length)];
                break;
            case 5:
                Destroy(this);
                break;
            default:
                break;
        }


        hp = 5 - StepsTaken;
        spriteRenderer.sprite = currentSprite;

    }

}


