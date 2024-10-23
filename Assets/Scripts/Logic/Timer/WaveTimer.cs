using System;
using Logic;
using UnityEngine;

public class WaveTimer : ITimer
{
    public Action EndWaveAction; 
    
    private float _remainingSeconds;
    private Timer _timer;
    
    public WaveTimer(float remainingTime, TimerType timerType)
    {
        _remainingSeconds = remainingTime;
        _timer = new Timer(timerType, _remainingSeconds);
        _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
        _timer.OnTimerFinishEvent += OnTimerFinished;
    }

    public void ReloadTimer(float waveTime)
    {
        _timer.SetTime(waveTime);
        _timer.Start();
    }

    public void StartTimer()
    {
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
    
    public void StopTimer()
    {
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