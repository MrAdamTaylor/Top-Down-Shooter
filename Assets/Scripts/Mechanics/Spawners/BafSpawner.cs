using System.Collections.Generic;
using Mechanics.BafMechaniks;
using UnityEngine;
using Random = UnityEngine.Random;

public class BafSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private List<GameObject> _bafs;

    private BafSpawnPositions _spawnPositions;
    private float _lastTime;
    private bool _trueSpawn;
    private Vector3 _vec;

    void Start()
    {
        transform.position = Vector3.zero;
        _spawnPositions = (BafSpawnPositions)ServiceLocator.Instance.GetData(typeof(BafSpawnPositions));
    }

    void Update()
    {
        if (_lastTime + _spawnTime < Time.time)
        {
            while (!_trueSpawn)
            {
                 Vector3 vec = new Vector3(Random.Range(-Constants.XBORDER_MAX, Constants.XBORDER_MAX), 
                     Constants.STANDART_Y_POSITION, Random.Range(-Constants.ZBORDER_MAX, Constants.ZBORDER_MAX));
                 _vec = transform.TransformPoint(vec);
                _trueSpawn = _spawnPositions.Position(_vec);
            }
            Instantiate(_bafs[Random.Range(0, _bafs.Count)], _vec, Quaternion.identity);
            _trueSpawn = false;
            _lastTime = Time.time;
        }
    }
}
