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

        float gridSize = 1.0f;  

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
        CentralTile = tiles[xCoordinates.Count / 2, yCoordinates.Count / 2];
        InitializePlayer();
        SetRedTiles();
        tiles[0, 0].StepTaken();
    }


    public void SetRedTiles()
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Check each direction safely
                if ((x > 0 && tiles[x - 1, y].GetStepsTaken() >= 4) || // Left
                    (x < width - 1 && tiles[x + 1, y].GetStepsTaken() >= 4) || // Right
                    (y < height - 1 && tiles[x, y + 1].GetStepsTaken() >= 4) || // Up
                    (y > 0 && tiles[x, y - 1].GetStepsTaken() >= 4)) // Down
                {
                    tiles[x, y].TurnRed();
                }
            }
        }
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