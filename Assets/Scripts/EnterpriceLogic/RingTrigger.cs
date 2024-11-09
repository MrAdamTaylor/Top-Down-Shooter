using UnityEditor;
using UnityEngine;

namespace EnterpriceLogic
{
    public class RingTrigger : MonoBehaviour
    {
        public float MaxRadiusAbs { get; private set; }

        private Transform _centerTransform;
        private float _minimalRadiusDiaposone;
        private float _maximumRadiusDiaposone;
        private bool _isConstructed;
        private bool _isInside;

        public void Construct(Transform centerTransform, float minRadius, float maxRadius)
        {
            _centerTransform = centerTransform;
            _minimalRadiusDiaposone = minRadius;
            _maximumRadiusDiaposone = maxRadius;
            MaxRadiusAbs = maxRadius;
            _isConstructed = true;
        }

        //void OnDrawGizmos()
        //{
        //    if(!_isConstructed)
        //        return;
        //    Handles.color = Color.cyan;
        //    Handles.DrawWireDisc(_centerTransform.position, Vector3.up, _minimalRadiusDiaposone);
        //    Handles.DrawWireDisc(_centerTransform.position, Vector3.up, _maximumRadiusDiaposone);
        //}
        
        public bool IsInRing(Vector3 triggerActivator)
        {
            Vector3 delta = _centerTransform.position - triggerActivator;
            Vector3 inverseDelta = transform.InverseTransformVector(delta);
            
            float sqrDistance = delta.x * delta.x + delta.z * delta.z;
            
            bool moreMinimal = sqrDistance >= _minimalRadiusDiaposone * _minimalRadiusDiaposone;
            bool lessMaximal = sqrDistance <= _maximumRadiusDiaposone * _maximumRadiusDiaposone;
            
            _isInside = moreMinimal && lessMaximal;
            return _isInside;
        }
    }
}