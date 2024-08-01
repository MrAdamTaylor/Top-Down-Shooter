using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
        Debug.Log("Создано состояние BootstrapState");
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        Debug.Log("Вход в начальное состояние StateMachine");
        RegisterServices();
        _sceneLoader.Load(Constants.First_LEVEL, onLoaded: EnterLoadLevel);
    }

    private void RegisterServices()
    {
        Debug.Log("Регистрация сервисов");
    }

    public void Exit()
    {
        Debug.Log("Выход из начального состояния StateMachine");
    }
    
    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(Constants.SECOND_LEVEL);
}