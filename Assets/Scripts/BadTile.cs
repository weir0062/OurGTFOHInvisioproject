using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTile : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Invisio>().StepOnBadTile();

    }
}
