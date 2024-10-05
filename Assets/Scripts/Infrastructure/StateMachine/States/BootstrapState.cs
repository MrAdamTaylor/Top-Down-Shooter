using EnterpriceLogic.Constants;
using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    private string _level;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, LevelConfigs levelConfigs)
    {
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        RegisterServices();
        ServiceLocator.Instance.BindData(typeof(LevelConfigs), levelConfigs);
        ServiceLocator.Instance.BindData(typeof(PlayerConfigs), levelConfigs.PlayerConfigs);
        _level = levelConfigs.LevelName;
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
        _services.RegisterSingle<IAsserts>(new Asserts());
        _services.RegisterSingle<ISpecialEffectFactory>(new SpecialEffectFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAsserts>(), 
            _services.Single<IWeaponFactory>()));
        _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAsserts>()));
        ServiceLocator.Instance.BindData(typeof(ISpecialEffectFactory),_services.Single<ISpecialEffectFactory>());
        ServiceLocator.Instance.BindData(typeof(UIAnimationPlayer), new UIAnimationPlayer());
    }

    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(_level);
}