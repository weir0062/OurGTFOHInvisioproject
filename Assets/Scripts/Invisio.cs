using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invisio : MonoBehaviour
{
    public GameObject footstepPrefab; 
    private bool isRightFoot = true;  
    private int activeFootsteps = 0;  
    int totalfootsteps = 0;
    public float footstepscale = 0.2f;
    private List<GameObject> Footsteps = new List<GameObject>();
    private float LastStepX = 0;


    public void StepOnGoodTile(Vector3 position, GameObject GoodTile)
    {

        Vector3 GoodTilePos = GoodTile.transform.position;
        if (activeFootsteps <= 2)
        {
            for(int i = 0; i < Footsteps.Count; i++)
            {
                if (Footsteps[i].transform.position.y >= GoodTilePos.y)
                {
                    Debug.Log("Previous Footstep - " + Footsteps[i].transform.position.y + "\n while this footstep is - " + GoodTile.transform.position.y);
                    return;
                }
                if (GoodTile.transform.position.y > Footsteps[i].transform.position.y+3.0f )
                {
                    Debug.Log("Too Far");
                    return;
                }
            }
             
            GameObject footstep = Instantiate(footstepPrefab, GoodTilePos, Quaternion.identity);
            isRightFoot = GetIsRightFoot(position.x, LastStepX, isRightFoot);
            LastStepX = GoodTilePos.x;
            footstep.transform.localScale = isRightFoot ? new Vector3(footstepscale, footstepscale, footstepscale) : new Vector3(-footstepscale, footstepscale, footstepscale);
            footstep.transform.SetParent(GoodTile.transform, true);
            Footsteps.Add(footstep);
            activeFootsteps++;
            totalfootsteps++;
            
        }
        if (activeFootsteps > 2)
        {
            RemoveOldestFootstep();
        }
    }

    void RemoveOldestFootstep()
    {
        if (Footsteps.Count > 0)
        {
            GameObject oldestFootstep = Footsteps[0];
            Footsteps.RemoveAt(0);
            Destroy(oldestFootstep);
            activeFootsteps--;
        }
    }
    public void StepOnBadTile()
    {
        Debug.Log("Game Over");

        Death();

    }

    private void Death()
    {
        Debug.Log("Dead");
        SceneManager.LoadScene("MainMenu");
    }

    private bool GetIsRightFoot(float thisStep, float LastStep, bool isRightFoot)
    {
        bool result = false;

        if ((thisStep > LastStep) && isRightFoot)
        {
            return thisStep > 0 ? true : false;
        }
        if ((thisStep > LastStep) && isRightFoot == false)
        {
            return true;
        }
        if ((thisStep < LastStep) && isRightFoot)
        {
            return false;
        }
        if ((thisStep < LastStep) && isRightFoot == false)
        {
            return thisStep < 0 ? false : true;
        }

        return result;
    }
    private void Update()
    {


        if (activeFootsteps > 0)
        {

        for(int i = 0; i < Footsteps.Count; i++)
        {
            if (Footsteps[i].transform.position.y < -6.0f)
            {
                Death();
            }
        }
        }
    }

}
