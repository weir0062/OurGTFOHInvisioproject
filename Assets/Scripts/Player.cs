using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField] TileController tileController;
    Tile ActiveTile;
    Tile LastActiveTile;
    public Camera cam;
    CameraController camController;
    public GameObject Indicator;
    Vector3 IndicatorPosition;
    public SoundManager soundManager;
    
     
    // Start is called before the first frame update
    void Start()
    {

        cam = GameObject.FindObjectOfType<Camera>();
        camController = cam.GetComponent<CameraController>();

        if(soundManager == null)
            soundManager = FindObjectOfType<SoundManager>();
    }



    public void TakeStep(Tile Newtile)
    {
        if(camController?.GetIsPaused() == false || Newtile.state == TileState.Start)
        {

        if (ActiveTile != null)
        {
            if (CheckDistanceToTile(ActiveTile, Newtile) )
            {


                LastActiveTile = ActiveTile; // Get reference to last active tile 
                LastActiveTile?.StepEnded(); // use "?" to check if last tile is active, and, if so, end the step on it
                ActiveTile = Newtile; // Get reference to new active tile 
                tileController.SetActiveTile(ActiveTile); 
                Vector2 tilepos = Newtile.GetPosition(); // getting our custom grid position for the tile 

                if (camController != null) { camController = cam.GetComponent<CameraController>(); }
                camController?.CameraFocus(ActiveTile.transform); // move camera to new tile
                MoveIndicator(ActiveTile); // Indicator movement animation to new tile 
                ActiveTile.StepTaken(); // Take Step To new Tile 



            }
        }

        }
    }


    void MoveIndicator(Tile newActiveTile)
    {
        IndicatorPosition = newActiveTile.gameObject.transform.position + new Vector3(0, 0f, 0.1f);
    }

    

    bool CheckDistanceToTile(Tile ActiveTile, Tile NewTile)
    {

        Vector2 activeTilePos = ActiveTile.GetPosition();
        Vector2 newTilePos = NewTile.GetPosition();
        Vector2 Distance = activeTilePos - newTilePos;
        float DistX = Distance.x > 0 ? Distance.x : Distance.x * -1;
        float DistY = Distance.y > 0 ? Distance.y : Distance.y * -1;


        return (DistX < 1.2 && DistY < 1.2);

    }
    void CheckForTouch()
    {
        if (cam == null)
        {
            Debug.LogError("Camera is not assigned or found.");
            return;
        }



        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 screenPoint = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;

            if (IsPointerOverUIElement(screenPoint))
            {
                return;
            }

            Ray ray = cam.ScreenPointToRay(screenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    return;
                }
                Tile tile = hit.collider.GetComponentInChildren<Tile>();
                if (tile != null)
                {
                    if (tile.state != TileState.NonActive)
                    {
                        if (!tile.IsActive)
                        {
                            tile.Pressed();
                            soundManager?.PlayStepSound();
                        }
                    }
                }
            }
        }
    }

    private bool IsPointerOverUIElement(Vector3 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void SetActiveTile(Tile newActiveTile)
    {
        ActiveTile = newActiveTile;
    }

    void Update()
    { 
        CheckForTouch();

        Indicator.transform.position = IndicatorPosition;
    }
}