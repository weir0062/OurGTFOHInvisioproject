using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab; // Префаб монетки
    public int numberOfCoins; // Количество монеток на уровне

    void Start()
    {
        GenerateCoins();
    }

    void GenerateCoins()
    {
        // Генерируем монетки в случайных позициях
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, 10), 0.5f, Random.Range(0, 10)); // Случайная позиция в пределах игрового поля
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }
}
