using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        Debug.Log("Создано состояние BootstrapState");
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        RegisterServices();
    }

    public void Enter()
    {
        Debug.Log("Вход в начальное состояние StateMachine");
        _sceneLoader.Load(Constants.First_LEVEL, onLoaded: EnterLoadLevel);
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IAsserts>(new Asserts());
        _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAsserts>(), 
            _services.Single<IWeaponFactory>()));
    }



    public void Exit()
    {
        Debug.Log("Выход из начального состояния StateMachine");
    }
    
    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(Constants.SECOND_LEVEL);
}