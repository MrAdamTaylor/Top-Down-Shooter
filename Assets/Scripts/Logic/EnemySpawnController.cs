using System.Collections.Generic;
using EnterpriceLogic.Constants;
using Logic.Timer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic
{
    public class EnemySpawnController : MonoBehaviour
    {
        public const float SPAWN_COOLDOWN = 0.5f;
        
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

        public bool IsWaveEnd { get; set; }

        private List<EnemySpawnerPool> _maximumPools = new();
        private List<EnemySpawnerPool> _wavePools;

        private WaveSystem _waveSystem;

        private bool _isWaweSystemWorking;
        private Timer.Timer _spawnTimer;
        private TimerManager _timerManager;
        private SpawnManager _spawnManager;
        private bool _isActiveSpawn;
        private float _spawnCooldown;
        private int _tempCounter;

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
            _timerManager.Constructed(this);
            _spawnTimer = new Timer.Timer(TimerType.OneSecTick, _spawnInterval);
            _spawnTimer.OnTimerFinishEvent += SpawnStart;
            _timerManager.SubscribeWaveTimer(waveSystem);
            _spawnCooldown = SPAWN_COOLDOWN;
        }

        private void SpawnStart()
        {
            if (!IsWaveEnd)
            {
                _isActiveSpawn = true;
                _spawnTimer.SetTime(_spawnInterval);
                _spawnTimer.Start();
                _tempCounter = 0;
            }
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (CanSpawn())
                EnemySpawn();

            if (IsWaveEnd)
            {
                _isActiveSpawn = false;
                _spawnTimer.Stop();
            }
        }

        public void UpdateParams(SpawnCharacteristics waveData, List<string> whiteBoardPool)
        {
            _percent = waveData.PercentForCalculates;
            _spawnCountsInMoment = waveData.SpawnCountByTick;
            _spawnInterval = waveData.SpawnInterval;
            _maximumEnemiesInWave = waveData.MaxEnemyOnWave;
            _wavePools = BlackHandleListPool(whiteBoardPool);
            _spawnTimer.SetTime(_spawnInterval);
            _spawnTimer.Start();
        }
        
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _spawnCooldown -= Time.deltaTime;
        }
        
        private void EnemySpawn()
        {
            Debug.Log("<color=yellow>Enemy Spawned!</color>");
            if (_wavePools.Count == Constants.ONE)
            {
                _wavePools[Constants.ZERO].Spawn();
            }
            else
            {
                _wavePools[GetRandomIndex()].Spawn();
            }
            _tempCounter++;
            _spawnCooldown = SPAWN_COOLDOWN;
        }

        private bool CanSpawn()
        {
            if (_isActiveSpawn && NoFullWave() && CooldownIsUp() && _lessThenMoment())
                return true;
            else
                return false;
        }

        private bool _lessThenMoment()
        {
            return _tempCounter <= _spawnCountsInMoment;
        }

        private bool CooldownIsUp()
        {
            return _spawnCooldown <= 0;
        }
        
        private bool NoFullWave()
        {
            int sum = 0;
            for (int i = 0; i < _wavePools.Count; i++)
            {
                sum += _wavePools[i].GetUnpooledCount();
            }

            if (sum < _maximumEnemiesInWave)
            {
                return true;
            }
            else
            {
                return false;
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
    
    /*[System.Serializable]
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
    }*/
}