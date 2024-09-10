using UnityEngine;

namespace Mechanics.DebafMechanics
{
    public class ZoneSpawnerTrigger : MonoBehaviour
    {
        [SerializeField] private float _minRadius = 3;
        public float MaxRadius = 9;
        
        private Vector3 _currentPosition;
        private Vector3 _center;

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
