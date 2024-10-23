using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic.Constants;
using UnityEngine;

public class WaveSystem
{
    public WaveTimer WaveTimer { get; private set; }

    private Queue<WaveStruct> _waves = new();

    private int _maxWaveCount;
    
    public WaveSystem(List<WaveStruct> spawnerConfigsWaves, List<EnemySpawnList> enemiesSpawnCharacteristics)
    {
        List<EnemySpawnList> enemySpawnLists = enemiesSpawnCharacteristics.Where(p => p.FirstAppearanceWave < 5).ToList();
        for (int i = 0; i < enemiesSpawnCharacteristics.Count; i++)
        {
            Debug.Log("Enemies Access before third wave: "+enemySpawnLists[i].EnemyConfigs.Name);
        }

        for (int i = 0; i < spawnerConfigsWaves.Count; i++)
        {
            _waves.Enqueue(spawnerConfigsWaves[i]);
        }
        _maxWaveCount = _waves.Count;
        
        WaveStruct firstWave = spawnerConfigsWaves.First();
        WaveTimer = new WaveTimer(firstWave.WaveTimePerSeconds, TimerType.OneSecTick);
    }

    public void StartNextWave()
    {
        if (_maxWaveCount == _waves.Count)
        {
            WaveTimer.StartTimer();
            _waves.Dequeue();
        }
        else if (Constants.ONE == _waves.Count)
        {
            Debug.Log("<color=red>Last Wave! </color>");
            WaveStruct wave = _waves.Dequeue();
            WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
        }
        else if(Constants.ZERO == _waves.Count)
        {
            Debug.Log("<color=red>Wawes Finish </color>");
        }
        else
        {
            WaveStruct wave = _waves.Dequeue();
            WaveTimer.ReloadTimer(wave.WaveTimePerSeconds);
        }
    }

}