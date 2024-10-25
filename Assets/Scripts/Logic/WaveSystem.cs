using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic.Constants;
using UnityEngine;

public class WaveSystem
{
    public WaveTimer WaveTimer { get; private set; }

    private Queue<WaveStruct> _waves = new();
    private SpawnManager _spawnManager;
    private List<EnemySpawnList> _enemySpawnLists;

    private int _maxWaveCount;
    private int _waweIndex = 1;

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
                _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex).ToList();
            Debug.Log($"Wave Name: <color=yellow>{waveStruct.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
            _waweIndex++;
            List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
            _spawnManager.Configure(waveStruct.TimeBetweenSpawnIntantiate, accessPool);
        }
        else if (Constants.ONE == _waves.Count)
        {
            Debug.Log("<color=red>Last Wave! </color>");
            WaveStruct wave = _waves.Dequeue();
            List<EnemySpawnList> enemySpawnLists =
                _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex).ToList();
            Debug.Log($"Wave Name: <color=yellow>{wave.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
            _waweIndex++;
            List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
            _spawnManager.Configure(wave.TimeBetweenSpawnIntantiate, accessPool);
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
                _enemySpawnLists.Where(p => p.FirstAppearanceWave < _waweIndex).ToList();
            Debug.Log($"Wave Name: <color=yellow>{wave.WaveName} </color>  Accesses Enemies: {enemySpawnLists.Count}");
            _waweIndex++;
            List<string> accessPool = AccessEnemiesPool(enemySpawnLists);
            _spawnManager.Configure(wave.TimeBetweenSpawnIntantiate, accessPool);
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