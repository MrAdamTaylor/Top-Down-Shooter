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
using Infrastructure.Services;
using Infrastructure.Services.AbstractFactory;
using Infrastructure.StateMachine.Interfaces;
using Logic;
using Logic.Spawners;
using Logic.Timer;
using UI.MVC;
using UI.MVC.Helper;
using UI.MVC.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        
        private IPlayerFactory _playerFactory;
        private IUIFactory _uiFactory;

        private GameObject _timer;
        private GameObject _commonParent;
        private TimerManager _timerManager;
        private GameObject _canvas;
        private PlayerUIBinder _playerUIBinder;
        private GameObject _player;
    
        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            DispoceList.Instance.Add(this);
            _playerFactory = playerFactory;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }
        
        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            DispoceList.Instance.Add(this);
            _playerFactory = playerFactory;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }
        
        public void Dispose()
        {
            _playerFactory = null;
            _uiFactory = null;
        
            _timerManager = null;
            _playerUIBinder = null;
            GC.SuppressFinalize(this);
        }
    
        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            LoadPlayer();
            LoadEnemySpawner();
            LoadBafSpawner();
            CreateGameSystem();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateGameSystem()
        {
            GameObject gameSystem = new GameObject(ConstantsSceneObjects.GAME_SYSTEM_NAME);
            GameSystem system = gameSystem.AddComponent<GameSystem>();

            GameObject resetButtonUI = _uiFactory.CreateResetButton(_canvas);
            GameBootstraper gameBootstraper = (GameBootstraper)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameBootstraper));
            system.Construct(resetButtonUI, _sceneLoader, gameBootstraper.gameObject);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameSystem), system);
        }

        private void LoadBafSpawner()
        {
            if (ServiceLocator.ServiceLocator.Instance.IsGetData(typeof(IBafFactory)))
            {
                GameObject spawner = new GameObject("Spawner");
                BafSpawner bafSpawner = spawner.AddComponent<BafSpawner>();
                IBafFactory bafFactory =
                    (IBafFactory)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IBafFactory));
                BafSpawnerConfigs bafSpawnerConfigs =
                    (BafSpawnerConfigs)ServiceLocator.ServiceLocator.Instance.GetData(typeof(BafSpawnerConfigs));
                RingTrigger trigger = _player.AddComponent<RingTrigger>();
                trigger.Construct(_player.transform, 
                    bafSpawnerConfigs.InnerRadius, bafSpawnerConfigs.MaximusSpawnRadius);
                
                bafSpawner.Construct(bafFactory, bafSpawnerConfigs, trigger);
            }
        }

        private void LoadPlayer()
        {
            _commonParent = GameObject.Find(ConstantsSceneObjects.PREFABS_SCENE_GAMEOBJECT_PARENT_NAME);
            Camera camera = Object.FindObjectOfType<Camera>();
            
            GameObject startPosition = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.INITIAL_POSITION);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(Vector3), startPosition.transform.position);
            
            _player = _playerFactory.Create(startPosition.transform.position, camera);
            _player.transform.parent =  _commonParent.transform;

            TimeData data = (TimeData)ServiceLocator.ServiceLocator.Instance.GetData(typeof(TimeData));
            if (data != null)
            {
                _timerManager = new TimerManager();
                _timer = CreateTimer(data);
            }
        
            _canvas = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.CANVAS_TAG);
            GameObject ui = _uiFactory.CreateWithLoadConnect(PrefabPath.UI_PLAYER_PATH, _canvas);
            _playerUIBinder = new PlayerUIBinder(ui.transform.GetComponentInChildren<CurrencyProvider>());
            _playerUIBinder.BindAmmo(_player);
            _playerUIBinder.BindHealth(_player);
            ConstructUI(ui);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameObject), ui);
        }

        private GameObject CreateTimer(TimeData data)
        {
            GameObject mainTimer = new GameObject(ConstantsSceneObjects.TIMER_NAME);
            GameObject timerParent = GameObject.Find(ConstantsSceneObjects.PREFAB_SCENE_DEBUG_COMPONENT);
            mainTimer.transform.SetParent(timerParent.transform);
            TimeInvoker invoker = mainTimer.AddComponent<TimeInvoker>();
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(TimeInvoker),invoker);
            GameTimer gameTimer = mainTimer.AddComponent<GameTimer>();
            gameTimer.Construct(data.StartedTime, data.BetweenWaveTime, TimerType.OneSecTick, _timerManager);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameTimer), gameTimer);
            return mainTimer;
        }

        private void LoadEnemySpawner()
        {
            if (!ServiceLocator.ServiceLocator.Instance.IsGetData(typeof(EnemySpawnerConfigs))) 
                return;
            EnemySpawnPoint[] spawnPoints = Object.FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
            
            GameObject enemySpawner = new GameObject("EnemySpawner");
            EnemySpawnController spawnController = enemySpawner.AddComponent<EnemySpawnController>();
            enemySpawner.transform.parent =  _commonParent.transform;
            EnemySpawnerConfigs spawnerConfigs = (EnemySpawnerConfigs)ServiceLocator.ServiceLocator.Instance.
                GetData(typeof(EnemySpawnerConfigs));
            IEnemyFactory factory = (IEnemyFactory)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IEnemyFactory));

            List<int> maximumEnemies = CalculateWaveCharacteristics(spawnerConfigs);
            
            List<EnemyStateMachine> enemyList = new List<EnemyStateMachine>();
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(List<EnemyStateMachine>), enemyList);
            
            List<EnemySpawnerPool> enemyPools = CreatePools(spawnerConfigs, factory, spawnPoints, spawnController, maximumEnemies);

            WaveSystem waveSystem = null;
            if (spawnerConfigs.Waves.Count != null)
            {
                waveSystem = new WaveSystem(spawnerConfigs.Waves, spawnerConfigs.SpawnList);
            }

            _playerUIBinder.BindTimer(_timer.GetComponent<GameTimer>(), waveSystem.WaveTimer);
            _playerUIBinder.BindScores();
            _playerUIBinder.BindWaves(waveSystem);
            if(waveSystem != null)
                spawnController.Construct(enemyPools, waveSystem, _timerManager);
            else
            {
                spawnController.Construct(enemyPools);
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

        private List<EnemySpawnerPool> CreatePools(EnemySpawnerConfigs spawnerConfigs, IEnemyFactory factory, EnemySpawnPoint[] spawnPoints,
            EnemySpawnController spawnController, List<int> maximumsEnemies)
        {
            List<EnemySpawnerPool> spawnerPools = new();
            for (int i = 0; i < spawnerConfigs.SpawnList.Count; i++)
            {
                string name = spawnerConfigs.SpawnList[i].EnemyConfigs.Name;
                GameObject enemyPool = new GameObject(name +Constants.POOL_PREFIX);
                EnemySpawnerPool pool = enemyPool.AddComponent<EnemySpawnerPool>();
                var i1 = i;

                
                pool.Construct(maximumsEnemies[i],()=>factory.Create(spawnerConfigs.SpawnList[i1].EnemyConfigs, spawnPoints, enemyPool), spawnPoints);
                enemyPool.transform.SetParent(spawnController.transform);
                EnemyDeath[] enemyDeaths = enemyPool.GetComponentsInChildren<EnemyDeath>(true);
                pool.SubscribeDeathAction(enemyDeaths);
                spawnerPools.Add(pool);
            }

            return spawnerPools;
        }

        private void ConstructUI(GameObject ui)
        {
            GameObject warning = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.WARNING_CANVAS_MESSAGE);
            warning.SetActive(false);
            UIHelper helper = ui.AddComponent<UIHelper>();
            helper.Construct();
        }

        
    }
}
