using System;
using UnityEngine;

namespace Logic.Timer
{
    public class GameTimer : MonoBehaviour, ITimer
    {
        //[SerializeField] private float _secondsByAuthor = Constants.TIMER_TEST;

        public Action GameTimerFinish;

        private float _staticTime;
        //private float _remainingSeconds;
        private Timer _timer;
        private TimerManager _timerManager;
    
    
    
        public void Construct(float startedTime, float remainingTime, TimerType timerType, TimerManager manager)
        {
            //_remainingSeconds = remainingTime;
            _staticTime = remainingTime;
            _timer = new Timer(timerType, startedTime);
            _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            _timer.OnTimerFinishEvent += OnTimerFinished;
            _timerManager = manager;
            _timerManager.SubscribeGameTimer(this);
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void ReloadTimer()
        {
            ReloadTimer(_staticTime);
        }

        public void ReloadTimer(float time)
        {
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
            GameTimerFinish?.Invoke();
            Debug.Log($"Game Timer FINISHED");
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void OnTimerValueChanged(float remainingSeconds)
        {
            Debug.Log($"Timer ticked. Remaining seconds: {remainingSeconds}");
        }
    }
}