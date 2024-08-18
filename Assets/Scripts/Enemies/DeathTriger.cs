using UnityEditor;
using UnityEngine;

namespace Enemies
{
    public class DeathTriger : MonoBehaviour
    {
        public Transform Killed;

        [SerializeField] private float _radius;
    
        private bool _isInside;

        private Death _death;

        public void OnDrawGizmos()
        {
            Vector3 center = transform.position.ExcludeY();
        
            if(Killed == null)
                return;

            Vector3 provoceuterPos = Killed.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            //_killed.position
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            Handles.color = _isInside ? Color.green : Color.red;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
        }

        public void Update()
        {
            Vector3 center = this.transform.position.ExcludeY();
        
            if(Killed == null)
                return;

            Vector3 provoceuterPos = Killed.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
        
            //_killed.position
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            _isInside = sqrDistance <= _radius * _radius;
        
            if (_isInside)
            {
                _death = Killed.gameObject.GetComponent<Death>();
                _death.MakeDeath();
                _death = null;
            }
        }
    }
}
