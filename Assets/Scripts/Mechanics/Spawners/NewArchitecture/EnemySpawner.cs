using System;
using Enemies;
using UnityEngine;

namespace Mechanics.Spawners.NewArchitecture
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnObject;

        [SerializeField] private Transform _parent;

        //Enemy Preload() => Instantiate(_spawnObject);
        //public void GetAction(Enemy enemy) => enemy.gameObject.SetActive(true);
        //public void ReturnAction(Enemy enemy) => enemy.gameObject.SetActive(false);

        //private PoolBase<Enemy> _pool;

        private GameObjectPool _pool;

        private void Start()
        {
            //_pool = new PoolBase<Enemy>(Preload, GetAction, ReturnAction, 10);

            Debug.Log(transform);
            _pool = new GameObjectPool(_spawnObject, 10, _parent);
        }

        public void Spawn()
        {
            GameObject obj = _pool.Get();
            //Enemy enemy = _pool.Get();
        }
    }
}
