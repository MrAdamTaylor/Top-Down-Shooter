using UnityEditor;
using UnityEngine;

namespace Mechanics.DebafMechanics
{
    public class ZoneSpawnerTrigger : MonoBehaviour
    {
        [SerializeField] private float _minRadius = 3;
        public float MaxRadius = 9;
        [SerializeField] private GameObject _provocateur;
        private Vector3 _currentPosition;
        private Vector3 _center;

        public bool IsInside { get; set; }

        private void OnDrawGizmos()
        {
            Vector3 center = transform.position.ExcludeY();
        
            if(_provocateur == null)
                return;

            Vector3 provoceuterPos = _provocateur.transform.position.ExcludeY();
            Vector3 delta = center - provoceuterPos;
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            bool isInside2 = _minRadius * _minRadius <= sqrDistance;
            bool isInside1 = sqrDistance <= MaxRadius * MaxRadius;

            bool isInside = isInside1 && isInside2;
        
            //Handles.color = IsInside ? Color.blue : Color.red;
            Handles.color = isInside ? Color.magenta : Color.blue;
            Handles.DrawWireDisc(this.transform.position, Vector3.up,_minRadius);
            Handles.DrawWireDisc(this.transform.position, Vector3.up, MaxRadius);
        }

        public bool CheckPosition(Vector3 vec)
        {
            Vector3 center = transform.position.ExcludeY();
            Vector3 delta = center - vec;
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            bool isInside2 = _minRadius * _minRadius <= sqrDistance;
            bool isInside1 = sqrDistance <= MaxRadius * MaxRadius;
            bool isInside = isInside1 && isInside2;
            return isInside;
        }
    }
}
