using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Enemies;
using Enemies.EnemyStateMachine;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Math;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using Logic;
using Logic.Spawners;
using Logic.Timer;
using UI.MVC;
using UnityEngine;
using IState = Infrastructure.StateMachine.Interfaces.IState;
using Object = UnityEngine.Object;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IState, IDisposable
    {
        private GameStateMachine _stateMachine;
        private ISceneLoader _sceneLoader;
        private LevelConfigs _levelConfigs;
        private IEnemyFactory _factory;
        private PlayerUIBinder _playerUIBinder;
        private WaveSystem _waveSystem;
        private List<EnemySpawnerPool> _enemyPools = new();
        private TimerManager _timerManager;
        private EnemySpawnController _spawnController;
        private GameTimer _gameTimer;
        private IUIFactory _uiFactory;
        private GameObject _canvas;

        private GameObject _commonParent;
    
        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, LevelConfigs levelConfigs)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _levelConfigs = levelConfigs;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            DispoceList.Instance.Add(this);
            _factory = (IEnemyFactory)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IEnemyFactory));
            _playerUIBinder = (PlayerUIBinder)ServiceLocator.ServiceLocator.Instance.GetData(typeof(PlayerUIBinder));
            _timerManager = (TimerManager)ServiceLocator.ServiceLocator.Instance.GetData(typeof(TimerManager));
            _gameTimer = (GameTimer)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameTimer));
            _uiFactory = (IUIFactory)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IUIFactory));
            EnemySpawnerConfigs spawnerConfigs = (EnemySpawnerConfigs)ServiceLocator.ServiceLocator.Instance.
                GetData(typeof(EnemySpawnerConfigs));
            List<int> maximumEnemies = CalculateWaveCharacteristics(spawnerConfigs);
        
            EnemySpawnerCreator(spawnerConfigs, maximumEnemies);
            BindUI();
            if(_waveSystem != null)
                _spawnController.Construct(_enemyPools, _waveSystem, _timerManager);
            CreateGameSystem();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Dispose()
        {
            ISubscrible subscrible = (ISubscrible)_factory;
            subscrible.Unsubscribe();
            GC.SuppressFinalize(this);
        }

        private void CreateGameSystem()
        {
            GameObject gameSystem = new GameObject(ConstantsSceneObjects.GAME_SYSTEM_NAME);
            GameSystem system = gameSystem.AddComponent<GameSystem>();
            _canvas = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.CANVAS_TAG);
            GameObject resetButtonUI = _uiFactory.CreateResetButton(_canvas);
            GameBootstraper gameBootstraper = (GameBootstraper)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameBootstraper));
            system.Construct(resetButtonUI, _sceneLoader, gameBootstraper.gameObject);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameSystem), system);
        }
    
        private void BindUI()
        {
            _playerUIBinder.BindTimer(_gameTimer, _waveSystem.WaveTimer);
            _playerUIBinder.BindScores();
            _playerUIBinder.BindWaves(_waveSystem);
        }

        private void EnemySpawnerCreator(EnemySpawnerConfigs spawnerConfigs, List<int> maximumEnemies)
        {
            _commonParent = GameObject.Find(ConstantsSceneObjects.PREFABS_SCENE_GAMEOBJECT_PARENT_NAME);
            EnemySpawnPoint[] spawnPoints = Object.FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
            
            GameObject enemySpawner = new GameObject("EnemySpawner");
            _spawnController = enemySpawner.AddComponent<EnemySpawnController>();
            enemySpawner.transform.parent =  _commonParent.transform;
        
            List<EnemyStateMachine> enemyList = new List<EnemyStateMachine>();
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(List<EnemyStateMachine>), enemyList);
            
            _enemyPools = CreatePools(spawnerConfigs, spawnPoints, _spawnController, maximumEnemies);

            _waveSystem = null;
            if (spawnerConfigs.Waves.Count != null)
            {
                _waveSystem = new WaveSystem(spawnerConfigs.Waves, spawnerConfigs.SpawnList);
            }
        }

        private List<int> CalculateWaveCharacteristics(EnemySpawnerConfigs spawnerConfigs)
        {
            List<SpawnCharacteristics> spawnCharacteristics = new List<SpawnCharacteristics>(); 
            List<int> sums = new List<int>();
            List<List<int>> percentValue = new List<List<int>>();
            List<int> maxCounts = new List<int>();
            List<int> maxValue = new List<int>();
            List<List<int>> calculatedResults = new List<List<int>>();
            
            for (int i = 0; i < spawnerConfigs.Waves.Count; i++)
            {
                SpawnCharacteristics spawnCharacteristic = new SpawnCharacteristics();
                spawnCharacteristics.Add(spawnCharacteristic);
                
                
                List<int> resultsRow = new List<int>();
                
                maxCounts.Add(spawnerConfigs.Waves[i].MaxEnemyCountOnScreen);
                calculatedResults.Add(resultsRow);
                
                int zeroSum = 0;
                sums.Add(zeroSum);
            }

            for (int i = 0; i < spawnerConfigs.SpawnList.Count; i++)
            {
                List<int> percentsForOneEnemy = new List<int>();
                percentValue.Add(percentsForOneEnemy);

                for (int j = 0; j < spawnerConfigs.SpawnList[i].PercantageForEachWaves.Count; j++)
                {
                    percentValue[i].Add(spawnerConfigs.SpawnList[i].PercantageForEachWaves[j]);
                    sums[j] += spawnerConfigs.SpawnList[i].PercantageForEachWaves[j];
                }
            }
            
            PercentageCalculater.PercentageСhecker(sums,"percent sums");
            
            for (int i = 0; i < percentValue.Count; i++)
            {
                for (int j = 0; j < percentValue[i].Count; j++)
                {
                    int result = PercentageCalculater.CalculateValueInPercantage(percentValue[i][j],maxCounts[i]);
                    calculatedResults[i].Add(result);
                }
            }

            for (int i = 0; i < spawnerConfigs.Waves.Count; i++)
            {
                int spawnByTick = spawnerConfigs.Waves[i].CountSpawnByTick;
                int maxEnemiesOnWave = spawnerConfigs.Waves[i].MaxEnemyCountOnScreen;
                int spawnInterval = spawnerConfigs.Waves[i].EnemySpawnIntervalPerSeconds;
                List<int> percentsValueOfOneWave = percentValue.GetArrayByIndex(i);
                List<int> percent = percentsValueOfOneWave.Where(i1 => i1 != 0).ToList();
                spawnCharacteristics[i].Construct(spawnByTick, maxEnemiesOnWave, spawnInterval,  percent);
            }
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(List<SpawnCharacteristics>),spawnCharacteristics);

            for (int i = 0; i < calculatedResults.Count; i++)
            {
                List<int> values = calculatedResults[i];
                int max = values.DefaultIfEmpty().Max();
                maxValue.Add(max);
            }

            List<int> wavesCoefficient = new();
            for (int i = 0; i < spawnerConfigs.Waves.Count; i++)
            {
                wavesCoefficient.Add(spawnerConfigs.Waves[i].CountSpawnByTick);
            }

            int multiplier = wavesCoefficient.DefaultIfEmpty().Max();

            for (int i = 0; i < maxValue.Count; i++)
            {
                maxValue[i] *= multiplier;
            }

            return maxValue;
        }

        private List<EnemySpawnerPool> CreatePools(EnemySpawnerConfigs spawnerConfigs,  EnemySpawnPoint[] spawnPoints,
            EnemySpawnController spawnController, List<int> maximumsEnemies)
        {
            List<EnemySpawnerPool> spawnerPools = new();
            for (int i = 0; i < spawnerConfigs.SpawnList.Count; i++)
            {
                string name = spawnerConfigs.SpawnList[i].EnemyConfigs.Name;
                GameObject enemyPool = new GameObject(name +Constants.POOL_PREFIX);
                EnemySpawnerPool pool = enemyPool.AddComponent<EnemySpawnerPool>();
                var i1 = i;
                
                pool.Construct(maximumsEnemies[i],()=>_factory.Create(spawnerConfigs.SpawnList[i1].EnemyConfigs, spawnPoints, enemyPool), spawnPoints);
                enemyPool.transform.SetParent(spawnController.transform);
                EnemyDeath[] enemyDeaths = enemyPool.GetComponentsInChildren<EnemyDeath>(true);
                pool.SubscribeDeathAction(enemyDeaths);
                spawnerPools.Add(pool);
            }

            return spawnerPools;
        }
    }
}