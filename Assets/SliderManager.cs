using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    Slider MusicSlider;

    public bool Music = true;
    void Start()
    {

        MusicSlider= GetComponent<Slider>();

        if(Music == true)
        {
            MusicManager temp = FindObjectOfType<MusicManager>();
            MusicSlider.onValueChanged.AddListener(temp.OnSliderChanged);

        }
        else
        {
            SoundManager temp = FindObjectOfType<SoundManager>();
            MusicSlider.onValueChanged.AddListener(temp.OnSliderChanged);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
