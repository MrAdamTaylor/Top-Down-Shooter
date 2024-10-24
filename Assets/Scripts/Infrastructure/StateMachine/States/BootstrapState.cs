using System;
using System.Collections.Generic;
using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    private readonly string _level;
    private readonly AssertBuilder _assertBuilder;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, LevelConfigs levelConfigs)
    {
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _assertBuilder = new AssertBuilder();
        ServiceLocator.Instance.BindData(typeof(LevelConfigs), levelConfigs);

        if (levelConfigs.IsTime)
            RegisteredTimer(levelConfigs.PerSeconds);
        
        if (levelConfigs.PlayerConfigs != null)
            RegisterPlayerServices(levelConfigs.PlayerConfigs);

        if (levelConfigs.SpawnerConfigsList.Count != 0)
        {
            for (int i = 0; i < levelConfigs.SpawnerConfigsList.Count; i++)
            {
                BindServicesByType(levelConfigs.SpawnerConfigsList[i]);
            }
        }

        ServiceLocator.Instance.BindData(typeof(PlayerConfigs), levelConfigs.PlayerConfigs);
        RegisterServices();
        _level = levelConfigs.LevelName;
    }

    private void RegisteredTimer(int levelConfigsPerSeconds)
    {
        ServiceLocator.Instance.BindData(typeof(TimeData), new TimeData(levelConfigsPerSeconds));
        Debug.Log("Timer Service Registered, Per seconds: "+levelConfigsPerSeconds);
    }

    private void BindServicesByType(SpawnerConfigs levelConfigsSpawnerConfigs)
    {
        switch (levelConfigsSpawnerConfigs)
        {
            case BafSpawnerConfigs bafSpawner:
                RegisterBafService(bafSpawner);
                Debug.Log($"<color=green> Baf Spawner Services Registered</color>");
                break;
            case EnemySpawnerConfigs enemySpawner:
                Debug.Log($"<color=green> Enemy Spawner Services Registered</color>");
                RegisterEnemyesServices(enemySpawner);
                break;
            case DebafSpawnerConfigs debafSpawnerConfigs:
                Debug.Log($"<color=green> Debaf Spawner Services Registered</color>");
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
        
    }

    private void RegisterEnemyesServices(EnemySpawnerConfigs levelConfigsSpawnerConfigs)
    {
        ServiceLocator.Instance.BindData(typeof(IEnemyFactory), new EnemyFactory(_assertBuilder));
        ServiceLocator.Instance.BindData(typeof(EnemySpawnerConfigs), levelConfigsSpawnerConfigs);
    }

    private void RegisterSpawnerServices(List<SpawnerConfigs> levelConfigsSpawnerConfigsList)
    {
        Debug.Log("Spawners count is "+levelConfigsSpawnerConfigsList.Count);
    }

    private void RegisterPlayerServices(PlayerConfigs levelConfigsPlayerConfigs)
    {
        _services.RegisterSingle<ISpecialEffectFactory>
            (new SpecialEffectFactory(_assertBuilder));
        _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_assertBuilder));
        _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_assertBuilder, 
            _services.Single<IWeaponFactory>()));
        _services.RegisterSingle<IUIFactory>(new UIFactory(_assertBuilder));
        ServiceLocator.Instance.BindData(typeof(ISpecialEffectFactory),_services.Single<ISpecialEffectFactory>());
        ServiceLocator.Instance.BindData(typeof(UIAnimationPlayer), new UIAnimationPlayer());
    }

    public void Enter()
    {
        _sceneLoader.Load(_level, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void RegisterServices()
    {
        
    }

    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(_level);
}


public class TimeData
{
    public float Time { get; private set; }

    public TimeData(int levelConfigsPerSeconds)
    {
        Time = levelConfigsPerSeconds;
    }
    
    
}
