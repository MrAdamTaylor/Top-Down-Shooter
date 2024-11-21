using System;
using System.Collections.Generic;
using Configs;
using Infrastructure.ServiceLocator;
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
        private GameInjector _gameInjector;

        public GameStateMachine(ISceneLoader sceneLoader, LoadingCurtain curtain, AllServices services, 
            LevelConfigs levelConfigs)
        {
            DispoceList.Instance.Add(this);
            
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, levelConfigs),
                [typeof(LoadAsyncLevelState)] = new LoadAsyncLevelState(this, sceneLoader, curtain, 
                    services.Single<IPlayerFactory>(), 
                    services.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader),
            };
        }
        
        public GameStateMachine(ISceneLoader sceneLoader,  AllServices services, 
            LevelConfigs levelConfigs, GameInjector gameInjector)
        {
            DispoceList.Instance.Add(this);
            _gameInjector = gameInjector;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, levelConfigs),
                [typeof(LoadAsyncLevelState)] = new LoadAsyncLevelState(this, sceneLoader, 
                    services.Single<IPlayerFactory>(), 
                    services.Single<IUIFactory>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, levelConfigs),
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