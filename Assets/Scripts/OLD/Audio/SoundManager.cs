using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource m_AudioSource;
    public static SoundManager Instance;



    [SerializeField] private AudioClip StepSound;
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip CoinSound;


    [Header("Sound Settings")]
    [SerializeField] float Volume = 100;

    [SerializeField] Slider SoundSlider;



    void Start()
    {
        if (Instance == null)
        {
            //This tells unity not to delete the object when you load another scene
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += Loaded;

        m_AudioSource = GetComponent<AudioSource>();

        if (m_AudioSource == null)
            throw new Exception("Could not find Audio Scorce");

        m_AudioSource.playOnAwake = false;
        m_AudioSource.loop = false;
        m_AudioSource.volume = Volume;

        if (StepSound == null)
            throw new Exception("Could not find Step Sound");

        if (ClickSound == null)
            throw new Exception("Could not find Click Sound");

        if (CoinSound == null)
            throw new Exception("Could not find Coin Sound");


        if (SoundSlider == null)
            SoundSlider = GameObject.Find("SliderMusic").GetComponent<Slider>();

        if (SoundSlider != null)
            SoundSlider.normalizedValue = Volume;


    }

    public void OnSliderChanged(float value)
    {
        Volume = value;
        m_AudioSource.volume = Volume;
    }


    public void PlayStepSound()
    {
        m_AudioSource.volume = Volume;
        m_AudioSource.Stop();
        m_AudioSource.clip= StepSound;
        m_AudioSource.Play();
    }

    public void PlayClickSound()
    {
        m_AudioSource.volume = Volume;
        m_AudioSource.Stop();
        m_AudioSource.clip = ClickSound;
        m_AudioSource.Play();
    }

    public void PlayCoinSound()
    {
        m_AudioSource.volume = Volume;
        m_AudioSource.Stop();
        m_AudioSource.clip = CoinSound;
        m_AudioSource.Play();
    }

    public void CheckForMainMenu()
    {
        StartCoroutine(WaitAndFind(0.3f));
    }

    public IEnumerator WaitAndFind(float waitTime)
    {
        // Print message before waiting
        Debug.Log("Waiting for " + waitTime + " seconds...");

        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Print message after waiting
        Debug.Log("Waited for " + waitTime + " seconds, now continuing...");

        // Continue with the rest of your code here
        // ...


        ReferenceHolder obj = FindObjectOfType<ReferenceHolder>();

        if (obj != null)
        {
            Slider temp = obj.SoundSliderRef;
            if (temp != null)
            {
                temp.value = Volume;
            }

        }



    }

    public void Loaded(Scene scene, LoadSceneMode mode)
    {
        CheckForMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
