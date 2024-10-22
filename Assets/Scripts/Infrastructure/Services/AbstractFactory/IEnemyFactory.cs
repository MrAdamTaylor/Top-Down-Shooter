using UnityEngine;

public interface IEnemyFactory : IService
{
    public GameObject Create(EnemyConfigs configs, EnemySpawnPoint[] positions, GameObject parent);
}