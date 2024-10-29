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

            PlayerDeath playerDeath = (PlayerDeath)ServiceLocator.ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            WaveSystem waveSystem = (WaveSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(WaveSystem));
            EnemySpawnController spawnController =
                (EnemySpawnController)ServiceLocator.ServiceLocator.Instance.GetData(typeof(EnemySpawnController));
            GameSystem gameSystem = (GameSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameSystem));

            playerDeath.PlayerDefeat += waveSystem.PauseWaveTimer;
            playerDeath.PlayerDefeat += spawnController.PlayerDefeated;
            playerDeath.PlayerDefeat += gameSystem.ShowResetMenu;
            
            List<EnemyController> enemyList =
                (List<EnemyController>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyController>));
            for (int i = 0; i < enemyList.Count; i++)
            {
                playerDeath.PlayerDefeat += enemyList[i].GoalIsDefeated;
                Debug.Log($"<color=red>Enemy is {enemyList[i].name} aded</color>");
            }
        }
    }
}