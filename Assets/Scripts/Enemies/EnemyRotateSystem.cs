using System.Collections;
using EnterpriceLogic.Constants;
using UnityEngine;

public class EnemyRotateSystem : MonoBehaviour
{
    private Transform _currentBody;
    private Transform _lookedObject;
    private float _rotateSpeed;
    
    private Coroutine _coroutineRotate;

    private bool _autoRotate;

    public void Construct(Transform currentBody, Transform lookedObject, float rotateSpeed)
    {
        _lookedObject = lookedObject;
        _currentBody = currentBody;
        _rotateSpeed = rotateSpeed;
        _autoRotate = true;
    }


    public void RotateStart()
    {
        _coroutineRotate ??= StartCoroutine(RotateDirection());
    }

    public void RotateStop()
    {
        if (_coroutineRotate == null) 
            return;
        StopCoroutine(_coroutineRotate);
        _coroutineRotate = null;

    }

    private IEnumerator RotateDirection()
    {
        while (_autoRotate)
        {
            yield return null;
            yield return null;
            yield return null;
            Rotate();
        }
    }
    
    private void Rotate()
    {
        var rotateTurple = CalculateRotate();
        if (rotateTurple.Item1 * Mathf.Rad2Deg > Constants.POSIBLE_ROTATION_ANGLE_DEVIANT)
        {
            _currentBody.transform.Rotate(0, rotateTurple.Item1 * Mathf.Rad2Deg * rotateTurple.Item2 * 
                                             _rotateSpeed * Time.deltaTime, 0);
        }
    }

    private (float angle, int clockwise) CalculateRotate()
    {
        Vector3 forwardDirection = _currentBody.forward;
        Vector3 goalDirection = _lookedObject.position - _currentBody.position;
        goalDirection = goalDirection.normalized;
        forwardDirection = forwardDirection.ExcludeY();
        goalDirection = goalDirection.ExcludeY();
        float dotXZ = GeometryMath.DotProductXZ(forwardDirection, goalDirection);
        float angleXY = Mathf.Acos(dotXZ / (forwardDirection.magnitude * goalDirection.magnitude));
        int clockwise = -Constants.STANDART_CLOCKWISE_VALUE;
        if (GeometryMath.CrossProduct(forwardDirection, goalDirection).y < 0)
            clockwise = Constants.STANDART_CLOCKWISE_VALUE;
        return (angleXY, clockwise);
    }
}