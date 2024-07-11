using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    [SerializeField] bool ShouldZoom = true;
    [SerializeField] TileController tileController;
    [SerializeField] Camera camera;
    [HideInInspector] public float MinZoomIn;
    [HideInInspector] public float MaxZoomIn;
    float defaultY = 0;
    public float focusSpeed = 6.9f;
    private Transform oldFocusObject;
    public Transform currentFocusObject;
    private Vector3 CameraOffset;
    bool IsPaused;
    float CurrentZoom;
    // Start is called before the first frame update
    void Awake()
    {
        InitializeLevel();
        SetIsPaused(false);
        IsPaused = false;
        MinZoomIn = 10;
        MaxZoomIn = 90;
        CurrentZoom = (MinZoomIn + MaxZoomIn) / 2;
        CameraZoom((MinZoomIn + MaxZoomIn)/2);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeLevel();
    }
    public void InitializeLevel()
    {
        InitializeTileController();
        InitilizeCamera();
    }
    void InitilizeCamera()
    {

        if (tileController == null)
        {
            tileController = GameObject.FindObjectOfType<TileController>();


            Debug.Log(tileController);

        }
        if (camera == null)
        {
            camera = GameObject.FindObjectOfType<Camera>();
            Debug.Log(camera);

            CameraFocus(tileController.GetActiveTile().transform);
        }

        if (camera != null && camera.orthographic == true)
        {
            camera.orthographic = false;

        }
        

        defaultY = transform.position.y;
    }


    void InitializeTileController()
    {

        List<GameObject> tileObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        tileController.InitializeTileArray(tileObjects);
    }
    // Update is called once per frame
    void Update()
    {
        if (camera == null || tileController == null)
        {
            InitilizeCamera();

        }
        CameraFocus();

        currentFocusObject = tileController.GetActiveTile().transform;
        tileController.GetActiveTile().Camera = this.gameObject;


        if(Input.mouseScrollDelta.y > 0)
        {
            CurrentZoom -= 5;
            CameraZoom(CurrentZoom);
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            CurrentZoom += 5;
            CameraZoom(CurrentZoom);
        }
    }


    public void SetIsPaused(bool isPaused)
    {
        IsPaused = isPaused;
    }

    public bool GetIsPaused()
    {
        return IsPaused;
    }

    public void CameraZoom(float zoom)
    {
        if (camera)
        {
            CurrentZoom = zoom;
            camera.fieldOfView = zoom;
        }
    }
    public void CameraFocus()
    {
        if (currentFocusObject != null)
        {
            // Calculate the target position. Adjust the Z coordinate as needed
            Vector3 targetPosition = new Vector3(currentFocusObject.position.x, defaultY, currentFocusObject.position.z) - (CameraOffset / 2);

            // Create a velocity vector to store the camera's velocity
            Vector3 velocity = Vector3.zero;

            // Smoothly interpolate the camera's position towards the target position using SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, focusSpeed * Time.deltaTime);



        }
    }
    public void CameraFocus(Transform focusObject)
    {
        oldFocusObject = currentFocusObject;
        currentFocusObject = focusObject;
        CameraOffset = (oldFocusObject.position - currentFocusObject.position).normalized;
        CameraFocus();
    }

     
}
