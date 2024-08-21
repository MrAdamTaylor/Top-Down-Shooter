using System.Collections;
using System.Collections.Generic;
using Mechanics.Spawners.NewArchitecture;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] float[] _percantage;

    [SerializeField] private EnemySpawner[] _enemySpawners;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _enemySpawners[GetRandomPool()].Spawn();
        }
    }
    
    private int GetRandomPool()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        float numForAdding = 0;
        float total = 0;
        for (int i = 0; i < _percantage.Length; i++)
        {
            total += _percantage[i];
        }
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            if (_percantage[i] / total + numForAdding >= random)
            {
                return i;
            }
            else
            {
                numForAdding += _percantage[i] / total;
            }
        }
        return 0;
    }
}
