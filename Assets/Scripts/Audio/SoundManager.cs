using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    }

    public void PlayStepSound()
    {
        m_AudioSource.Stop();
        m_AudioSource.clip= StepSound;
        m_AudioSource.Play();
    }

    public void PlayClickSound()
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = ClickSound;
        m_AudioSource.Play();
    }

    public void PlayCoinSound()
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = CoinSound;
        m_AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
