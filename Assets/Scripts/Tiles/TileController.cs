using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

                // Проверка на дубликаты
                if (tiles[xIndex, zIndex] != null)
                {
                    Debug.LogError($"Duplicate tile at position ({xIndex}, {zIndex})");
                    continue;
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

        // Проверка и исправление пропущенных тайлов
        for (int x = 0; x < sortedX.Count; x++)
        {
            for (int z = 0; z < sortedZ.Count; z++)
            {
                if (tiles[x, z] == null)
                {
                    ShiftTilesToFillMissing(x, z);
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
        player.TakeStep(tiles[0, 0]) ;
        ActiveTile?.StepTaken();
    }

    void ShiftTilesToFillMissing(int missingX, int missingZ)
    {
        for (int x = missingX; x < tiles.GetLength(0); x++)
        {
            for (int z = missingZ; z < tiles.GetLength(1); z++)
            {
                if (tiles[x, z] == null)
                {
                    for (int nextX = x; nextX < tiles.GetLength(0); nextX++)
                    {
                        for (int nextZ = z + 1; nextZ < tiles.GetLength(1); nextZ++)
                        {
                            if (tiles[nextX, nextZ] != null)
                            {
                                tiles[x, z] = tiles[nextX, nextZ];
                                tiles[nextX, nextZ] = null;
                                tiles[x, z].SetPosition(x, z);
                                tiles[x, z].num.text = (x + ", " + z);
                                tiles[x, z].num.SetText((x + ", " + z).ToString());
                                break;
                            }
                        }
                    }
                }
            }
            missingZ = 0; // Сброс Z на 0 после первой итерации
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
