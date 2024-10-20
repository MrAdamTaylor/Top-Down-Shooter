using System;
using UnityEngine;

namespace Logic
{
    public class Timer
    {
        public event Action<float> OnTimerValueChangedEvent;
        public event Action OnTimerFinishEvent;

        public TimerType ValueType { get; }

        public float RemainingSeconds { get; private set; }
        public bool isPaused { get; private set; }

        public Timer(TimerType type)
        {
            ValueType = type;
        }
        
        public Timer(TimerType type, float seconds)
        {
            ValueType = type;
            SetTime(seconds);
        }

        public void SetTime(float seconds)
        {
            RemainingSeconds = seconds;
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void Start()
        {
            if (RemainingSeconds == 0)
            {
                Debug.LogError("TIMER: You are trying start timer with remaining seconds equal 0");
                OnTimerFinishEvent?.Invoke();
            }

            isPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void Start(float seconds)
        {
            SetTime(seconds);
            Start();
        }

        public void Pause()
        {
            isPaused = true;
            Unsubscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void UnPause()
        {
            isPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void Stop()
        {
            Unsubscribe();
            RemainingSeconds = 0;
            
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
            OnTimerFinishEvent?.Invoke();
        }

        private void Subscribe()
        {
            switch (ValueType)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.Instance.OnUpdateTimeTickedEvent += OnUpdateTick;    
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    TimeInvoker.Instance.OnOneSecondTickedEvent += OnOneSecondTick;
                    break;
                case TimerType.OneSecTickUnscaled:
                    TimeInvoker.Instance.OnOneSecondUnscaledTickedEvent += OnOneSecondTick;
                    break;
                default:
                    throw new Exception("Unknown Timer type");
            }
        }

        private void Unsubscribe()
        {
            switch (ValueType)
            {
                case TimerType.UpdateTick:
                    TimeInvoker.Instance.OnUpdateTimeTickedEvent -= OnUpdateTick;    
                    break;
                case TimerType.UpdateTickUnscaled:
                    TimeInvoker.Instance.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    TimeInvoker.Instance.OnOneSecondTickedEvent -= OnOneSecondTick;
                    break;
                case TimerType.OneSecTickUnscaled:
                    TimeInvoker.Instance.OnOneSecondUnscaledTickedEvent -= OnOneSecondTick;
                    break;
                default:
                    throw new Exception("Unknown Timer type");
            }
        }

        private void OnOneSecondTick()
        {
            if (isPaused)
                return;

            RemainingSeconds -= 1f;
            CheckFinis();
        }

        private void OnUpdateTick(float deltaTime)
        {
            if(isPaused)
                return;

            RemainingSeconds -= deltaTime;
            CheckFinis();
        }

        private void CheckFinis()
        {
            if(RemainingSeconds <= 0)
                Stop();
            else
            {
                OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
            }
        }
    }
}