using System.Collections;
using System.Collections.Generic;
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

    bool IsSteppedOn = false;

    Sprite currentSprite;

    public Sprite SolidSprite;
    public Sprite SmallSprite;
    public Sprite MidSprite;
    public Sprite VerySprite;
    public Sprite MaxSprite;
    // Start is called before the first frame update
    void Start()
    {

        InitializeDefaults();
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {

    }





    public void StepTaken()
    {
        StepsTaken++;
        UpdateState();
    }

    public void StepEnded()
    {
    }



    void InitializeDefaults()
    {
        switch (state)
        {
            case TileState.Solid:
                StepsTaken = 0; break;
            case TileState.SmallDamage:
                StepsTaken = 1; break;
            case TileState.MidDamage:
                StepsTaken = 2; break;
            case TileState.VeryDamaged:
                StepsTaken = 3; break;
            case TileState.MaxDamaged:
                StepsTaken = 4; break;
        }
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

        spriteRenderer.sprite = currentSprite;

    }










}


