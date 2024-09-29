using System;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class TouchTriger : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private Bonus _bonus;
        public event Action OnTouch;
        private bool _isInside;
        private Transform _touching;

        void Start()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            _touching = player.transform;
        }

        //TODO - commented during compilation WebGL app
        /*void OnDrawGizmos()
        {
            Handles.color = _isInside ? Color.green : Color.red;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
        }*/

        void FixedUpdate()
        {
            Vector3 center = transform.position.ExcludeY();
        
            if(_touching == null)
                return;

            Vector3 provoceuterPos = _touching.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            if (_isInside)
            {
                OnTouch?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
