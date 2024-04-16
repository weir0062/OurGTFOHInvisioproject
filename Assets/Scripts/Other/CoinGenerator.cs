using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab; // ������ �������
    public int numberOfCoins; // ���������� ������� �� ������

    void Start()
    {
        GenerateCoins();
    }

    void GenerateCoins()
    {
        // ���������� ������� � ��������� ��������
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(0, 10), 0.5f, Random.Range(0, 10)); // ��������� ������� � �������� �������� ����
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }
}
