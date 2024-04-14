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
    int StepsTaken = 0;

    Material currentMat;

    Material SolidMat;
    Material SmallMat;
    Material MidMat;
    Material VeryMat;
    Material MaxMat;
    // Start is called before the first frame update
    void Start()
    {

        InitializeDefaults();
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



        
    }










}


