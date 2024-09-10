using UnityEditor;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class BafSpawnPositions : MonoBehaviour
    {
        [SerializeField] private Transform _provocateur;
        [SerializeField] private float _minimalRadiusDiaposone = 4;
        [SerializeField] private float _maximumRadiusDiaposone = 12;

        private Vector3 _center;
        private Vector3 _currentPosition;
        private Vector3 _provocateurPosition;

        private bool _isInsideCheck;

        private bool _isInside;

        private void Awake()
        {
            ServiceLocator.Instance.BindData(typeof(BafSpawnPositions), this);
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(_center, Vector3.up, _minimalRadiusDiaposone);
            Handles.DrawWireDisc(_center, Vector3.up, _maximumRadiusDiaposone);
        }

        public bool Position(Vector3 otherPosition)
        {
            Vector3 center = transform.position.ExcludeY();
            
            Vector3 provoceuterPos = otherPosition.ExcludeY();
            Vector3 delta = center - provoceuterPos;
            
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;

            bool moreMinimal = sqrDistance >= _minimalRadiusDiaposone * _minimalRadiusDiaposone;
            bool lessMaximal = sqrDistance <= _maximumRadiusDiaposone * _maximumRadiusDiaposone;

            _isInside = moreMinimal && lessMaximal;

            return _isInside;
        }

    }
}
