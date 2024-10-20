using UnityEngine;

namespace Logic
{
    public class TimerTester : MonoBehaviour
    {
        [SerializeField] private TimerType _type;
        [SerializeField] private float _timerSeconds;
        
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(_type, _timerSeconds);
            _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            _timer.OnTimerFinishEvent += OnTimerFinished;
        }

        private void OnDestroy()
        {
            _timer.OnTimerValueChangedEvent -= OnTimerValueChanged;
            _timer.OnTimerFinishEvent -= OnTimerFinished;
        }

        private void OnTimerFinished()
        {
            Debug.Log($"Timer FINISHED");
        }

        private void OnTimerValueChanged(float remainingSeconds)
        {
            Debug.Log($"Timer ticked. Remaining seconds: {remainingSeconds}");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
                StartTimerClicked();
            
            if(Input.GetKeyDown(KeyCode.Space))
                PauseTimerClicked();
            
            if(Input.GetKeyDown(KeyCode.O))
                StopTimerClicked();
        }

        private void StartTimerClicked()
        {
            _timer.Start();
        }

        private void PauseTimerClicked()
        {
            if(_timer.isPaused)
                _timer.UnPause();
            else
            {
                _timer.Pause();
            }
        }

        private void StopTimerClicked()
        {
            _timer.Stop();
        }
    }
}