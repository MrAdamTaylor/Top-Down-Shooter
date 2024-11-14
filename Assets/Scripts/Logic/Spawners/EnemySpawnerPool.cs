using System;
using System.Threading.Tasks;
using Enemies;
using EnterpriceLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Spawners
{
    public class EnemySpawnerPool : MonoBehaviour
    {
        private GameObjectPool _pool;
        private int _objectCount;
        private EnemyDeath[] _enemyDeaths;

        private EnemySpawnPoint[] _enemySpawnPoints;

        public void Construct(int enemyCount, Func<GameObject> action, EnemySpawnPoint[] enemySpawnPoints)
        {
            _enemySpawnPoints = enemySpawnPoints;
            _objectCount = enemyCount;
            _pool = new GameObjectPool(action, _objectCount);
        }

        public void SubscribeDeathAction(EnemyDeath[] enemyDeaths)
        {
            _enemyDeaths = enemyDeaths;
            for (int i = 0; i < _enemyDeaths.Length; i++)
            {
                _enemyDeaths[i].ObjectDeathAction += ReturnPool;
            }
        }

        public void Spawn()
        {
            if (_pool.PoolCount > 0)
            {
                GameObject obj = _pool.Get();
            }
            else
            {
                Debug.Log("<color=yellow>Pool is Empty</color>");
            }
        }

        private void ReturnPool(GameObject obj)
        {
            Vector3 pos;
        
            int index = Random.Range(0, _enemySpawnPoints.Length);
            pos = _enemySpawnPoints[index].transform.position;
            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.identity;

            _pool.Return(obj);
        }

        public int GetUnpooledCount()
        {
           return _pool.GetUnpooledCount();
        }
    }
}