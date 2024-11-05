using System.Collections.Generic;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
    
        private float _innerSpeed;
        private List<MonoBehaviour> _components = new();
        //private AnimationBlendTree _animationBlendTree;

        public void Construct(float speed)
        {
            _speed = speed;
            _innerSpeed = _speed;
            ServiceLocator.Instance.BindData(typeof(Player),this);
            
        }

        public void AddBlockList(MonoBehaviour component)
        {
            _components.Add(component);
        }

        public void Blocked()
        {
            for (int i = 0; i < _components.Count; i++) 
                _components[i].enabled = false;
        }

        public void UnBlocked()
        {
            for (int i = 0; i < _components.Count; i++) 
                _components[i].enabled = true;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Revive()
        {
            PlayerAnimator animationBlendTree = (PlayerAnimator)ServiceLocator.Instance
                .GetData(typeof(PlayerAnimator));
            animationBlendTree.PlayIdle();
            PlayerHealth health = (PlayerHealth)ServiceLocator.Instance.GetData(typeof(PlayerHealth));
            health.Revive();
            UnBlocked();
        }
    }
}
