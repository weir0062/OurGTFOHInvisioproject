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
        camController = cam.GetComponent<CameraController>();
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

                if(camController!=null) { camController = cam.GetComponent<CameraController>(); }
                camController?.CameraFocus(ActiveTile.transform); // меняем фокусировку на новую активную плиту
                ActiveTile.StepTaken(); // делаем шаг на новую плитку
            }
        }

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
        CheckForTouch();
    }
}
