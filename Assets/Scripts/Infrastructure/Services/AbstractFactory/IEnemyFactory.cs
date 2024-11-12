using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using Logic.Spawners;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IEnemyFactory : IService
    {
        public GameObject Create(EnemyConfigs configs, EnemySpawnPoint[] positions, GameObject parent);
    }
}