using System;
using UnityEngine;

namespace Logic.Timer
{
    public class WaveTimer : ITimer
    {
        public Action EndWaveAction;
        
        public bool IsActive { get; private set; }

        private float _remainingSeconds;
        private Timer _timer;


        public WaveTimer(float remainingTime, TimerType timerType)
        {
            _remainingSeconds = remainingTime;
            _timer = new Timer(timerType, _remainingSeconds);
            _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            _timer.OnTimerFinishEvent += OnTimerFinished;
        }

        public void Subscribe(Action<float> updateAmmo)
        {
            _timer.OnTimerValueChangedEvent += updateAmmo;
        }
        
        public void UnSubscribe(Action<float> updateAmmo)
        {
            _timer.OnTimerValueChangedEvent -= updateAmmo;
        }


        public void ReloadTimer(float waveTime)
        {
            IsActive = true;
            _timer.SetTime(waveTime);
            _timer.Start();
        }

        public void StartTimer()
        {
            IsActive = true;
            _timer.Start();
        }

        public void PauseResume()
        {
            Debug.Log("<color=red> Timer Pause/Resume </color>");
            if(_timer.IsPaused)
                _timer.UnPause();
            else
            {
                _timer.Pause();
            }
        }

        public void StopTimer()
        {
            IsActive = false;
            _timer.Stop();
        }

        private void OnTimerFinished()
        {
            EndWaveAction?.Invoke();
            Debug.Log($"Wave Finished: {_remainingSeconds}");
        }

        private void OnTimerValueChanged(float remainingSeconds)
        {
            Debug.Log($"Wave Timer ticked. Remaining seconds: {remainingSeconds}");
        }
    }
}