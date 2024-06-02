using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] TileController tileController;
    Tile ActiveTile;
    Tile LastActiveTile;
    public Camera cam;
    CameraController camController;
    InGameMenu inGameMenu;
    public GameObject PositionIndicator;



    public float IndicatorOscillationFrequency = 1.69f; // Frequency of the oscillation
    public float IndicatorOscillationAmplitude = 0.169f; // Amplitude of the oscillation
    private Vector3 idlePosition;
    // Start is called before the first frame update
    void Start()
    {

        cam = GameObject.FindObjectOfType<Camera>();
        camController = cam.GetComponent<CameraController>();
    }



    public void TakeStep(Tile Newtile)
    {
        if (ActiveTile != null)
        {
            if (CheckDistanceToTile(ActiveTile, Newtile))
            {


                LastActiveTile = ActiveTile; // Get reference to last active tile 
                LastActiveTile?.StepEnded(); // use "?" to check if last tile is active, and, if so, end the step on it
                ActiveTile = Newtile; // Get reference to new active tile 
                tileController.SetActiveTile(ActiveTile); 
                Vector2 tilepos = Newtile.GetPosition(); // getting our custom grid position for the tile 

                if (camController != null) { camController = cam.GetComponent<CameraController>(); }
                camController?.CameraFocus(ActiveTile.transform); // move camera to new tile
                StartCoroutine(MoveIndicator(ActiveTile, Newtile)); // Indicator movement animation to new tile 
                ActiveTile.StepTaken(); // Take Step To new Tile 



            }
        }

    }


    private IEnumerator MoveIndicator(Tile activeTile, Tile newTile)
    {
       // Vector3 Offset = new Vector3(0.0f, 0.0f, 0.69f*1.69f);
        Vector3 startPos = activeTile.gameObject.transform.position;
        Vector3 endPos = newTile.gameObject.transform.position;// - Offset;
        startPos.y = 5f;
        endPos.y = 5f;
        float duration = 0.69f; // Duration of the LERP in seconds
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);

            // Calculate vertical oscillation
            float oscillation = Mathf.Sin(t * Mathf.PI * 2.0f) * IndicatorOscillationAmplitude;

            // Interpolate position with oscillation
            Vector3 interpolatedPosition = Vector3.Lerp(startPos, endPos, t);
            interpolatedPosition.y += oscillation;

            PositionIndicator.transform.position = interpolatedPosition;
            yield return null;
        }

        // Ensure the final position is set
        PositionIndicator.transform.position = endPos;
        idlePosition = endPos; // Update the idle position
    }

    private void PerformIdleOscillation()
    {
        float t = Time.time * IndicatorOscillationFrequency;
        float oscillation = Mathf.Sin(t) * IndicatorOscillationAmplitude;

        Vector3 oscillatedPosition = idlePosition;
        oscillatedPosition.y += oscillation;

        PositionIndicator.transform.position = oscillatedPosition;
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
            Ray ray = cam.ScreenPointToRay(screenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Tile tile = hit.collider.GetComponentInChildren<Tile>();
                if (tile != null)
                {
                    if (tile.state != TileState.NonActive)
                    {
                        if (!tile.IsActive)
                        {
                            tile.Pressed();
                        }
                    }
                }
            }
        }
    }




    public void SetActiveTile(Tile newActiveTile)
    {
        ActiveTile = newActiveTile;
    }

    void Update()
    {
        PerformIdleOscillation();
        CheckForTouch();
    }
}