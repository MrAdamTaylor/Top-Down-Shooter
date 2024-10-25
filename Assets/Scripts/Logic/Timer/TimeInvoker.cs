using System;
using UnityEngine;

namespace Logic.Timer
{
    public class TimeInvoker : MonoBehaviour
    {
        public event Action<float> OnUpdateTimeTickedEvent;
        public event Action<float> OnUpdateTimeUnscaledTickedEvent;
        public event Action OnOneSecondTickedEvent;
        public event Action OnOneSecondUnscaledTickedEvent;
    
        private float _oneSecTimer;
        private float _oneSecUnscaledTimer;

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            OnUpdateTimeTickedEvent?.Invoke(deltaTime);

            _oneSecTimer += deltaTime;
            if (_oneSecTimer >= 1f)
            {
                _oneSecTimer -= 1;
                OnOneSecondTickedEvent?.Invoke();
            }
        
            float unscaledDeltaTimer = Time.unscaledDeltaTime;
            OnUpdateTimeUnscaledTickedEvent?.Invoke(unscaledDeltaTimer);

            _oneSecUnscaledTimer += unscaledDeltaTimer;
            if (_oneSecUnscaledTimer >= 1f)
            {
                _oneSecUnscaledTimer -= 1f;
                OnOneSecondUnscaledTickedEvent?.Invoke();
            }
        }
    }
}
