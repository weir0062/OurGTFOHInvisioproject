using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTile : MonoBehaviour
{
    private void OnMouseDown()
    {
        Vector3 clickPosition = Input.mousePosition;
        clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);  
        clickPosition.z = 0;  
        FindObjectOfType<Invisio>().StepOnGoodTile(clickPosition, this.gameObject);
    }
}
