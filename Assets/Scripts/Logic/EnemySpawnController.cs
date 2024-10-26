using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Logic.Timer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
    public class EnemySpawnController : MonoBehaviour
    {
        [Header("Percent of enemy spawn chanse: ")]
        [SerializeField] private List<int> _percent;

        [Header("SpawnCount in moment")] [SerializeField]
        private int _spawnCountsInMoment;
        
        [Header("Maximum Enemies in Wave")] [SerializeField]
        private int _maximumEnemiesInWave;
        
        [Header("Spawn Interval")] [SerializeField]
        private int _spawnInterval;
        
        [Header("Delay Duration In TickSpawn")] [SerializeField]
        private float _duration;
        
        //[SerializeField] private EnemyTypeValues[] _enemyTypeValues;
        private List<EnemySpawnerPool> _maximumPools = new();
        private List<EnemySpawnerPool> _wavePools;
        
        private WaveSystem _waveSystem;

        private bool _isWaweSystemWorking;
        private Timer.Timer _spawnTimer;
        private TimerManager _timerManager;
        private SpawnManager _spawnManager;

        public void Construct(List<EnemySpawnerPool> pools)
        {
            _maximumPools = pools;
        }
        
        public void Construct(List<EnemySpawnerPool> pools, WaveSystem waveSystem, TimerManager timerManager)
        {
            _maximumPools = pools;
            _waveSystem = waveSystem;
            _spawnManager = new SpawnManager(this);
            _waveSystem.Construct(_spawnManager);
            _timerManager = timerManager;
            _timerManager.SubscribeWaveTimer(waveSystem);
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                int index = Random.Range(0, _maximumPools.Count);
                if (_spawnManager.CanSpawn(_maximumPools[index].name))
                {
                    _maximumPools[index].Spawn();
                }
                else
                {
                    Debug.Log("<color=yellow>Can't spawn </color>");
                }
            }*/
        }

        public void UpdateParams(SpawnCharacteristics waveData, List<string> whiteBoardPool)
        {
            _percent = waveData.PercentForCalculates;
            _spawnCountsInMoment = waveData.SpawnCountByTick;
            _duration = waveData.SpawnTickDelay;
            _spawnInterval = waveData.SpawnInterval;
            _maximumEnemiesInWave = waveData.MaxEnemyOnWave;
            _wavePools = BlackHandleListPool(whiteBoardPool);
            if (_wavePools.Count > Constants.ONE)
            {
                TestSpawn();
            }
            else
            {
                _wavePools[Constants.ZERO].Spawn();
            }
        }

        private void TestSpawn()
        {
            _maximumPools[GetRandomIndex()].Spawn();
        }

        private int GetRandomIndex()
        {
            float random = Random.Range(0f, 1f);
            float numForAdding = 0;
            float total = 0;
            for (int i = 0; i < _percent.Count; i++)
            {
                total += _percent[i];
            }
            for (int i = 0; i < _wavePools.Count; i++)
            {
                if (_percent[i] / total + numForAdding >= random)
                {
                    return i;
                }
                else
                {
                    numForAdding += _percent[i] / total;
                }
            }
            return 0;
        }

        /*private void Spawn()
        {
            
        }*/

        private List<EnemySpawnerPool> BlackHandleListPool(List<string> whiteBoardPool)
        {
            List<EnemySpawnerPool> enemySpawnerPools = new List<EnemySpawnerPool>();
                for (int j = 0; j < whiteBoardPool.Count; j++)
                {
                    for (int i = 0; i < _maximumPools.Count; i++)
                    {
                        if (_maximumPools[i].name == whiteBoardPool[j])
                        {
                            Debug.Log("Pool Name"+_maximumPools[i].name + "WhiteBoardName: "+whiteBoardPool[j]);
                            enemySpawnerPools.Add(_maximumPools[i]);
                        }
                    }
                }
                return enemySpawnerPools;
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