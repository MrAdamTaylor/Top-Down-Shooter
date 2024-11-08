using UnityEngine;

namespace Logic
{
    public class GameTimeStoper
    {
        private float _innerSpeed;

        public void Construct()
        {
            _innerSpeed = Time.timeScale;
        }

        public void StopTime()
        {
            _innerSpeed = Time.timeScale;
            Time.timeScale = 0;
        }
        
        public void ResumeTime()
        {
            Time.timeScale = _innerSpeed;
        }
    }
}