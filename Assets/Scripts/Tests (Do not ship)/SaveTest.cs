using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class SaveTest : MonoBehaviour, Saveable
{
    // Start is called before the first frame update

    Button ColorButton;

    float r;
    float g;
    float b;
    void Start()
    {
        ColorButton= GetComponent<Button>();
        ColorButton.onClick.AddListener(ChangeColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        // Get the ColorBlock from the button
        ColorBlock colorBlock = ColorButton.colors;

        // Set the color for different states

        r = Random.Range(0.0f, 1.0f);
        g = Random.Range(0.0f, 1.0f);
        b = Random.Range(0.0f, 1.0f);

        Color newColor = new Color(r, g, b);

        colorBlock.normalColor = newColor;
        colorBlock.highlightedColor = newColor * 1.2f; // slightly brighter when highlighted
        colorBlock.pressedColor = newColor * 0.8f; // slightly darker when pressed
        colorBlock.selectedColor = newColor;
        colorBlock.disabledColor = newColor * 0.5f; // slightly transparent when disabled

        // Assign the updated ColorBlock back to the button
        ColorButton.colors = colorBlock;

        
    }

    public void SetColor()
    {
        // Get the ColorBlock from the button
        ColorBlock colorBlock = ColorButton.colors;

        Color newColor = new Color(r, g, b);

        colorBlock.normalColor = newColor;
        colorBlock.highlightedColor = newColor * 1.2f; // slightly brighter when highlighted
        colorBlock.pressedColor = newColor * 0.8f; // slightly darker when pressed
        colorBlock.selectedColor = newColor;
        colorBlock.disabledColor = newColor * 0.5f; // slightly transparent when disabled

        // Assign the updated ColorBlock back to the button
        ColorButton.colors = colorBlock;
    }

    public void OnSave(Stream stream, IFormatter formatter)
    {
        formatter.Serialize(stream, r);
        formatter.Serialize(stream, g);
        formatter.Serialize(stream, b);
        SaveUtils.SerializeObjectRef(stream, formatter, ColorButton.gameObject);

        
    }

    public void OnLoad(Stream stream, IFormatter formatter)
    {
        r = (float)formatter.Deserialize(stream);
        g = (float)formatter.Deserialize(stream);
        b = (float)formatter.Deserialize(stream);
        ColorButton = SaveUtils.DeserializeObjectRef(stream, formatter).GetComponent<Button>();

        SetColor();


    }
}
