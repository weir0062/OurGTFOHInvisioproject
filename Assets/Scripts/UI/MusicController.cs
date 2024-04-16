using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{





    /*
    Clips
     */





    public AudioClip MenuMusicClip;
    public AudioClip MenuSFXClip;
    public AudioClip GameMusicClip;
    public AudioClip GameSFXClip;









    /*
     */

    public static MusicController instance;
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public GameObject MusicSlider;
    public GameObject SFXSlider;
    Slider MusicvolumeSlider;
    Slider SFXvolumeSlider;

    // Start is called before the first frame update


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
            UpdateSettings(); // Initialize settings from the scene.
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy if another instance is already there.
            return;
        }
    }


    void Start()
    {
        UpdateSettings();




        LoadSettings();
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = MusicvolumeSlider.value;
    }
    public void ChangeSFXVolume()
    {
        SFXSource.volume = SFXvolumeSlider.value;
    }


    public void UpdateSettings()
    {
        /*  GameObject MusicAudioSource= GameObject.Find("Music");
         musicSource = MusicAudioSource.GetComponent<AudioSource>();
         MusicvolumeSlider = MusicAudioSource.GetComponent<Slider>(); 
         GameObject SFXAudioSource = GameObject.Find("Clicks");
         SFXSource = SFXAudioSource.GetComponent<AudioSource>();
         SFXvolumeSlider = SFXAudioSource.GetComponent<Slider>();
         SFXSource.loop = false;*/


        //music
        MusicvolumeSlider = MusicSlider.GetComponent<Slider>();
        MusicvolumeSlider.value = 50;
        musicSource.volume = MusicvolumeSlider.value;

        //sfx
        SFXvolumeSlider = SFXSlider.GetComponent<Slider>();
        SFXvolumeSlider.value = 50;
        SFXSource.volume = SFXvolumeSlider.value;
    }
    public void PlaySFXSound()
    {

        if (SFXSource == null || SFXSource.clip == null)
        {
            SFXSource = GameObject.Find("Clicks").GetComponent<AudioSource>();
            SFXSource.clip = SceneManager.GetActiveScene().name == "MainMenu" ? MenuSFXClip : GameSFXClip;
        }

        SFXSource.Play();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
        PlayerPrefs.SetFloat("SFXVolume", SFXSource.volume);
        PlayerPrefs.Save(); // Don't forget to call Save to write to disk
    }

    public void LoadSettings()
    {

        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 50); // 50 is the default value
        SFXSource.volume = PlayerPrefs.GetFloat("SFXVolume", 50); // 50 is the default value
        MusicvolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 50); // 50 is the default value
        SFXvolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 50); // 50 is the default value
    }

    public void SetMusicClip(AudioClip clip)
    {
        if (musicSource)
        {

            musicSource.Stop();
        }
        if (musicSource == null || musicSource.clip == null)
        {
            musicSource = GameObject.Find("Music").GetComponent<AudioSource>();

        }
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SetSFXClip(AudioClip clip)
    {
        if (SFXSource == null || SFXSource.clip == null)
        {
            SFXSource = GameObject.Find("Clicks").GetComponent<AudioSource>();

        }
        SFXSource.clip = clip;
        SFXSource.loop = false;
    }

}