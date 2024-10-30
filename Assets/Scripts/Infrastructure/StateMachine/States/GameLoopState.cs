using System;
using System.Collections.Generic;
using Enemies;
using Infrastructure.StateMachine.Interfaces;
using Logic;
using Logic.Timer;
using Player;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class GameLoopState : IState, IDisposable
    {
        private readonly GameStateMachine _stateMachine;

        private GameTimer _gameTimer;
        private PlayerDeath _playerDeath;
        private WaveSystem _waveSystem;
        private GameSystem _gameSystem;
        private EnemySpawnController _spawnController;

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

            _playerDeath = (PlayerDeath)ServiceLocator.ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            _waveSystem = (WaveSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(WaveSystem));
            _spawnController =
                (EnemySpawnController)ServiceLocator.ServiceLocator.Instance.GetData(typeof(EnemySpawnController));
            _gameSystem = (GameSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameSystem));

            _playerDeath.PlayerDefeat += _waveSystem.PauseWaveTimer;
            _playerDeath.PlayerDefeat += _spawnController.PlayerDefeated;
            _playerDeath.PlayerDefeat += _gameSystem.ShowResetMenu;
            
            List<EnemyController> enemyList =
                (List<EnemyController>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyController>));
            for (int i = 0; i < enemyList.Count; i++)
            {
                _playerDeath.PlayerDefeat += enemyList[i].GoalIsDefeated;
                Debug.Log($"<color=red>Enemy is {enemyList[i].name} aded</color>");
            }
        }

        public void Dispose()
        {
            _playerDeath.PlayerDefeat -= _waveSystem.PauseWaveTimer;
            _playerDeath.PlayerDefeat -= _spawnController.PlayerDefeated;
            _playerDeath.PlayerDefeat -= _gameSystem.ShowResetMenu;
        }
    }
}