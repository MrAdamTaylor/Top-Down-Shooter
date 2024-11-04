using System;
using System.Collections.Generic;
using Enemies;
using Enemies.EnemyStateMachine;
using Infrastructure.StateMachine.Interfaces;
using Logic;
using Logic.Timer;
using Player;
using UI.Menu;
using UI.MVC.Presenters;
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
            TimerAdapter timerAdapter =
                (TimerAdapter)ServiceLocator.ServiceLocator.Instance.GetData(typeof(TimerAdapter));
            timerAdapter.Initialize();
            
            _playerDeath = (PlayerDeath)ServiceLocator.ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            GameObject playerUI = (GameObject)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameObject));
            Blocker blocker = new Blocker(_playerDeath, playerUI);
            //_waveSystem = (WaveSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(WaveSystem));
            /*_spawnController =
                (EnemySpawnController)ServiceLocator.ServiceLocator.Instance.GetData(typeof(EnemySpawnController));
            */
            _gameSystem = (GameSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameSystem));
            
            _playerDeath.PlayerDefeatAction += _gameSystem.ShowResetMenu;
            //_playerDeath.PlayerDefeat += _waveSystem.PauseWaveTimer;
            //_playerDeath.PlayerDefeat += _spawnController.PlayerDefeated;
            //_playerDeath.PlayerDefeat += _gameSystem.ShowResetMenu;
            
            /*List<EnemyController> enemyList =
                (List<EnemyController>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyController>));*/
            List<EnemyStateMachine> enemyList =
                (List<EnemyStateMachine>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyStateMachine>));
            for (int i = 0; i < enemyList.Count; i++)
            {
                _playerDeath.PlayerDefeatAction += enemyList[i].GoalIsDefeated;
                //Debug.Log($"<color=red>Enemy is {enemyList[i].name} aded</color>");
            }
        }

        public void Dispose()
        {
            //_playerDeath.PlayerDefeat -= _waveSystem.PauseWaveTimer;
            //_playerDeath.PlayerDefeat -= _spawnController.PlayerDefeated;
            _playerDeath.PlayerDefeatAction -= _gameSystem.ShowResetMenu;
        }
    }

    public class Blocker : IDisposable
    {
        private UIPauseManager _pauseManager;
        private PlayerDeath _playerDeath;
        private GameObject _playerCanvas;
        
        public Blocker(PlayerDeath playerDeath, GameObject playerCanvas)
        {
            _playerCanvas = playerCanvas;
            _playerDeath = playerDeath;
            _playerDeath.PlayerDefeat += HideMenu;
            _pauseManager = (UIPauseManager)ServiceLocator.ServiceLocator.Instance.GetData(typeof(UIPauseManager));
        }

        private void HideMenu()
        {
            _playerCanvas.SetActive(false);
            _pauseManager.panelPause.SetActive(false);
            _pauseManager.panelSound.SetActive(false);
            //_pauseManager.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _playerDeath.PlayerDefeat -= HideMenu;
        }
    }
}