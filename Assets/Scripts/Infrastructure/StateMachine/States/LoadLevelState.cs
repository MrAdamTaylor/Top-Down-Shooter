using EnterpriceLogic.Constants;
using Mechanics.Spawners.NewSpawner;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IPlayerFactory _playerFactory;
    private readonly IUIFactory _uiFactory;
    private GameObject _commonParent;
    
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
        _commonParent = GameObject.Find(Constants.SCENE_PARENT_NAME);
        Camera camera = Object.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        GameObject player = _playerFactory.Create(startPosition.transform.position, camera);
        player.transform.parent =  _commonParent.transform;
        GameObject canvas = GameObject.FindGameObjectWithTag("PlayerUI");
        GameObject ui = _uiFactory.CreateWithLoadConnect(PrefabPath.UI_PLAYER_PATH, canvas, player);
        ConstructUI(ui);
    }

    private void LoadEnemySpawner()
    {
        if(ServiceLocator.Instance.IsGetData(typeof(EnemySpawnerConfigs)))
        {
            Debug.Log($"Enemy Spawner is Registered!");
            GameObject enemySpawner = new GameObject("EnemySpawner");
            EnemySpawnController spawnController = enemySpawner.AddComponent<EnemySpawnController>();
            enemySpawner.transform.parent =  _commonParent.transform;
            EnemySpawnerConfigs spawnerConfigs = (EnemySpawnerConfigs)ServiceLocator.Instance.
                GetData(typeof(EnemySpawnerConfigs));
            IEnemyFactory factory = (IEnemyFactory)ServiceLocator.Instance.GetData(typeof(IEnemyFactory));
            
            spawnController.Construct(factory, spawnerConfigs.SpawnList,_commonParent);
        }
    }

    private void ConstructUI(GameObject ui)
    {
        GameObject warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
        UIHelper helper = ui.AddComponent<UIHelper>();
        helper.Construct();
    }
}