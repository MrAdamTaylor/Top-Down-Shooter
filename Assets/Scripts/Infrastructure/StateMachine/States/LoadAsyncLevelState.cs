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
    public class LoadAsyncLevelState : IPayloadedState<string>, IDisposable
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
        private IEnemyFactory _enemyFactory;

        public LoadAsyncLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            DispoceList.Instance.Add(this);
            _playerFactory = playerFactory;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }
        
        public LoadAsyncLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader,
            IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            DispoceList.Instance.Add(this);
            _playerFactory = playerFactory;
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(IUIFactory), _uiFactory);
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
            _commonParent = GameObject.Find(ConstantsSceneObjects.PREFABS_SCENE_GAMEOBJECT_PARENT_NAME);
            LoadPlayer();
            LoadBafSpawner();
            LoadEnemySpawner();
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
            
            Camera camera = Object.FindObjectOfType<Camera>();
            
            GameObject startPosition = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.INITIAL_POSITION);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(Vector3), startPosition.transform.position);
            
            _player = _playerFactory.Create(startPosition.transform.position, camera);
            _player.transform.parent =  _commonParent.transform;

            TimeData data = (TimeData)ServiceLocator.ServiceLocator.Instance.GetData(typeof(TimeData));
            if (data != null)
            {
                _timerManager = new TimerManager();
                ServiceLocator.ServiceLocator.Instance.BindData(typeof(TimerManager),_timerManager);
                _timer = CreateTimer(data);
            }
        
            _canvas = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.CANVAS_TAG);
            GameObject ui = _uiFactory.CreateWithLoadConnect(PrefabPath.UI_PLAYER_PATH, _canvas);
            
            _playerUIBinder = new PlayerUIBinder(ui.transform.GetComponentInChildren<CurrencyProvider>());
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(PlayerUIBinder), _playerUIBinder);
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
            _enemyFactory = (IEnemyFactory)ServiceLocator.ServiceLocator.Instance.GetData(typeof(IEnemyFactory));
            
            ISubscrible asyncSubscriber = (ISubscrible)_enemyFactory;
            asyncSubscriber.Subscribe(this);
        }
        
        private void ConstructUI(GameObject ui)
        {
            GameObject warning = GameObject.FindGameObjectWithTag(ConstantsSceneObjects.WARNING_CANVAS_MESSAGE);
            warning.SetActive(false);
        }

        public void EndAsyncLoad()
        {
            _stateMachine.Enter<LoadLevelState>();
        }
    }
}
