using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject goodTilePrefab;
    public GameObject badTilePrefab;
    public float tileSpeed = 3f;
    public int tilesPerLine = 5;  
    private float nextSpawnTime;
    private float lastLineYPosition = 0f;  
    public float badTileStartPercentage = 35f; 
    public float badTileMaxPercentage = 75f;   
    private float badTileCurrentPercentage;  
    public float percentageIncreaseRate = 0.1f; 


    private List<GameObject> spawnedTiles = new List<GameObject>();



    private void Start()
    {
    }

    private void Awake()
    {

        nextSpawnTime = Time.time;
        badTileCurrentPercentage = badTileStartPercentage;
    }
    void Update()
    {
        tileSpeed += 0.01f * Time.deltaTime;

        MoveTilesDown();

        float spawnInterval = CalculateSpawnInterval();

        if (Time.time >= nextSpawnTime)
        {
            SpawnTilesLine();
            nextSpawnTime = Time.time + spawnInterval;
        }

        CleanupTiles();

        UpdateBadTilePercentage();
    }

    float CalculateSpawnInterval()
    {
        float tileSize = 1.01f;  
        return (tileSize / tileSpeed)*Time.deltaTime;  
    }

    void UpdateBadTilePercentage()
    {
        if (badTileCurrentPercentage < badTileMaxPercentage)
        {
            badTileCurrentPercentage += percentageIncreaseRate;// * Time.deltaTime;
        }
    }




    void SpawnTilesLine()
    {

        float totalSize = 1.01f; // размер тайла + отсуп

        for (int i = 0; i < tilesPerLine; i++)
        {
            Vector3 spawnPosition = new Vector3((i * totalSize) - ((tilesPerLine * totalSize) / 2.0f) + (totalSize / 2.0f), transform.position.y + lastLineYPosition, 0);
            bool isBadTile = Random.Range(0f, 100f) < badTileCurrentPercentage;
            GameObject tilePrefab = isBadTile ? badTilePrefab : goodTilePrefab;
            GameObject spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
            spawnedTiles.Add(spawnedTile);
        }
        lastLineYPosition += 1.01f;
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
            if (spawnedTiles[i].transform.position.y < -6)  
            {
                GameObject tileToDestroy = spawnedTiles[i];
                spawnedTiles.RemoveAt(i);
                Destroy(tileToDestroy);
            }
        }
    }
}
