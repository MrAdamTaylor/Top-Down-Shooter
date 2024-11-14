using Configs;
using Logic.Spawners;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IEnemyFactory : IService
    {
        public GameObject Create(EnemyConfigs configs, EnemySpawnPoint[] positions, GameObject parent);
    }
}