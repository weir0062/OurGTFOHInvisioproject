using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.Examples.TMP_ExampleScript_01;

public class ButtonRefFinder : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToFind;
    void Start()
    {
       SceneHandler objectFound = FindObjectOfType<SceneHandler>();

        Button button = GetComponent<Button>();

        button.onClick.AddListener(objectFound.LoadNextLevel);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
