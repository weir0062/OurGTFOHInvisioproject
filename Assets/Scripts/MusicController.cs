using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

    public AudioSource musicSource;
    public GameObject Slider;
    Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        volumeSlider = Slider.GetComponent<Slider>();

        volumeSlider.value = 50;
        musicSource.volume = volumeSlider.value;
    }

    public void ChangeVolume()
    {
         musicSource.volume = volumeSlider.value;
    }
      
}
