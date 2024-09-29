using EnterpriceLogic.Constants;
using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    private string _level;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, GameParams gameParams)
    {
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        RegisterServices();
        ServiceLocator.Instance.BindData(typeof(GameParams), gameParams);
        _level = ChoseLevel(gameParams.ELevel);
    }

    private string ChoseLevel(Level gameParamsELevel)
    {
        string levelName = "";
        switch (gameParamsELevel)
        {
            case Level.Level1:
                levelName = Constants.FIRST_LEVEL;
                break;
            case Level.Level2:
                levelName = Constants.SECOND_LEVEL;
                break;
            case Level.Level3:
                levelName = Constants.LEVEL_THREE;
                break;
        }
        return levelName;
    }

    public void Enter()
    {
        _sceneLoader.Load(_level, onLoaded: EnterLoadLevel);
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

    public void Exit()
    {
    }
    
    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(_level);
}