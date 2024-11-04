using System.Collections.Generic;
using Infrastructure.ServiceLocator;
using Player.MouseInput;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
    
        private float _innerSpeed;
        private List<MonoBehaviour> _components = new();

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
    }

    public interface IPlayerSystem
    {
        public void AddSelfBlockList();
    }
}
