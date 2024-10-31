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
            Debug.Log("<color=green>GameTimer EndWork, Start Wave Timer</color>");
            _enemySpawnController.IsWaveEnd = false;
            _waveSystem.StartNextWave();
        }

        private void ReloadGameTimer()
        {
            //Debug.Log("<color=green>Wawe Timer End Work, Restart GameTimer Timer</color>");
            _enemySpawnController.IsWaveEnd = true;
            _gameTimer.ReloadTimer();
        }
    }
}