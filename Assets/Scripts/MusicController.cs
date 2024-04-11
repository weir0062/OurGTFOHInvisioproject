using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource SFXSource;
    public GameObject MusicSlider;
    public GameObject SFXSlider;
    public AudioClip SFXClip;
    Slider MusicvolumeSlider;
    Slider SFXvolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        //music
        MusicvolumeSlider = MusicSlider.GetComponent<Slider>();
        MusicvolumeSlider.value = 50;
        musicSource.volume = MusicvolumeSlider.value;

        //sfx
        SFXvolumeSlider = SFXSlider.GetComponent<Slider>();
        SFXvolumeSlider.value = 50;
        SFXSource.clip = SFXClip;
        SFXSource.volume = SFXvolumeSlider.value;

        SFXSource.loop = false;
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = MusicvolumeSlider.value;
    }
    public void ChangeSFXVolume()
    {
        SFXSource.volume = SFXvolumeSlider.value;
    }

    public void PlaySFXSound()
    {
        SFXSource.Play();
    }
}
