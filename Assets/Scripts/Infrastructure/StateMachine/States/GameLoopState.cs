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

        private Vector3 _respawnPosition;

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
            GameTimeStoper gameTimeStoper = new GameTimeStoper();
            _player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
            _gameSystem = (GameSystem)ServiceLocator.ServiceLocator.Instance.GetData(typeof(GameSystem));
            Blocker blocker = new Blocker(_playerDeath, playerUI, _gameSystem);
            _respawnPosition = (Vector3)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Vector3));

            #region Player Death Subscribe
            
            _playerDeath.PlayerDefeat += _player.Blocked;
            _playerDeath.PlayerDefeatAction += _gameSystem.ShowResetMenu;
            _playerDeath.PlayerDefeatAction += gameTimeStoper.StopTime;
            List<EnemyStateMachine> enemyList =
                (List<EnemyStateMachine>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyStateMachine>));
            for (int i = 0; i < enemyList.Count; i++) 
                _playerDeath.PlayerDefeatAction += enemyList[i].GoalIsDefeated;
            
            #endregion

            #region Game Resume Subscribe

            //player.gameObject.transform.position = position;
            _gameSystem.GameResumeAction += PlayerTeleport;
            _gameSystem.GameResumeAction += _player.Revive;
            _gameSystem.GameResumeAction += gameTimeStoper.ResumeTime;

            #endregion
        }

        private void PlayerTeleport()
        {
            _playerDeath.gameObject.transform.position = _respawnPosition;
        }

        public void Dispose()
        {
            _playerDeath.PlayerDefeatAction -= _gameSystem.ShowResetMenu;
        }
    }

    public class GameTimeStoper
    {
        private float _innerSpeed;
        
        public void StopTime()
        {
            _innerSpeed = Time.timeScale;
            Time.timeScale = 0;
        }
        
        public void ResumeTime()
        {
            Time.timeScale = _innerSpeed;
        }
    }

    public class Blocker : IDisposable
    {
        private UIPauseManager _pauseManager;
        private PlayerDeath _playerDeath;
        private GameObject _playerCanvas;
        private GameSystem _gameSystem;
        
        public Blocker(PlayerDeath playerDeath, GameObject playerCanvas, GameSystem gameSystem)
        {
            _playerCanvas = playerCanvas;
            _playerDeath = playerDeath;
            _playerDeath.PlayerDefeat += HideMenu;
            _gameSystem = gameSystem;
            _gameSystem.GameResumeAction += ShowMenu;
            _pauseManager = (UIPauseManager)ServiceLocator.ServiceLocator.Instance.GetData(typeof(UIPauseManager));
            _pauseManager.Construct();
        }

        private void HideMenu()
        {
            _playerCanvas.SetActive(false);
            _pauseManager.BlockAll();
        }

        private void ShowMenu()
        {
            _playerCanvas.SetActive(true);
            _pauseManager.UnblockAll();
            _playerDeath.Alive();
        }

        public void Dispose()
        {
            _playerDeath.PlayerDefeat -= HideMenu;
        }
    }
}