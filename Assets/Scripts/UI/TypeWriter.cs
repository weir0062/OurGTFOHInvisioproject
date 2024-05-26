using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public string fullText;
    public float delay = 0.1f;

    private string currentText = "";

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
