using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BafSpawner", menuName = "Spawner/BafSpawner")]
public class BafSpawnerConfigs : SpawnerConfigs
{
    public List<BafConfigs> BafConfigsList;
}