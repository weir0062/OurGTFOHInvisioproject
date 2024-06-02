using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] bool ShouldZoom = true;
    [SerializeField] float ZoomSpeed = 1f;
    [SerializeField] float ZoomSensitivity = 6.9f;
    [SerializeField] TileController tileController;
    [SerializeField] Camera camera;
    float MinZoomIn = 69/2;
    float MidZoomIn = 0; 
    float focusSpeed = 6.9f*1.5f;
    private Transform oldFocusObject;
    private Transform currentFocusObject;
    private Vector3 CameraOffset;
    float MaxZoomIn = 0;
    // Start is called before the first frame update
    void Start()
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
        }

        if (camera != null && camera.orthographic == true)
        {
            camera.orthographic = false;
            
        }
        if(tileController)
        {

        }
        CalculateMaxZoom();
    }


    void InitializeTileController()
    {

        List<GameObject> tileObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        tileController.InitializeTileArray(tileObjects);
    }
    // Update is called once per frame
    void Update()
    {
        if(camera == null || tileController == null)
        {
            InitilizeCamera();

        }
            CameraFocus();

        if (camera.fieldOfView < MidZoomIn)
        {
            currentFocusObject = tileController.GetActiveTile().transform;
            CameraFocus();
        }
        else
        {
            currentFocusObject = tileController.GetCentralTile().transform;

        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (camera)
        {
            camera.fieldOfView -= scroll * ZoomSensitivity;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, MinZoomIn, MaxZoomIn);
        }
    }


    public void CameraFocus()
    {
        if (currentFocusObject != null)
        {
            // Calculate the target position. Adjust the Z coordinate as needed
            Vector3 targetPosition = new Vector3(currentFocusObject.position.x, transform.position.y, currentFocusObject.position.z-2f) - (CameraOffset * 6.9f / 3);

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

    void CalculateMaxZoom()
    {
        // Assume tiles are 1 unit in size, adjust if they are different
        float tileSize = 1.5f;
        if (tileController.tiles.GetLength(1) > 0)
        {
            
        float boardWidth = tileController.tiles.GetLength(0) * tileSize;
        float boardHeight = tileController.tiles.GetLength(1) * tileSize;

        // Calculate the vertical size needed to fit the board
        float verticalSize = boardHeight / 2.0f;
        float horizontalSize = boardWidth / (2.0f * camera.aspect);

        // Set the max zoom to the larger of the two sizes, plus a little margin
        MaxZoomIn = Mathf.Max(verticalSize, horizontalSize) * 1.69f; // 1.1f adds a 69% margin
        MaxZoomIn = VerticalSizeToFOV(MaxZoomIn * 2, camera.transform.position.z - boardHeight / 2.0f);
        MaxZoomIn *= MaxZoomIn < 0 ? -2 : 2;


            MidZoomIn = MaxZoomIn * 0.8f;
        }

    }
     
    float VerticalSizeToFOV(float size, float distance)
    {
        // Calculate the FOV needed to fit the size at the given distance
        return 2.0f * Mathf.Atan(size / (2.0f * distance)) * Mathf.Rad2Deg;
    }
}
