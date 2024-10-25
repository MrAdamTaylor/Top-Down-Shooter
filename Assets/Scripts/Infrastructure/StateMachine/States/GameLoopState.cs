using System;
using Infrastructure.StateMachine.Interfaces;
using Logic.Timer;

namespace Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private GameTimer _gameTimer;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Enter()
        {
            _gameTimer = (GameTimer)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameTimer));
            _gameTimer.StartTimer();
        }
    }
}