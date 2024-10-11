using System;
using UnityEngine;

public class GameLoopState : IState
{
    public GameLoopState(GameStateMachine gameStateMachine)
    {
        
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void Enter()
    {
        Debug.Log("Game Loop Scene load");
    }
}