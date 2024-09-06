using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class TouchTriger : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private Bonus _bonus;
        private bool _isInside;
        private Transform _touching;
        public event Action OnTouch;

        private void Start()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            _touching = player.transform;
        }


        private void OnDrawGizmos()
        {
            Handles.color = _isInside ? Color.green : Color.red;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 center = this.transform.position.ExcludeY();
        
            if(_touching == null)
                return;

            Vector3 provoceuterPos = _touching.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            //_killed.position
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            if (_isInside)
            {
                OnTouch?.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}