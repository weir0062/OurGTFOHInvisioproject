using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileController : MonoBehaviour
{
    public Tile[,] tiles;


    Tile ActiveTile;
    Tile CentralTile;

    Player player;
    void Start()
    {

    }

    public void InitializeTileArray(List<GameObject> tileObjects)
    {
        if (tileObjects.Count == 0) return;

        var xCoordinates = new HashSet<float>();
        var yCoordinates = new HashSet<float>();

        float gridSize = 1.0f; // Предполагаемый размер тайла / сетки

        // Сбор и округление уникальных координат
        foreach (GameObject obj in tileObjects)
        {
            Vector3 pos = obj.transform.position;

            float roundedX = Mathf.Round(pos.x / gridSize) * gridSize;
            float roundedY = Mathf.Round(pos.y / gridSize) * gridSize;

            xCoordinates.Add(roundedX);
            yCoordinates.Add(roundedY);
        }

        var sortedX = xCoordinates.OrderBy(x => x).ToList();
        var sortedY = yCoordinates.OrderBy(y => y).ToList();

        // Создание словарей для маппинга координат в индексы
        Dictionary<float, int> xMap = sortedX.Select((value, index) => new { value, index })
                                             .ToDictionary(pair => pair.value, pair => pair.index);
        Dictionary<float, int> yMap = sortedY.Select((value, index) => new { value, index })
                                             .ToDictionary(pair => pair.value, pair => pair.index);

        tiles = new Tile[sortedX.Count, sortedY.Count];

        foreach (GameObject obj in tileObjects)
        {
            Tile tile = obj.GetComponent<Tile>();
            if (tile != null)
            {
                Vector3 pos = obj.transform.position;
                // Округляем координаты при запросе к словарю
                float roundedX = Mathf.Round(pos.x / gridSize) * gridSize;
                float roundedY = Mathf.Round(pos.y / gridSize) * gridSize;

                // Используем округленные координаты для получения индексов
                int xIndex = xMap[roundedX];
                int yIndex = yMap[roundedY];

                tile.SetPosition(xIndex, yIndex);
                tiles[xIndex, yIndex] = tile;
            }
        }
        ActiveTile = tiles[0, 0];
        tiles[0, 0].SetActive();
        CentralTile = tiles[xCoordinates.Count / 2, yCoordinates.Count / 2];
        InitializePlayer();
    }

    public void SetRedTiles()
    {
        //for (int i = 1; i < tiles.GetLength(0)-1; i++)
        //{
        //    for (int j = 1; j < tiles.GetLength(1)-1; j++)
        //    {

        //        if (tiles[i - 1, j].GetStepsTaken() >= 4)
        //        {
        //            tiles[i, j].TurnRed();
        //            tiles[i - 1, j].TurnRed();

        //        } 
        //        if (tiles[i + 1, j].GetStepsTaken() >= 4)
        //        {
        //            tiles[i, j].TurnRed();
        //            tiles[i + 1, j].TurnRed();

        //        }
        //        if (tiles[i, j + 1].GetStepsTaken() >= 4)
        //        {
        //            tiles[i, j].TurnRed();
        //            tiles[i, j + 1].TurnRed();

        //        }
        //        if (tiles[i, j - 1].GetStepsTaken() >= 4)
        //        {
        //            tiles[i, j].TurnRed();
        //            tiles[i, j - 1].TurnRed();

        //        }
        //    }
        //}
    }

    void InitializePlayer()
    {

        player = GameObject.FindObjectOfType<Player>();

        if (player == null)
        {
            InitializePlayer();
        }
        player.SetActiveTile(ActiveTile);
        player.TakeStep(ActiveTile);
    }

    public void SetActiveTile(Tile tile)
    {
        ActiveTile = tile;
    }
    public Tile GetActiveTile()
    {
        if (ActiveTile)
        {
            return ActiveTile;
        }
        Debug.Log("TileNotActive");
        return null;
    }
    public Tile GetCentralTile()
    {
        if (CentralTile)
        {
            return CentralTile;
        }
        Debug.Log("NoCentralTileFound");
        return null;
    }
}