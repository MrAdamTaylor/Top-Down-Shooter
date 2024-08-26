using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics.BafMechaniks;
using UnityEngine;
using Random = UnityEngine.Random;

public class BafSpawner : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _spawnEffect;

    [SerializeField] private float _spawnTime;

    [SerializeField] private List<GameObject> _bafs;


    private BafSpawnPositions _spawnPositions;
    private float _lastTime;
    private bool _trueSpawn;
    private Vector3 _vec;

    private void Start()
    {
        transform.position = Vector3.zero;
        _spawnPositions = (BafSpawnPositions)ServiceLocator.Instance.GetData(typeof(BafSpawnPositions));
    }

    public void Update()
    {
        if (_lastTime + _spawnTime < Time.time)
        {
            while (!_trueSpawn)
            {
                 Vector3 vec = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-15f, 15f));
                 _vec = transform.TransformPoint(vec);
                _trueSpawn = _spawnPositions.Position(_vec);
            }
            Instantiate(_bafs[Random.Range(0, _bafs.Count)], _vec, Quaternion.identity);
            _trueSpawn = false;
            _lastTime = Time.time;
        }
    }
}
