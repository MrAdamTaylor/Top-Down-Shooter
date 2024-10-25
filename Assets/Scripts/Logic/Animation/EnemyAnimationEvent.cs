using UnityEngine;

namespace Logic.Animation
{
    public class EnemyAnimationEvent : MonoBehaviour
    {
        public AnimationEvent EnemyAnimEvent = new AnimationEvent();
        public void OnAnimationEvent(string eventName)
        {
            EnemyAnimEvent.Invoke(eventName);
        }
    }
}