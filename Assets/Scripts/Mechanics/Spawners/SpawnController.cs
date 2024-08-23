using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mechanics.Spawners.NewArchitecture;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] float[] _percantage;

    [SerializeField] private EnemySpawner[] _enemySpawners;

    [Range(10,100)]
    [SerializeField] private int _maxEnemyOnLevel;

    private void Awake()
    {
        float sum = _percantage.Sum();
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            float koef = _percantage[i] / sum;
            float maxEnemy = _maxEnemyOnLevel * koef;
            int enemyCount = (int)maxEnemy;
            _enemySpawners[i].Construct(enemyCount);
        }
    }


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
