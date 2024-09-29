using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.Spawners.NewArchitecture
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnObject;

        private GameObjectPool _pool;
        private int _objectsCount;

        void Start()
        {
            GameObjectPool._parent = this.transform;
            _pool = new GameObjectPool(_spawnObject, _objectsCount);
            List<Transform> childs = transform.Cast<Transform>().ToList();
            for (int i = 0; i < childs.Count; i++)
            {
                EnemyController controller = childs[i].GetComponent<EnemyController>();
                controller.Construct(this);
                controller.SubscribeDeath();
            }
        }

        public void Construct(int enemyCount)
        {
            _objectsCount = enemyCount;
        }

        public void Spawn()
        {
            GameObject obj = _pool.Get();
        }

        public void ReturnPool(GameObject o)
        {
            _pool.Return(o);
        }
    }
}
