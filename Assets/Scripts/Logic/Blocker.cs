using System;
using Player;
using UI.Menu;
using UnityEngine;

namespace Logic
{
    public class Blocker : IDisposable
    {
        private UIPauseManager _pauseManager;
        private PlayerDeath _playerDeath;
        private GameObject _playerCanvas;
        private GameSystem _gameSystem;
        
        public Blocker(PlayerDeath playerDeath, GameObject playerCanvas, GameSystem gameSystem, UIPauseManager pauseManager)
        {
            _playerCanvas = playerCanvas;
            _playerDeath = playerDeath;
            _playerDeath.PlayerDefeat += HideMenu;
            _gameSystem = gameSystem;
            _gameSystem.GameResumeAction += ShowMenu;
            _pauseManager = pauseManager;
            
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