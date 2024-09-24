using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services, GameParams gameParams)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, gameParams),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                services.Single<IPlayerFactory>(), 
                services.Single<IUIFactory>())
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
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