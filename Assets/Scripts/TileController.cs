using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TileController : MonoBehaviour
{
    public Tile[,] tiles; 

    void Start()
    {
        List<GameObject> tileObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        InitializeTileArray(tileObjects);
    }






    void InitializeTileArray(List<GameObject> tileObjects)
    {
        if (tileObjects.Count == 0) return;

        float minX = float.MaxValue, maxX = float.MinValue, minY = float.MaxValue, maxY = float.MinValue;
        foreach (GameObject obj in tileObjects)
        {
            Vector3 pos = obj.transform.position;
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
        }

        float tileSize = 1.0f; 
        int width = Mathf.FloorToInt((maxX - minX) / tileSize) + 1;
        int height = Mathf.FloorToInt((maxY - minY) / tileSize) + 1;

        tiles = new Tile[width, height];

        foreach (GameObject obj in tileObjects)
        {
            Tile tile = obj.GetComponent<Tile>();
            if (tile != null)
            {
                Vector3 pos = obj.transform.position;
                int x = Mathf.FloorToInt((pos.x - minX) / tileSize);
                int y = Mathf.FloorToInt((pos.y - minY) / tileSize);
                tile.SetText(x + ", " +  y);
                tiles[x, y] = tile;
            }
        }
    }

}
