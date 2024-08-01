using UnityEngine;

public class BootstrapState : IState
{
    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        Debug.Log("Создано состояние BootstrapState");
    }

    public void Exit()
    {
        Debug.Log("Выход из начального состояния StateMachine");
    }

    public void Enter()
    {
        Debug.Log("Вход в начальное состояние StateMachine");
    }
}