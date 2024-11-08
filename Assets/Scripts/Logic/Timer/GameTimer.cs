using System;
using UnityEngine;

namespace Logic.Timer
{
    public class GameTimer : MonoBehaviour, ITimer
    {
        public bool IsActive { get; private set; }

        public Action GameTimerFinish;

        private float _staticTime;
        private Timer _timer;
        private TimerManager _timerManager;
    
    
    
        public void Construct(float startedTime, float remainingTime, TimerType timerType, TimerManager manager)
        {
            _staticTime = remainingTime;
            _timer = new Timer(timerType, startedTime);
            _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            _timer.OnTimerFinishEvent += OnTimerFinished;
            _timerManager = manager;
            _timerManager.SubscribeGameTimer(this);
        }

        public void Subscribe(Action<float> action)
        {
            _timer.OnTimerValueChangedEvent += action;
        }
        
        public void Unsubscribe(Action<float> action)
        {
            _timer.OnTimerValueChangedEvent -= action;
        }

        public void StartTimer()
        {
            IsActive = true;
            _timer.Start();
        }

        public void ReloadTimer()
        {
            ReloadTimer(_staticTime);
        }

        public void ReloadTimer(float time)
        {
            IsActive = true;
            _timer.SetTime(time);
            _timer.Start();
        }

        public void PauseResume()
        {
            if(_timer.IsPaused)
                _timer.UnPause();
            else
            {
                _timer.Pause();
            }
        }
    
        private void OnTimerFinished()
        {
            IsActive = false;
            GameTimerFinish?.Invoke();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void OnTimerValueChanged(float remainingSeconds)
        {
            //Debug.Log($"Timer ticked. Remaining seconds: {remainingSeconds}");
        }
    }
}