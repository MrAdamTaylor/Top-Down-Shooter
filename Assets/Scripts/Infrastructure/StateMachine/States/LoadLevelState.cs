using System.Collections.Generic;
using System.Linq;
using Configs;
using Enemies;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Math;
using Infrastructure.Services.AbstractFactory;
using Infrastructure.StateMachine.Interfaces;
using Logic;
using Logic.Spawners;
using Logic.Timer;
using UI.MVC.Helper;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;

        private GameObject _timer;
        private GameObject _commonParent;
        private TimerManager _timerManager;
    
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            _playerFactory = playerFactory;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }
    
        public void Enter(string sceneName)
        {
            var canvas = _loadingCurtain.GetComponent<Canvas>();
            canvas.enabled = true;
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            LoadPlayer();
            LoadEnemySpawner();

            _stateMachine.Enter<GameLoopState>();
        }

        private void LoadPlayer()
        {
            _commonParent = GameObject.Find(Constants.PREFABS_SCENE_GAMEOBJECT_PARENT_NAME);
            Camera camera = Object.FindObjectOfType<Camera>();
            GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        
            GameObject player = _playerFactory.Create(startPosition.transform.position, camera);
            player.transform.parent =  _commonParent.transform;

            TimeData data = (TimeData)ServiceLocator.ServiceLocator.Instance.GetData(typeof(TimeData));
            if (data != null)
            {
                _timerManager = new TimerManager();
                _timer = CreateTimer(data);
            }
        
            GameObject canvas = GameObject.FindGameObjectWithTag(Constants.CANVAS_TAG);
            GameObject ui = _uiFactory.CreateWithLoadConnect(PrefabPath.UI_PLAYER_PATH, canvas, player);
            ConstructUI(ui);
        }

        private GameObject CreateTimer(TimeData data)
        {
            GameObject mainTimer = new GameObject(Constants.TIMER_NAME);
            GameObject timerParent = GameObject.Find(Constants.PREFAB_SCENE_DEBUG_COMPONENT);
            mainTimer.transform.SetParent(timerParent.transform);
            TimeInvoker invoker = mainTimer.AddComponent<TimeInvoker>();
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(TimeInvoker),invoker);
            GameTimer gameTimer = mainTimer.AddComponent<GameTimer>();
            gameTimer.Construct(data.Time, TimerType.OneSecTick, _timerManager);
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

            List<EnemySpawnerPool> enemyPools = CreatePools(spawnerConfigs, factory, spawnPoints, spawnController, maximumEnemies);

            WaveSystem waveSystem = null;
            if (spawnerConfigs.Waves.Count != null)
            {
                waveSystem = new WaveSystem(spawnerConfigs.Waves, spawnerConfigs.SpawnList);
            }

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
                
                int zeroSum = Constants.ZERO;
                sums.Add(zeroSum);
            }

            for (int i = 0; i < spawnerConfigs.SpawnList.Count; i++)
            {
                List<int> percentsForOneEnemy = new List<int>();
                percentValue.Add(percentsForOneEnemy);

                for (int j = 0; j < spawnerConfigs.SpawnList[i].PercantageForEachWaves.Count; j++)
                {
                    Debug.Log($"Sum element in <color=red> I{i} J{j} - {spawnerConfigs.SpawnList[i].PercantageForEachWaves[j]}</color>");
                    percentValue[i].Add(spawnerConfigs.SpawnList[i].PercantageForEachWaves[j]);
                    sums[j] += spawnerConfigs.SpawnList[i].PercantageForEachWaves[j];
                }
            }
            
            sums.OutputCollection("Sums of percent");
            PercentageCalculater.PercentageСhecker(sums,"percent sums");

            //List<int> PercentsValueOfOneWave 
            
            for (int i = 0; i < percentValue.Count; i++)
            {
                
                //List<int> percent = percentValue[i].Where(i1 => i1 != 0).ToList();
                //percent.OutputCollection("Only full percent");
                //spawnCharacteristics[i].Construct();
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
                percent.OutputCollection("Only full percent");
                spawnCharacteristics[i].Construct(spawnByTick, maxEnemiesOnWave, spawnInterval,  percent);
                spawnCharacteristics[i].Output(i);
            }
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(List<SpawnCharacteristics>),spawnCharacteristics);

            for (int i = 0; i < calculatedResults.Count; i++)
            {
                calculatedResults[i].OutputCollection("Max Value in Each Wave");
                List<int> values = calculatedResults[i];
                int max = values.DefaultIfEmpty().Max();
                maxValue.Add(max);
                Debug.Log("Maximum Enemies: "+max);
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
            GameObject warning = GameObject.FindGameObjectWithTag(Constants.WARNING_CANVAS_MESSAGE);
            warning.SetActive(false);
            UIHelper helper = ui.AddComponent<UIHelper>();
            helper.Construct();
        }
    }
}

public class SpawnCharacteristics
{
    public int SpawnCountByTick { get; private set; }
    public int MaxEnemyOnWave { get; private set; }
    public int SpawnInterval { get; private set; }
    public int EnemiesForCalculateCount { get; private set; }

    public List<int> PercentForCalculates;


    public void Construct(int spawnByTick, int maxEnemiesOnWave, int spawnInterval, List<int> percent)
    {
        SpawnCountByTick = spawnByTick;
        MaxEnemyOnWave = maxEnemiesOnWave;
        SpawnInterval = spawnInterval;
        EnemiesForCalculateCount = percent.Count;
        PercentForCalculates = percent;
    }

    public void Output(int index)
    {
        Debug.Log($"Wave is {index+1}: <color=yellow>Count in OneSpawn: {SpawnCountByTick} </color> " +
                  $"<color=red>MaxEnemyOnWave: {MaxEnemyOnWave}</color>, <color=cyan>SpawnInterval: {SpawnInterval}</color>," +
                  $"<color=pink> EnemiesCount {EnemiesForCalculateCount}</color>");

        for (int i = 0; i < PercentForCalculates.Count; i++)
        {
            Debug.Log($"<color=cyan>Percent: [{PercentForCalculates[i]}]</color>");
        }
    }
}