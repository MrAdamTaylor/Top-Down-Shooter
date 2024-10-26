using UnityEngine;

namespace Logic.Timer
{
    public class TimerManager
    {
        private GameTimer _gameTimer;
        //private WaveTimer _waveTimer;
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
            //waveSystem.StartNextWave();
            _waveSystem = waveSystem;
            waveSystem.WaveTimer.EndWaveAction += ReloadGameTimer;
            //_waveTimer = waveSystem.WaveTimer;
        }

        private void LaunchWaweTimer()
        {
            Debug.Log("<color=green>GameTimer EndWork, Start Wave Timer</color>");
            _enemySpawnController.IsWaveEnd = false;
            _waveSystem.StartNextWave();
            //_waveTimer.StartTimer();
        }

        private void ReloadGameTimer()
        {
            Debug.Log("<color=green>Wawe Timer End Work, Restart GameTimer Timer</color>");
            _enemySpawnController.IsWaveEnd = true;
            _gameTimer.ReloadTimer();
        }
    }
}