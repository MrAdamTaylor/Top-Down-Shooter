using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.Spawners.NewSpawner
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private EnemyTypeValues[] _enemyTypeValues;

        private GameObject _parent;
        private IEnemyFactory _factory;

        private List<EnemyConfigs> _enemyConfigs = new();
        private List<float> _percantage = new();
        private List<Transform> _pointsPositions = new();
        
        public void Construct(IEnemyFactory factory, List<EnemySpawnList> configsList, GameObject parent, EnemySpawnPoint[] points)
        {
            _factory = factory;
            _enemyTypeValues = new EnemyTypeValues[configsList.Count];
            for (int i = 0; i < configsList.Count; i++)
            {
                _enemyTypeValues[i] = new EnemyTypeValues(
                    configsList[i].PercantageSpawn,
                    configsList[i].EnemyConfigs.name,
                    configsList[i].EnemyConfigs.Skins
                    );

                if (Contain(configsList[i].EnemyConfigs))
                {
                    _enemyConfigs.Add(configsList[i].EnemyConfigs);
                    _percantage.Add(configsList[i].PercantageSpawn);
                }
            }

            for (int i = 0; i < _enemyConfigs.Count; i++)
            {
                Debug.Log($"{_enemyConfigs[i].name}");
            }

            for (int i = 0; i < points.Length; i++)
            {
                _pointsPositions.Add(points[i].transform);
            }

            _parent = parent;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int index = Random.Range(0, _pointsPositions.Count);
                Transform randomTransform = _pointsPositions[index].transform;
                _factory.Create(GetRandomEnemy(),randomTransform.position, _parent);
            }
        }

        private bool Contain(EnemyConfigs enemyConfigs)
        {
            bool flag = false;
            switch (enemyConfigs)
            {
                case EnemyTurretConfigs:
                    flag = false;
                    break;
                case EnemyWalkingConfigs:
                    flag = true;
                    break;
                case null:
                    throw new ArgumentNullException();
            }
            return flag;
        }

        private EnemyConfigs GetRandomEnemy()
        {
            return _enemyConfigs[1];
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