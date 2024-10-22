using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.Spawners.NewSpawner
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private EnemyTypeValues[] _enemyTypeValues;
        private List<EnemySpawnerPool> _pools = new();

        public void Construct(List<EnemySpawnerPool> pools)
        {
            _pools = pools;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int index = Random.Range(0, _pools.Count);
                _pools[index].Spawn();
            }
        }
        
    }
    
    [System.Serializable]
    public struct EnemyTypeValues
    {
        public EnemyTypeValues(int percantage, string enemyName, List<GameObject> enemySkins)
        {
            _percantage = percantage;
            _enemyName = enemyName;
            _enemySkins = enemySkins;
        }

        [SerializeField] private int _percantage;
        [SerializeField] private string _enemyName;
        [SerializeField] private List<GameObject> _enemySkins;
    }
}