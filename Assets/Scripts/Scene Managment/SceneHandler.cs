using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class SceneHandler : MonoBehaviour//, Saveable
{
    public int LevelID = 0;

    public static SceneHandler Instance;

    //This isn't really needed for anything other than confirming that the game manager persisted between scenes
    public string OwningLevelName;

    public string SaveFileName = "TestSaveGame.sav";

    //Tracks the version of your save games.  You should increment this number whenever you make changes that will
    //effect what gets saved.  This will allow you to detect incompatible save game versions early, instead of getting
    //weird hard to track down bugs.
    const int SaveGameVersionNum = 1;

    bool TransferingScenes = false;


    void Awake()
    {
        //This is similar to a singleton in that it only allows one instance to exist and there is instant global 
        //access to the LevelManager using the static Instance member.
        //
        //This will set the instance to this object if it is the first time it is created.  Otherwise it will delete 
        //itself.
        if (Instance == null)
        {
            //This tells unity not to delete the object when you load another scene
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save(SaveFileName);
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            Load(SaveFileName);
        }
    }

    public void LoadNextLevel()
    {
        LevelID++;
        SceneManager.LoadScene(LevelID);

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(LevelID);
    }

    public void LoadLevelAt(int level)
    {
        LevelID = level;
        //SceneManager.LoadScene(level);
        SceneManager.LoadSceneAsync(level);
    }

    public void GoBackALevel()
    {
        LevelID--;
        SceneManager.LoadScene(LevelID);
    }

    //public void OnSave(Stream stream, IFormatter formatter)
    //{
    //    formatter.Serialize(stream, LevelID);
    //}

    //public void OnLoad(Stream stream, IFormatter formatter)
    //{
    //    LevelID = (int)formatter.Deserialize(stream);

    //    LoadLevelAt(LevelID);

    //}

    //This will search for a game object based on a save id.  This might end up being slow if 
    //there are a lot of objects in a scene.
    public GameObject GetGameObjectBySaveId(string saveId)
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject searchObj in gameObjects)
        {
            SaveHandler saveHandler = searchObj.GetComponent<SaveHandler>();
            if (saveHandler == null)
            {
                continue;
            }

            if (saveHandler.SaveId == saveId)
            {
                return searchObj;
            }
        }

        return null;
    }


    void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 3000, 40), "Fade In: F2, Fade Out: F3, Save Game: F5, Load Game: F9");
    }



    //Use this to save your game.  Make sure that the order that you serialize things is the same as the order that
    //you deserialize things
    public void Save(string fileName)
    {
        string savePath = GetSaveFilePath(fileName);

        DebugUtils.Log("Saving Game to file: '{0}'", savePath);

        //Serializes and deserializes an object, or an entire graph of connected objects, in binary format
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //Creates or overwrites a file in the specified path
        FileStream file = File.Create(savePath);

        //Save Version number
        binaryFormatter.Serialize(file, SaveGameVersionNum);

        //Save Level 
        binaryFormatter.Serialize(file, LevelID);


        //Save the objects
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();

        //Need to save out how many objects are saved so we know how how many to loop over
        //when we load
        List<SaveHandler> objectsToSave = new List<SaveHandler>();
        foreach (GameObject gameObjectToSave in gameObjects)
        {
            //Get the save handler on the object if there is one
            SaveHandler saveHandler = gameObjectToSave.GetComponent<SaveHandler>();
            if (saveHandler == null)
            {
                continue;
            }

            //Checking if this object can and should be saved
            if (!saveHandler.AllowSave)
            {
                continue;
            }

            objectsToSave.Add(saveHandler);
        }

        binaryFormatter.Serialize(file, objectsToSave.Count);

        //Save the game objects stage 1.  Saving out all of the data needed to recreate
        //the objects
        foreach (SaveHandler saveHandler in objectsToSave)
        {
            saveHandler.SaveObject(file, binaryFormatter);
        }

        //Save the game objects stage 2.  Saving the rest of the data for the objects Scripts
        foreach (SaveHandler saveHandler in objectsToSave)
        {
            saveHandler.SaveData(file, binaryFormatter);
        }

        //Clean up
        file.Close();
    }

    //Use this to load your game.  Make sure that the order that you deserialize things is the same as the order that
    //you serialize things
    public void Load(string fileName)
    {

        //Get and verify the save path
        string savePath = GetSaveFilePath(fileName);

        DebugUtils.Log("Loading Game from file: {0}...", savePath);

        if (!File.Exists(savePath))
        {
            DebugUtils.Log("LoadFile doesn't exist: {0}", savePath);
            return;
        }

        m_LoadGameFormatter = new BinaryFormatter();
        m_LoadGameStream = File.Open(savePath, FileMode.Open);

        //Version number
        int versionNumber = (int)m_LoadGameFormatter.Deserialize(m_LoadGameStream);
        DebugUtils.Log("Load file version number: {0}", versionNumber);

        //Level ID
        LevelID = (int)m_LoadGameFormatter.Deserialize(m_LoadGameStream);
        DebugUtils.Log("Level Id: {0}", LevelID);

        TransferingScenes = true;

        LoadLevelAt(LevelID);


    }

    //A helper function to create the save path.  This uses the persistentDataPath, which will be a safe place
    //to store data on a user's machine without errors.
    string GetSaveFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    //This callback gets called when a scene is done loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(TransferingScenes == true)
        {
            //This section will finish loading the save game.  We need to load the objects here since

            //Get number of objects to load  //continue loading from stream of open file
            int numObjectsToLoad = (int)m_LoadGameFormatter.Deserialize(m_LoadGameStream);

            //Load objects Stage 1.  The objects are loaded in two stages so that
            //by the time the second stage is running all of the objects will exist which
            //will make reconstructing relationships between everything easier
            List<GameObject> gameObjectsLoaded = new List<GameObject>();
            for (int i = 0; i < numObjectsToLoad; ++i)
            {
                GameObject loadedObject = SaveHandler.LoadObject(m_LoadGameStream, m_LoadGameFormatter);

                if (loadedObject != null)
                {
                    gameObjectsLoaded.Add(loadedObject);
                }
            }

            //Load objects Stage 2
            for (int i = 0; i < numObjectsToLoad; ++i)
            {
                GameObject loadedObject = gameObjectsLoaded[i];

                SaveHandler saveHandler = loadedObject.GetComponent<SaveHandler>();

                saveHandler.LoadData(m_LoadGameStream, m_LoadGameFormatter);
            }

            //Clean up
            m_LoadGameStream.Close();
            m_LoadGameStream = null;
            m_LoadGameFormatter = null;


        }
        TransferingScenes = false;
    }

    BinaryFormatter m_LoadGameFormatter;
    FileStream m_LoadGameStream;


}
