using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using Logic.Spawners;
using Logic.Timer;
using UnityEngine;

namespace Logic
{
    public class WaveSystem
    {
        public WaveTimer WaveTimer { get; private set; }

        private Queue<WaveStruct> _waves = new();
        private SpawnManager _spawnManager;
        private List<EnemySpawnList> _enemySpawnLists;
        private List<SpawnCharacteristics> _spawnCharacteristics;

        private float _lastWaveTime;
        private int _maxWaveCount;
        private int _waweIndex = 0;
        private int _lastIndex;

        public Action<int> WaveChange;

        public WaveSystem(List<WaveStruct> spawnerConfigsWaves, List<EnemySpawnList> enemiesSpawnCharacteristics)
        {
            _enemySpawnLists = enemiesSpawnCharacteristics;
            for (int i = 0; i < spawnerConfigsWaves.Count; i++)
            {
                _waves.Enqueue(spawnerConfigsWaves[i]);
            }
            _maxWaveCount = _waves.Count;
            WaveStruct firstWave = spawnerConfigsWaves.First();
            WaveTimer = new WaveTimer(firstWave.WaveTimePerSeconds, TimerType.OneSecTick);
            _spawnCharacteristics = 
                (List<SpawnCharacteristics>)ServiceLocator.Instance.GetData(typeof(List<SpawnCharacteristics>));
            ServiceLocator.Instance.BindData(typeof(WaveSystem), this);
        }

        public void Construct(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
        }

        public void PauseWaveTimer()
        {
            WaveTimer.PauseResume();
        }

        public void ResumeWaveTimer()
        {
            WaveTimer.PauseResume();
        }

        public void StartNextWave()
        {
            if (_maxWaveCount == _waves.Count)
            {
                WaveTimer.StartTimer();
                GetNextWave();
            }
            else if (1 == _waves.Count)
            {
                _lastIndex = _waweIndex + 1;
                WaveStruct wave = GetNextWave();
                _lastWaveTime = wave.WaveTimePerSeconds;
                WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
            }
            else if(0 == _waves.Count)
            {
                /*Debug.Log("<color=red>Wawes Finish </color>");
                List<string> accessPool = GetEnemiesList(_enemySpawnLists);
                SpawnCharacteristics waveData = _spawnCharacteristics[_lastIndex];
                _waweIndex++;
                WaveChange.Invoke(_waweIndex);
                _spawnManager.Configure( accessPool,waveData);
                WaveTimer.ReloadTimer(_lastWaveTime);*/
            }
            else
            {
                WaveStruct wave = GetNextWave();
                WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
            }
        }

        private WaveStruct GetNextWave()
        {
            WaveStruct wave = _waves.Dequeue();
            List<string> accessPool = GetEnemiesList(_enemySpawnLists);
            SpawnCharacteristics waveData = _spawnCharacteristics[_waweIndex];
            _waweIndex++;
            WaveChange.Invoke(_waweIndex);
            _spawnManager.Configure( accessPool,waveData);
            return wave;
        }

        private List<string> GetEnemiesList(List<EnemySpawnList> enemySpawnLists)
        {
            List<EnemySpawnList> newEnemySpawnLists =
                enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex+1).ToList();
            List<string> accessPool = AccessEnemiesPool(newEnemySpawnLists);
            return accessPool;
        }

        private List<string> AccessEnemiesPool(List<EnemySpawnList> enemySpawnLists)
        {
            List<string> accessPools = new List<string>();
            for (int i = 0; i < enemySpawnLists.Count; i++)
            {
                string accessPool = enemySpawnLists[i].EnemyConfigs.Name + Constants.POOL_PREFIX;
                accessPools.Add(accessPool);
            }
            return accessPools;
        }
    }
}