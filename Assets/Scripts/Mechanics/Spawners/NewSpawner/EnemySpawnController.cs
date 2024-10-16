using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Mechanics.Spawners.NewSpawner
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private EnemyTypeValues[] _enemyTypeValues;

        private GameObject _parent;
        private IEnemyFactory _factory;

        private List<EnemyConfigs> _enemyConfigs = new();
        private List<float> _percantage = new();
        
        public void Construct(IEnemyFactory factory, List<EnemySpawnList> configsList, GameObject parent)
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

            _parent = parent;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _factory.Create(GetRandomEnemy(), _parent);
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