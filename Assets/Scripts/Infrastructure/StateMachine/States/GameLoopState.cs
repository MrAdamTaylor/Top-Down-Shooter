using System;
using System.Collections.Generic;
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
        private Player.Player _player;
        private UIPauseManager _pauseManager;
        private ISceneLoader _sceneLoader;
        private Blocker _blocker;
        private GameTimeStoper _gameTimeStoper;

        private Vector3 _respawnPosition;

        public GameLoopState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            DispoceList.Instance.Add(this);
            _sceneLoader = sceneLoader;
            _stateMachine = gameStateMachine;
        }

        public void Exit()
        {
            //throw new NotImplementedException();
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
            _gameTimeStoper = new GameTimeStoper();
            _gameTimeStoper.Construct();
            _player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
            _gameSystem = (GameSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameSystem));
            _pauseManager = (UIPauseManager)ServiceLocator.ServiceLocator.Instance.GetData(typeof(UIPauseManager));
            _pauseManager.Construct(_sceneLoader, _gameTimeStoper);
            _gameSystem.AddTimeStoper(_gameTimeStoper);
            _blocker = new Blocker(_playerDeath, playerUI, _gameSystem, _pauseManager);
            _respawnPosition = (Vector3)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Vector3));

            #region Player Death Subscribe
            
            _playerDeath.PlayerDefeat += _player.Blocked;
            _playerDeath.PlayerDefeatAction += _gameSystem.ShowResetMenu;
            _playerDeath.PlayerDefeatAction += _gameTimeStoper.StopTime;
            List<EnemyStateMachine> enemyList =
                (List<EnemyStateMachine>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyStateMachine>));
            for (int i = 0; i < enemyList.Count; i++) 
                _playerDeath.PlayerDefeatAction += enemyList[i].GoalIsDefeated;
            
            #endregion

            #region Game Resume Subscribe

            _gameSystem.GameResumeAction += PlayerTeleport;
            _gameSystem.GameResumeAction += _player.Revive;
            _gameSystem.GameResumeAction += _gameTimeStoper.ResumeTime;

            #endregion
        }

        private void PlayerTeleport()
        {
            _playerDeath.gameObject.transform.position = _respawnPosition;
        }

        public void Dispose()
        {
            _playerDeath.PlayerDefeatAction -= _gameSystem.ShowResetMenu;

            _gameTimer = null;
            _playerDeath = null;
            _waveSystem = null;
            _gameSystem = null;
            _spawnController = null;
            _player = null;
            _pauseManager = null;
            _sceneLoader = null;
            _blocker = null;
            _gameTimeStoper = null;
            
        }
    }
}