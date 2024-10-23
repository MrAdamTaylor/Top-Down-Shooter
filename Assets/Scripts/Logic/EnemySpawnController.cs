using System.Collections.Generic;
using Logic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mechanics.Spawners.NewSpawner
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private EnemyTypeValues[] _enemyTypeValues;
        private List<EnemySpawnerPool> _pools = new();
        private WaveSystem _waveSystem;

        private bool _isWaweSystemWorking;
        private Timer _spawnTimer;
        private TimerManager _timerManager;

        public void Construct(List<EnemySpawnerPool> pools)
        {
            _pools = pools;
        }
        
        public void Construct(List<EnemySpawnerPool> pools, WaveSystem waveSystem, TimerManager timerManager)
        {
            _pools = pools;
            _waveSystem = waveSystem;
            _timerManager = timerManager;
            _timerManager.SubscribeWaveTimer(waveSystem);
        }

        private void Update()
        {
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*if (!_isWaweSystemWorking && _waveSystem != null)
                {
                    _waveSystem.StartWaves();
                    _isWaweSystemWorking = true;
                }*/
                
                int index = Random.Range(0, _pools.Count);
                _pools[index].Spawn();
            }
        }
        
    }
    
    [System.Serializable]
    public struct EnemyTypeValues
    {
        public EnemyTypeValues(int percantage, string enemyName, List<GameObject> enemySkins)
        {
            _percantage = percantage;
            _enemyName = enemyName;
            _enemySkins = enemySkins;
        }

        [SerializeField] private int _percantage;
        [SerializeField] private string _enemyName;
        [SerializeField] private List<GameObject> _enemySkins;
    }
}