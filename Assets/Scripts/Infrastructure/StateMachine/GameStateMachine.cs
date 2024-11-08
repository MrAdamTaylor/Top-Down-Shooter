using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.Services;
using Infrastructure.Services.AbstractFactory;
using Infrastructure.StateMachine.Interfaces;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IDisposable
    {
    
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ISceneLoader sceneLoader, LoadingCurtain curtain, AllServices services, LevelConfigs levelConfigs)
        {
            DispoceList.Instance.Add(this);
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, levelConfigs),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                    services.Single<IPlayerFactory>(), 
                    services.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader),
            };
        }

        public void Dispose()
        {
            _states.Clear();
            _activeState = null;
            GC.SuppressFinalize(this);
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
}