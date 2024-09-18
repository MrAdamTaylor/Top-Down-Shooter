using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(Constants.First_LEVEL, onLoaded: EnterLoadLevel);
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IAsserts>(new Asserts());
        _services.RegisterSingle<ISpecialEffectFactory>(new SpecialEffectFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAsserts>(), 
            _services.Single<IWeaponFactory>()));
        ServiceLocator.Instance.BindData(typeof(ISpecialEffectFactory),_services.Single<ISpecialEffectFactory>());
    }

    public void Exit()
    {
    }
    
    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(Constants.SECOND_LEVEL);
}