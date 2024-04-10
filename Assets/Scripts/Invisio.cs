using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisio : MonoBehaviour
{
    public GameObject footstepPrefab; // ������ �����
    private bool isRightFoot = true; // ������������� ����� ������ � ����� �����
    private int activeFootsteps = 0; // ������� �������� ������
    public float footstepscale = 0.2f;
    private List<GameObject> Footsteps = new List<GameObject>();



    public void StepOnGoodTile(Vector3 position, GameObject GoodTile)
    {
        if (activeFootsteps <= 2)  
        {
            GameObject footstep = Instantiate(footstepPrefab, position, Quaternion.identity);
            isRightFoot = position.x > 0 ? true : false;
            footstep.transform.localScale = isRightFoot ? new Vector3(footstepscale, footstepscale, footstepscale) : new Vector3(-footstepscale, footstepscale, footstepscale);
            footstep.transform.SetParent(GoodTile.transform, true);
            Footsteps.Add(footstep);
            activeFootsteps++;
            
        }
        if (activeFootsteps >2)
        {
            Footsteps[0].SetActive(false);
            Destroy(Footsteps[0]);
            Footsteps.RemoveAt(0);
            activeFootsteps--;
        }
    }

    public void StepOnBadTile()
    {
        // ������ ���������, ��������, ����� ������ ���������
        Debug.Log("Game Over");
    }
     

    private void Update()
    {
    }
}
