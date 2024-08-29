using System;
using Mechanics;
using Mechanics.BafMechaniks;
using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class DeathTriger : MonoBehaviour
    {
        [SerializeField] private float _radius;

        private Transform _killed;
        private bool _isInside;

        private Death _death;

        private void OnEnable()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            _killed = player.transform;
        }

        public void OnDrawGizmos()
        {
            Vector3 center = transform.position.ExcludeY();
        
            if(_killed == null)
                return;

            Vector3 provoceuterPos = _killed.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            //_killed.position
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            Handles.color = _isInside ? Color.green : Color.red;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
        }

        public void FixedUpdate()
        {
            Vector3 center = this.transform.position.ExcludeY();
        
            if(_killed == null)
                return;

            Vector3 provoceuterPos = _killed.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            //_killed.position
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            if (_isInside)
            {
                if (_killed.gameObject.GetComponent<PlayerInvincible>() != null)
                {
                    return;
                }
                else
                {
                    _death = _killed.gameObject.GetComponent<Death>();
                    _death.MakeDeath();
                    _death = null;
                }
            }
        }
    }
}
