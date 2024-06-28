using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;

    public List<AudioClip> Songs;

    AudioSource audioSorce;

    SceneHandler sceneHandler;

    [Header("Music Settings")]
    [SerializeField] float Volume;
    [SerializeField] bool PlayOnAwake;
    [SerializeField] bool Loop;

    // Start is called before the first frame update
    void Start()
    {
        ////This should be used at the end to ensure we dont have more music than levels. However first we need the levels
        //if(Songs.Count > SceneManager.sceneCount) 
        //{
        //    throw new Exception("More Songs Than Scenes");
        //}


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

        if (Songs.Count <= 0 )
            throw new Exception("No Songs in the List");


        audioSorce = GetComponent<AudioSource>();
        sceneHandler = GameObject.FindObjectOfType<SceneHandler>();

        if(audioSorce == null)
            throw new Exception("Could not find Audio Scorce");

        audioSorce.playOnAwake = PlayOnAwake;
        audioSorce.loop = Loop;
        audioSorce.volume = Volume;



        if (sceneHandler == null)
            throw new Exception("Could not find Scene Handler");
        

        SceneManager.sceneLoaded += PlayMusic;
        audioSorce.clip = Songs[sceneHandler.LevelID];



        if(PlayOnAwake == true)
        {
            audioSorce.Play();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if(audioSorce.isPlaying == true) 
        {
            PlayOnAwake= false;
        }
    }

    //gets called when a scene is loaded, however if there is an add initilizer it wont go off until the ad is complete=
    public void PlayMusic(Scene scene, LoadSceneMode mode)
    {
        audioSorce.Stop();

        if (GameObject.FindObjectOfType<AdsInitializer>() == null)
        {
            audioSorce.volume = Volume;
            audioSorce.clip = Songs[sceneHandler.LevelID];
            audioSorce.Play();
        }

        if (sceneHandler.LevelID == 0 || sceneHandler.LevelID == 1)
        {
            audioSorce.volume = Volume;
            audioSorce.clip = Songs[sceneHandler.LevelID];
            audioSorce.Play();
        }
    }

    //Used for levels that start with an add like level 2 that way the music does not play over the ad
    public void PlayMusicAfterAd()
    {
        audioSorce.Stop();
        audioSorce.volume = Volume;
        audioSorce.clip = Songs[sceneHandler.LevelID];
        audioSorce.Play();
    }

}
