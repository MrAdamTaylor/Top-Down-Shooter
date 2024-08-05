using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolSpawner : MonoBehaviour
{
    [SerializeField] float[] _percantage;
    [SerializeField] GameObject[] _objectsToSpawn;
    [SerializeField] GameObject _spawnPoints;

    [SerializeField] GameObject _player;

    private List<Transform> spawns = new List<Transform>();

    private void Start()
    {
        foreach (Transform spawnObj in _spawnPoints.transform)
        {
            spawns.Add(spawnObj);
        }
    }

    private void Update()
    {
        Debug.Log("Создаём префаб!");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemy = Instantiate(_objectsToSpawn[GetRandomPool()], GetRandomPoint(spawns));
            RotateSystem rotateSystem = enemy.GetComponent<RotateSystem>();
            rotateSystem.LookedObject = _player.transform;
            MoveTo moving = enemy.GetComponent<MoveTo>();
            moving.Goal = _player.transform;
            foreach (Transform child in enemy.transform)
            {
                if (child.gameObject.name == "<Renderer>")
                {
                    PlayerDeathTriger trigger = child.GetComponent<PlayerDeathTriger>();
                    trigger.Killed = _player.transform;

                }
            }
        }
    }

    

    private Transform GetRandomPoint(List<Transform> spawnPoints)
    {
        return spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Count-1)];
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
        for (int i = 0; i < _objectsToSpawn.Length; i++)
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
