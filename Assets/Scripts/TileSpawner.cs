using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject goodTilePrefab;
    public GameObject badTilePrefab;
    public float spawnRate = 1f;
    public float tileSpeed = 3f;
    
    private float nextSpawnTime;


    void Update()
    {
        MoveTilesDown();

        if (Time.time >= nextSpawnTime)
        {
            SpawnTile();
            nextSpawnTime = Time.time + 1 / spawnRate;
        }

        CleanupTiles();
    }

    void SpawnTile()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), transform.position.y, 0);
        bool isBadTile = Random.Range(0, 5) == 0;

        GameObject tilePrefab = isBadTile ? badTilePrefab : goodTilePrefab;
        GameObject spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
        spawnedTile.transform.SetParent(transform); // Сделаем TileSpawner родителем для удобства управления
    }

    void MoveTilesDown()
    {
        foreach (Transform child in transform)
        {
            child.position += Vector3.down * tileSpeed * Time.deltaTime;
        }
    }

    void CleanupTiles()
    {
        foreach (Transform child in transform)
        {
            if (child.position.y < -6) // Предполагая, что -6 это ниже видимой области экрана
            {
                Destroy(child.gameObject);
            }
        }
    }
}
