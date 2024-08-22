using System;
using Enemies;
using UnityEngine;

namespace Mechanics.Spawners.NewArchitecture
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnObject;

        private GameObjectPool _pool;
        private int _objectsCount;

        private void Start()
        {
            Debug.Log(transform);
            GameObjectPool._parent = this.transform;
            _pool = new GameObjectPool(_spawnObject, _objectsCount);
            foreach (EnemyController controller in transform.GetComponentsInChildren<EnemyController>())
            {
                controller.Construct(this);
                controller.SubscribeDeath();
            }
        }

        public void Spawn()
        {
            GameObject obj = _pool.Get();
        }

        public void Construct(int enemyCount)
        {
            _objectsCount = enemyCount;
        }

        public void ReturnPool(GameObject o)
        {
            _pool.Return(o);
        }
    }
}
