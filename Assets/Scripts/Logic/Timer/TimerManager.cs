using UnityEngine;

namespace Logic.Timer
{
    public class TimerManager
    {
        private GameTimer _gameTimer;
        private WaveSystem _waveSystem;
        private EnemySpawnController _enemySpawnController;

        public void Constructed(EnemySpawnController enemySpawnController)
        {
            _enemySpawnController = enemySpawnController;
        }

        public void SubscribeGameTimer(GameTimer gameTimerFinish)
        {
            _gameTimer = gameTimerFinish;
            _gameTimer.GameTimerFinish += LaunchWaweTimer;
        }

        public void SubscribeWaveTimer(WaveSystem waveSystem)
        {
            _waveSystem = waveSystem;
            waveSystem.WaveTimer.EndWaveAction += ReloadGameTimer;
        }

        private void LaunchWaweTimer()
        {
            _enemySpawnController.IsWaveEnd = false;
            _waveSystem.StartNextWave();
        }

        private void ReloadGameTimer()
        {
            _enemySpawnController.IsWaveEnd = true;
            _gameTimer.ReloadTimer();
        }
    }
}