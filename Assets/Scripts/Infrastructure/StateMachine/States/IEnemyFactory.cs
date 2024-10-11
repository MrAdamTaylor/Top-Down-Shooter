using UnityEngine;

public interface IEnemyFactory : IService
{
    public void Create(EnemyConfigs configs, GameObject parent);
}