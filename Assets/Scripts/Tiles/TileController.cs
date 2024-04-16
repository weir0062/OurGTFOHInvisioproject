using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileController : MonoBehaviour
{
    public Tile[,] tiles;


    Tile ActiveTile;
    Tile CentralTile;
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

                tile.SetPosition(xIndex, yIndex); // Установка позиции тайла
                tile.SetText(xIndex + ", " + yIndex); // Отладочный вывод позиции
                tiles[xIndex, yIndex] = tile;
            }
        }
        ActiveTile = tiles[0, 0];

        CentralTile = tiles[xCoordinates.Count / 2, yCoordinates.Count / 2];
    }






    public Tile GetActiveTile()
    {
        if(ActiveTile)
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