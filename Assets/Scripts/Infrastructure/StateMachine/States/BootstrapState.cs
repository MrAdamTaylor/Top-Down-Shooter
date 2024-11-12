using System;
using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using Infrastructure.Services;
using Infrastructure.Services.AbstractFactory;
using Infrastructure.Services.AssertService;
using Infrastructure.StateMachine.Interfaces;
using UI.MVC.Model;
using UI.MVC.Presenters;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly string _level;
        private AssertBuilder _assertBuilder;
    
        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader, AllServices services, LevelConfigs levelConfigs)
        {
            DispoceList.Instance.Add(this);
            _assertBuilder = new AssertBuilder();
            _services = services;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(LevelConfigs), levelConfigs);

            if (levelConfigs.IsTime)
                RegisteredTimer(levelConfigs.PerSeconds, levelConfigs.TimeBeforeStartGame);
        
            if (levelConfigs.PlayerConfigs != null)
                RegisterPlayerServices(levelConfigs.PlayerConfigs);

            if (levelConfigs.SpawnerConfigsList.Count != 0)
            {
                for (int i = 0; i < levelConfigs.SpawnerConfigsList.Count; i++)
                {
                    BindServicesByType(levelConfigs.SpawnerConfigsList[i]);
                }
            }

            ServiceLocator.ServiceLocator.Instance.BindData(typeof(PlayerConfigs), levelConfigs.PlayerConfigs);
            RegisterServices();
            _level = levelConfigs.LevelName;
            _sceneLoader.Construct(_level);
        }

        public void Dispose()
        {
            _assertBuilder = null;
            _services.Dispose();
            GC.SuppressFinalize(this);
        }

        private void RegisteredTimer(int levelConfigsPerSeconds, int starSeconds)
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(TimeData), new TimeData(levelConfigsPerSeconds, starSeconds));
        }

        private void BindServicesByType(SpawnerConfigs levelConfigsSpawnerConfigs)
        {
            switch (levelConfigsSpawnerConfigs)
            {
                case BafSpawnerConfigs bafSpawner:
                    RegisterBafService(bafSpawner);
                    break;
                case EnemySpawnerConfigs enemySpawner:
                    RegisterEnemyesServices(enemySpawner);
                    break;
                case DebafSpawnerConfigs debafSpawnerConfigs:
                    break;
                case null:
                    throw new ArgumentNullException();
                    break;
                default:
                    Debug.Log($"<color=red>Unknown Spawner Configs</color>");
                    break;
            }
        }

        private void RegisterBafService(BafSpawnerConfigs bafSpawner)
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(IBafFactory), new BafFactory(_assertBuilder));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(BafSpawnerConfigs), bafSpawner);
        }

        private void RegisterEnemyesServices(EnemySpawnerConfigs enemySpawnerConfigs)
        {
            EnemyConfigs[] enemyConfigs = Resources.LoadAll<EnemyConfigs>("StaticData/Enemy/Actual");
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(IEnemyFactory), new EnemyFactory(_assertBuilder, enemyConfigs));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(EnemySpawnerConfigs), enemySpawnerConfigs);
        }


        private void RegisterPlayerServices(PlayerConfigs levelConfigsPlayerConfigs)
        {
            _services.RegisterSingle<ISpecialEffectFactory>
                (new SpecialEffectFactory(_assertBuilder));
            _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_assertBuilder));
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_assertBuilder, 
                _services.Single<IWeaponFactory>()));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_assertBuilder));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(ISpecialEffectFactory),_services.Single<ISpecialEffectFactory>());
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(UIAnimationPlayer), new UIAnimationPlayer());
        }

        public void Enter()
        {
            _sceneLoader.Load(Constants.INTERMEDIATE_SCENE);
            _stateMachine.Enter<LoadLevelState, string>(_level);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(0));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(DataSaver), 
                new DataSaver((ScoresStorage)ServiceLocator.ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        }
    }
}