using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisio : MonoBehaviour
{
    public GameObject footstepPrefab; // Префаб следа
    private bool isRightFoot = true; // Переключатель между правой и левой ногой
    private int activeFootsteps = 0; // Счетчик активных следов
    int totalfootsteps = 0;
    public float footstepscale = 0.2f;
    private List<GameObject> Footsteps = new List<GameObject>();
    private float LastStepX = 0;


    public void StepOnGoodTile(Vector3 position, GameObject GoodTile)
    {
        if (activeFootsteps <= 2)
        {
            GameObject footstep = Instantiate(footstepPrefab, position, Quaternion.identity);
            isRightFoot = GetIsRightFoot(position.x, LastStepX, isRightFoot);
            LastStepX = position.x;
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
        // Логика проигрыша, например, показ экрана проигрыша
        Debug.Log("Game Over");
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

        Debug.Log(totalfootsteps);
    }

}
