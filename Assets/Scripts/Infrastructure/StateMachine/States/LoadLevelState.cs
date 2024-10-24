using System.Collections.Generic;
using EnterpriceLogic.Constants;
using Mechanics.Spawners.NewSpawner;
using UnityEngine;
using Object = UnityEngine.Object;

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

        TimeData data = (TimeData)ServiceLocator.Instance.GetData(typeof(TimeData));
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
        ServiceLocator.Instance.BindData(typeof(TimeInvoker),invoker);
        GameTimer gameTimer = mainTimer.AddComponent<GameTimer>();
        gameTimer.Construct(data.Time, TimerType.OneSecTick, _timerManager);
        ServiceLocator.Instance.BindData(typeof(GameTimer), gameTimer);
        return mainTimer;
    }

    private void LoadEnemySpawner()
    {
        if (!ServiceLocator.Instance.IsGetData(typeof(EnemySpawnerConfigs))) 
            return;
        EnemySpawnPoint[] spawnPoints = Object.FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);
            
        GameObject enemySpawner = new GameObject("EnemySpawner");
        EnemySpawnController spawnController = enemySpawner.AddComponent<EnemySpawnController>();
        enemySpawner.transform.parent =  _commonParent.transform;
        EnemySpawnerConfigs spawnerConfigs = (EnemySpawnerConfigs)ServiceLocator.Instance.
            GetData(typeof(EnemySpawnerConfigs));
        IEnemyFactory factory = (IEnemyFactory)ServiceLocator.Instance.GetData(typeof(IEnemyFactory));

        List<EnemySpawnerPool> enemyPools = CreatePools(spawnerConfigs, factory, spawnPoints, spawnController);

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

    private List<EnemySpawnerPool> CreatePools(EnemySpawnerConfigs spawnerConfigs, IEnemyFactory factory, EnemySpawnPoint[] spawnPoints,
        EnemySpawnController spawnController)
    {
        List<EnemySpawnerPool> spawnerPools = new();
        for (int i = 0; i < spawnerConfigs.SpawnList.Count; i++)
        {
            string name = spawnerConfigs.SpawnList[i].EnemyConfigs.Name;
            GameObject enemyPool = new GameObject(name +" Pool");
            EnemySpawnerPool pool = enemyPool.AddComponent<EnemySpawnerPool>();
            var i1 = i;
            
            pool.Construct(1,()=>factory.Create(spawnerConfigs.SpawnList[i1].EnemyConfigs, spawnPoints, enemyPool), spawnPoints);
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