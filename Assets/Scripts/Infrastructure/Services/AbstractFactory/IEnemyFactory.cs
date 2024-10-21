using UnityEngine;

public interface IEnemyFactory : IService
{
    public void Create(EnemyConfigs configs, Vector3 position, GameObject parent);
}