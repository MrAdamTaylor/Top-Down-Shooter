using Logic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float _remainingSeconds;
    private Timer _timer;
    
    public void Construct(float remainingTime, TimerType timerType)
    {
        _remainingSeconds = remainingTime;
        _timer = new Timer(timerType, _remainingSeconds);
        _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
        _timer.OnTimerFinishEvent += OnTimerFinished;
    }

    public void StartTimer()
    {
        _timer.Start();
    }

    public void PauseResume()
    {
        if(_timer.isPaused)
            _timer.UnPause();
        else
        {
            _timer.Pause();
        }
    }

    private void OnTimerFinished()
    {
        Debug.Log($"Game Timer FINISHED");
    }

    public void Stop()
    {
        _timer.Stop();
    }

    private void OnTimerValueChanged(float remainingSeconds)
    {
        Debug.Log($"Timer ticked. Remaining seconds: {remainingSeconds}");
    }
}