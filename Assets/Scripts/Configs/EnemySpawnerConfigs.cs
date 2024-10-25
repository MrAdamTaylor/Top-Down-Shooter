using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner/EnemySpawner")]
    public class EnemySpawnerConfigs : SpawnerConfigs
    {
        public List<EnemySpawnList> SpawnList;

        public List<WaveStruct> Waves;

        public bool RepeatLastWave;

        public void OnValidate()
        {
            for (int i = 0; i < SpawnList.Count; i++)
            {
                if (SpawnList[i].PercantageForEachWaves.Count > Waves.Count)
                {
                    SpawnList[i].PercantageForEachWaves.RemoveAt(Waves.Count);
                }
            }

            for (int i = 0; i < SpawnList.Count; i++)
            {
                for (int j = 0; j < SpawnList[i].PercantageForEachWaves.Count; j++)
                {
                    if (j < SpawnList[i].FirstAppearanceWave)
                    {
                        SpawnList[i].PercantageForEachWaves[j] = 0;
                    }
                }
            }
        }
    }

    [Serializable]
    public struct WaveStruct
    {
        private const int MIN_ENEMY_SPAWN = 1;
        private const int MAX_ENEMY_SPAWN = 10;
        private const int MAX_ENEMY_SPAWN_ON_SCREEN = 20;
    
        private const float MIN_TIME_INTERVAL_SPAWN = 0.2f;
        private const float MAX_TIME_INTERVAL_SPAWN = 1f;

        private const int MIN_SECONDS_SPAWN_INTERVAL = 3;
        private const int MAX_SECONDS_SPAWN_INTERVAL = 10;

        public string WaveName;
    
        [Range(MIN_TIME_INTERVAL_SPAWN, MAX_TIME_INTERVAL_SPAWN)]
        public float TimeBetweenSpawnIntantiate;
    
        [Range(MIN_ENEMY_SPAWN,MAX_ENEMY_SPAWN)]
        public int CountSpawnByTick;
    
        public int WaveTimePerSeconds;
    
        [Range(MIN_ENEMY_SPAWN, MAX_ENEMY_SPAWN_ON_SCREEN)]
        public int MaxEnemyCountOnScreen;
    
        [Range(MIN_SECONDS_SPAWN_INTERVAL,MAX_SECONDS_SPAWN_INTERVAL)]
        public int EnemySpawnIntervalPerSeconds;
    }

    [Serializable]
    public struct EnemySpawnList
    {
        public EnemyConfigs EnemyConfigs;
    
        public int FirstAppearanceWave;

        [Space]
        [Header("Not be more than waves count")]
        [Range(0, 100)]
        public List<int> PercantageForEachWaves;
    }
}