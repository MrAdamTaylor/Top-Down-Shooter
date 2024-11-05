using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BafSpawner", menuName = "Spawner/BafSpawner")]
    public class BafSpawnerConfigs : SpawnerConfigs
    {
        private const int MINIMAL_INNER_RADIUS = 3;
        private const int MAXIMUM_INNER_RADIUS = 5;
        
        private const int MINIMAL_SPAWN_RADIUS = 6;
        private const int MAXIMUM_SPAWN_RADIUS = 12;
        
        [Header("SpawnRadius")]
        
        [Range(MINIMAL_INNER_RADIUS, MAXIMUM_INNER_RADIUS)]
        public int InnerRadius = MINIMAL_INNER_RADIUS;

        [Range(MINIMAL_SPAWN_RADIUS, MAXIMUM_SPAWN_RADIUS)] 
        public int MaximusSpawnRadius = MINIMAL_SPAWN_RADIUS;
        
        public List<BafConfigs> BafConfigsList;

        [Header("First Time Wait")]
        public int FirstTimeWait;
        
        [Header("Per Sec Interval for Spawner")]
        public int SpawnInterval;
        
        [Header("Increase parameters")]
        public bool IncreaseSpawnTime;
        [Range(2, 10)] 
        public int PerSecTimeIncrease;
    }
}