using System.Collections.Generic;
using UnityEngine;

namespace Logic.Spawners
{
    public class SpawnCharacteristics
    {
        public int SpawnCountByTick { get; private set; }
        public int MaxEnemyOnWave { get; private set; }
        public int SpawnInterval { get; private set; }
        public int EnemiesForCalculateCount { get; private set; }

        public List<int> PercentForCalculates;


        public void Construct(int spawnByTick, int maxEnemiesOnWave, int spawnInterval, List<int> percent)
        {
            SpawnCountByTick = spawnByTick;
            MaxEnemyOnWave = maxEnemiesOnWave;
            SpawnInterval = spawnInterval;
            EnemiesForCalculateCount = percent.Count;
            PercentForCalculates = percent;
        }

        public void Output(int index)
        {
            Debug.Log($"Wave is {index+1}: <color=yellow>Count in OneSpawn: {SpawnCountByTick} </color> " +
                      $"<color=red>MaxEnemyOnWave: {MaxEnemyOnWave}</color>, <color=cyan>SpawnInterval: {SpawnInterval}</color>," +
                      $"<color=pink> EnemiesCount {EnemiesForCalculateCount}</color>");

            for (int i = 0; i < PercentForCalculates.Count; i++)
            {
                Debug.Log($"<color=cyan>Percent: [{PercentForCalculates[i]}]</color>");
            }
        }
    }
}