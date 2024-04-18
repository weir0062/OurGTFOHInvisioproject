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
    // Start is called before the first frame update
    void Start()
    {

        cam = GameObject.FindObjectOfType<Camera>();
        camController=cam.GetComponent<CameraController>();
    }



    public void TakeStep(Tile Newtile)
    {
        if (ActiveTile != null)
        {
            if (CheckDistanceToTile(ActiveTile, Newtile))
            {


                LastActiveTile = ActiveTile; // ставим ту которая активна сейчас в позицию предыдущей плитки
                LastActiveTile?.StepEnded(); // заканчиваем шаг на старой, если старая была. знак "?" после названия переменной проверяет работает ли она
                ActiveTile = Newtile; // меняем нынешнюю активную плитку( которая уже в старой) на новую плитку, на которую переходим
                tileController.SetActiveTile(ActiveTile); // Даем контроллеру понять что новая плитка активна
                Vector2 tilepos = Newtile.GetPosition(); // получаем позицию плитки на карте (кастомную позицию плитки, а не позицию по Юнити)
                camController?.CameraFocus(ActiveTile.transform); // меняем фокусировку на новую активную плиту
                ActiveTile.StepTaken(); // делаем шаг на новую плитку
                tileController.SetRedTiles(); // делаем плитки рядом с плохими красными
            }
        }

    }

    bool CheckDistanceToTile(Tile ActiveTile, Tile NewTile)
    {

        Vector2 activeTilePos = ActiveTile.GetPosition();
        Vector2 newTilePos = NewTile.GetPosition();
        Vector2 Distance = activeTilePos - newTilePos;
        float DistX = Distance.x > 0 ? Distance.x : Distance.x*-1;
        float DistY = Distance.y > 0 ? Distance.y : Distance.y*-1;


        return (DistX < 1.2 && DistY < 1.2);

    }
    void CheckForTouch()
    {
        if (cam == null)
        {
            Debug.LogError("Camera is not assigned or found.");
            return; // Early exit if camera is not found
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 screenPoint = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;

            // Convert the screen point to a world point on the plane of the tiles
            float distanceToTiles = Mathf.Abs(cam.transform.position.z - 0); // Assuming tiles are at z = 0
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, distanceToTiles));

            // Perform the raycast from the world point downwards in the z direction (or whichever direction is appropriate)
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero);
            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null)
                {
                    Debug.Log($"Tile hit: {tile.name} at position {hit.point}"); // Confirm hit in the console
                    if (!tile.IsActive)
                    {
                        tile.Pressed(); // Ensure this function is implemented in the Tile class
                      

                    }
                    else
                    {
                        Debug.Log($"Hit object is not a Tile. Hit: {hit.collider.gameObject.name}");
                    }
                }
                else
                {
                    Debug.Log("No object hit by ray.");
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
            CheckForTouch();
        }
    }
