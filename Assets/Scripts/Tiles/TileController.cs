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
    private void Update()
    {
        // Debug.Log(ActiveTile);
    }
    public void InitializeTileArray(List<GameObject> tileObjects)
    {
        if (tileObjects.Count == 0) return;

        var xCoordinates = new HashSet<float>();
        var zCoordinates = new HashSet<float>();

        float gridSize = 1.0f;

        foreach (GameObject obj in tileObjects)
        {
            Vector3 pos = obj.transform.position;

            float roundedX = Mathf.Round(pos.x / gridSize) * gridSize;
            float roundedZ = Mathf.Round(pos.z / gridSize) * gridSize;

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

                tile.SetPosition(xIndex, zIndex);
                tile.text.SetText(xIndex + "," + zIndex);
                tiles[xIndex, zIndex] = tile;

                 
            }
        }



        CentralTile = tiles[sortedX.Count / 2, sortedZ.Count / 2];
        ActiveTile = tiles[0, 0];
        InitializePlayer();
        tiles[0, 0]?.StepTaken();
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