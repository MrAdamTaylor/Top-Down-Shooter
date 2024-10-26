using System.Collections.Generic;
using System.Linq;
using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
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

        
        private int _maxWaveCount;
        private int _waweIndex = Constants.ZERO;

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
        }

        public void Construct(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
        }

        public void StartNextWave()
        {
            if (_maxWaveCount == _waves.Count)
            {
                WaveTimer.StartTimer();
                WaveStruct waveStruct = _waves.Dequeue();
                List<EnemySpawnList> enemySpawnLists =
                    _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex+Constants.ONE).ToList();
                Debug.Log($"Wave Name: <color=yellow>{waveStruct.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
                SpawnCharacteristics waveData = _spawnCharacteristics[_waweIndex];
                _waweIndex++;
                List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
                _spawnManager.Configure(accessPool, waveData);
            }
            else if (Constants.ONE == _waves.Count)
            {
                Debug.Log("<color=red>Last Wave! </color>");
                WaveStruct wave = _waves.Dequeue();
                List<EnemySpawnList> enemySpawnLists =
                    _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex+Constants.ONE).ToList();
                Debug.Log($"Wave Name: <color=yellow>{wave.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
                SpawnCharacteristics waveData = _spawnCharacteristics[_waweIndex];
                _waweIndex++;
                List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
                _spawnManager.Configure(accessPool, waveData);
                WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
            }
            else if(Constants.ZERO == _waves.Count)
            {
                Debug.Log("<color=red>Wawes Finish </color>");
            }
            else
            {
                WaveStruct wave = _waves.Dequeue();
                List<EnemySpawnList> enemySpawnLists =
                    _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex+Constants.ONE).ToList();
                Debug.Log($"Wave Name: <color=yellow>{wave.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
                SpawnCharacteristics waveData = _spawnCharacteristics[_waweIndex];
                _waweIndex++;
                List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
                _spawnManager.Configure( accessPool,waveData);
                WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
            }
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