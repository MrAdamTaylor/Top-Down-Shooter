using System;
using UnityEngine;

namespace Logic
{
    public class Timer
    {
        public event Action<float> OnTimerValueChangedEvent;
        public event Action OnTimerFinishEvent;

        private TimerType ValueType { get; }

        private float RemainingSeconds { get; set; }
        public bool IsPaused { get; private set; }

        private TimeInvoker _invoker;
        
        public Timer(TimerType type, float seconds)
        {
            ValueType = type;
            SetTime(seconds);
            _invoker = ConstructTimeInvoker();
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

            IsPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void Pause()
        {
            IsPaused = true;
            Unsubscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingSeconds);
        }

        public void UnPause()
        {
            IsPaused = false;
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
                    _invoker.OnUpdateTimeTickedEvent += OnUpdateTick;    
                    break;
                case TimerType.UpdateTickUnscaled:
                    _invoker.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    _invoker.OnOneSecondTickedEvent += OnOneSecondTick;
                    break;
                case TimerType.OneSecTickUnscaled:
                    _invoker.OnOneSecondUnscaledTickedEvent += OnOneSecondTick;
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
                    _invoker.OnUpdateTimeTickedEvent -= OnUpdateTick;    
                    break;
                case TimerType.UpdateTickUnscaled:
                    _invoker.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    _invoker.OnOneSecondTickedEvent -= OnOneSecondTick;
                    break;
                case TimerType.OneSecTickUnscaled:
                    _invoker.OnOneSecondUnscaledTickedEvent -= OnOneSecondTick;
                    break;
                default:
                    throw new Exception("Unknown Timer type");
            }
        }

        private static TimeInvoker ConstructTimeInvoker()
        {
            TimeInvoker invoker = (TimeInvoker)ServiceLocator.Instance.GetData(typeof(TimeInvoker));
            if (invoker == null)
            {
                throw new Exception("Not load TimeInvoker for Timer in Subscribe method");
            }
            return invoker;
        }

        private void OnOneSecondTick()
        {
            if (IsPaused)
                return;

            RemainingSeconds -= 1f;
            CheckFinis();
        }

        private void OnUpdateTick(float deltaTime)
        {
            if(IsPaused)
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