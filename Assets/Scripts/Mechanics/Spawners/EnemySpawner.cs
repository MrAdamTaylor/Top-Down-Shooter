using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Mechanics.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] float[] _percantage;
        [SerializeField] Enemy[] _objectsToSpawn;
        [SerializeField] GameObject _spawnPoints;
        [SerializeField] private GameObject[] _spawnerParent;

        [SerializeField] private int _maxCount;

        private EnemyCreater _creater;
    
        private List<Transform> spawns = new List<Transform>();

        //private List<EnemyPool> _pools = new List<EnemyPool>();
        private List<PoolBase<Enemy>> _pools = new List<PoolBase<Enemy>>();

        #region PoolFunctions

        //public Enemy Preload() => Instantiate(_)
        //public List<Action> _instantiate = new List<Action>();
        public void GetAction(Enemy enemy) => enemy.gameObject.SetActive(true);
        public void ReturnAction(Enemy enemy) => enemy.gameObject.SetActive(false);

        #endregion
    
        public void AddPool(EnemyPool pool)
        {
            //_pools.Add(pool);
        }

        private void Awake()
        {
            //ServiceLocator.Instance.BindData(typeof(EnemySpawner), this);
        }

        private void Start()
        {
            for (int i = 0; i < _objectsToSpawn.Length; i++)
            {
                var i1 = i;
                Enemy Preload() => Instantiate(_objectsToSpawn[i1]);
            
                PoolBase<Enemy> poolBase = new PoolBase<Enemy>(Preload,GetAction, ReturnAction, _maxCount);
                _pools.Add(poolBase);
            }

            _creater = (EnemyCreater)ServiceLocator.Instance.GetData(typeof(EnemyCreater));
            foreach (Transform spawnObj in _spawnPoints.transform)
            {
                spawns.Add(spawnObj);
            }
            for (int i = 0; i < _maxCount; i++)
            {
                int index = GetRandomPool();
                //PoolBase<Enemy> poolBase = new PoolBase<Enemy>(,GetAction, ReturnAction, _maxCount);
                //_creater
                //_creater.CreateAndHide(_objectsToSpawn[index], GetRandomPoint(spawns), _spawnerParent[index].transform, _pools[index]);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _pools[GetRandomPool()].Get();
                //_pools[GetRandomPool()].Activate();
            }
        }

        private Transform GetRandomPoint(List<Transform> spawnPoints)
        {
            return spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Count-1)];
        }

        private int GetRandomPool()
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            float numForAdding = 0;
            float total = 0;
            for (int i = 0; i < _percantage.Length; i++)
            {
                total += _percantage[i];
            }
            for (int i = 0; i < _objectsToSpawn.Length; i++)
            {
                if (_percantage[i] / total + numForAdding >= random)
                {
                    return i;
                }
                else
                {
                    numForAdding += _percantage[i] / total;
                }
            }
            return 0;
        }
    }
}



