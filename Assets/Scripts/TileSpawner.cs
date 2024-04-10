using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject goodTilePrefab;
    public GameObject badTilePrefab;
    public float spawnRate = 1f;
    public float tileSpeed = 3f;
    public int tilesPerLine = 4; // Количество тайлов в линии
    private float nextSpawnTime;
    private float lastLineYPosition = 0f; // Последняя позиция Y для контроля вертикального зазора
    public float verticalSpacing = 0.01f; // Вертикальный зазор между линиями плиток

    private List<GameObject> spawnedTiles = new List<GameObject>();

    void Update()
    {
        MoveTilesDown();

        if (Time.time >= nextSpawnTime)
        {
            SpawnTilesLine();
            nextSpawnTime = Time.time + 1 / spawnRate;
        }

        CleanupTiles();
    }

    void SpawnTilesLine()
    {
        int goodTileIndex = Random.Range(0, tilesPerLine); // Гарантируем одну "хорошую" плитку в линии

        for (int i = 0; i < tilesPerLine; i++)
        {
            Vector3 spawnPosition = new Vector3(i - (tilesPerLine / 2.0f) + 0.5f, transform.position.y + lastLineYPosition, 0);
            GameObject tilePrefab = i == goodTileIndex ? goodTilePrefab : badTilePrefab;
            GameObject spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
            spawnedTiles.Add(spawnedTile);
        }
    }

    void MoveTilesDown()
    {
        foreach (GameObject tile in spawnedTiles)
        {
            tile.transform.position += Vector3.down * tileSpeed * Time.deltaTime;
        }
    }

    void CleanupTiles()
    {
        for (int i = spawnedTiles.Count - 1; i >= 0; i--)
        {
            if (spawnedTiles[i].transform.position.y < -6) // Предполагаемая нижняя граница экрана
            {
                GameObject tileToDestroy = spawnedTiles[i];
                spawnedTiles.RemoveAt(i);
                Destroy(tileToDestroy);
            }
        }
    }
}
