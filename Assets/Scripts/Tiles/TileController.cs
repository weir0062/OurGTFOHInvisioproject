using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class TileController : MonoBehaviour
{
    public Tile[,] tiles;

    Tile ActiveTile;
    Tile CentralTile;
    Tile StartingTile;
    Player player;

    void Start()
    {
        // Инициализация перенесена в другую функцию
    }

    private void Update()
    {
        // Не используется
    }

    public void InitializeTileArray(List<GameObject> tileObjects)
    {
        if (tileObjects.Count == 0) return;

        var xCoordinates = new HashSet<float>();
        var zCoordinates = new HashSet<float>();

        float gridSize = 1.01f;

        foreach (GameObject obj in tileObjects)
        {
            Vector3 pos = obj.transform.position;

            float roundedX = Mathf.Round(pos.x / gridSize) * gridSize;
            float roundedZ = Mathf.Round(pos.z / gridSize) * gridSize;

            Tile tile = obj.GetComponent<Tile>();
            tile.initialPosition = tile.transform.localPosition;

            xCoordinates.Add(roundedX);
            zCoordinates.Add(roundedZ);
        }

        var sortedX = xCoordinates.OrderBy(x => x).ToList();
        var sortedZ = zCoordinates.OrderBy(y => y).ToList();

        Dictionary<float, int> xMap = sortedX.Select((value, index) => new { value, index })
                                             .ToDictionary(pair => pair.value, pair => pair.index);
        Dictionary<float, int> zMap = sortedZ.Select((value, index) => new { value, index })
                                             .ToDictionary(pair => pair.value, pair => pair.index);

        tiles = new Tile[sortedX.Count, sortedZ.Count];

        foreach (GameObject obj in tileObjects)
        {
            Tile tile = obj.GetComponent<Tile>();
            if (tile != null)
            {
                Vector3 pos = obj.transform.position;
                float roundedX = Mathf.Round(pos.x / gridSize) * gridSize;
                float roundedZ = Mathf.Round(pos.z / gridSize) * gridSize;

                if (!xMap.ContainsKey(roundedX) || !zMap.ContainsKey(roundedZ))
                {
                    Debug.LogError($"Tile at position {pos} has invalid rounded coordinates ({roundedX}, {roundedZ})");
                    continue;
                }

                int xIndex = xMap[roundedX];
                int zIndex = zMap[roundedZ];

                // Проверяем, если предыдущий номер отсутствует, то используем его
                if (xIndex > 0 && tiles[xIndex - 1, zIndex] == null)
                {
                    xIndex--;
                }
                else if (zIndex > 0 && tiles[xIndex, zIndex - 1] == null)
                {
                    zIndex--;
                }

                tile.SetPosition(xIndex, zIndex);
                tiles[xIndex, zIndex] = tile;
                tiles[xIndex, zIndex].num.text = (xIndex + ", " + zIndex);
                tiles[xIndex, zIndex].num.SetText((xIndex + ", " + zIndex).ToString());

                if (tile.state == TileState.Start)
                {
                    StartingTile = tile;
                }
            }
        }

        CentralTile = tiles[sortedX.Count / 2, sortedZ.Count / 2];

        if (StartingTile != null)
        {
            ActiveTile = StartingTile;
        }
        else
        {
            ActiveTile = tiles[0, 0];
        }

        InitializePlayer();
        ActiveTile?.StepTaken();
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
        if (ActiveTile != null)
        {
            return ActiveTile;
        }
        else
        {
            Debug.Log("TileNotActive");
            return null;
        }
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
