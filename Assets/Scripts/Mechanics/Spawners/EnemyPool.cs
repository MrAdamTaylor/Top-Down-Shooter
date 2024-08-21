using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Spawners
{
    public class EnemyPool : MonoBehaviour
    {
        private Queue<GameObject> _pool = new Queue<GameObject>();

        private void Start()
        {
            EnemySpawner spawner = (EnemySpawner)ServiceLocator.Instance.GetData(typeof(EnemySpawner));
            spawner.AddPool(this);
        }


        public void AddToPool(GameObject createdObject)
        {
            createdObject.SetActive(false);
            _pool.Enqueue(createdObject);
        }

        public GameObject Activate()
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
    }
}
