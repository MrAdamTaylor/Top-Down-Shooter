using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Spawner/EnemySpawner")]
public class EnemySpawnerConfigs : SpawnerConfigs
{
    public List<EnemySpawnList> SpawnList;
}

[Serializable]
public struct EnemySpawnList
{
    public EnemyConfigs EnemyConfigs;
    public int PercantageSpawn;
}