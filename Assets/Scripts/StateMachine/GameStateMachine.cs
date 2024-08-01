using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain)
    {
        Debug.Log("Инициализирована StateMachine");
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader)
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }
    
    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();
      
        TState state = GetState<TState>();
        _activeState = state;
      
        return state;
    }
    
    private TState GetState<TState>() where TState : class, IExitableState => 
        _states[typeof(TState)] as TState;
}