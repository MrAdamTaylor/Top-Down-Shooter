using System.Collections;
using UnityEngine;

public class RotateSystem : MonoBehaviour
{
    public Transform Body;
    public Transform LookedObject;
    [Range(0,10)]public float RotateSpeed = 1.5f;
    
    private bool _autoRotate = false;
    private bool _coroutineRotate = false;
    private IEnumerator _rotateCoroutine;

    public void OnStart()
    {
        _rotateCoroutine = Rotate();
        _autoRotate = true;
    }

    public void Update()
    {
        if (_autoRotate)
        {
            AutoRotate();
        }
    }
    
    private IEnumerator Rotate()
    {
        while (_autoRotate)
        {
            yield return null;
            yield return null;
            yield return null;
            CalculateAngle(true);
        }
    }

    private void AutoRotate()
    {
        if (_coroutineRotate == false)
        {
            StartCoroutine(_rotateCoroutine);
            _coroutineRotate = true;
        }
    }
    
    private void CalculateAngle(bool makeRotate = false)
    {
        var transform1 = Body.transform;
        Vector3 forwardDirection = transform1.forward;
        Vector3 goalDirection = LookedObject.transform.position - transform1.position;
        goalDirection = goalDirection.normalized;
            
        float dotXZ = GeometryMath.DotProductXZ(forwardDirection, goalDirection);
            
        forwardDirection = forwardDirection.ExcludeY();
        goalDirection = goalDirection.ExcludeY();
            
        float angleXY = Mathf.Acos(dotXZ / (forwardDirection.magnitude * goalDirection.magnitude));

        int clockwise = -1;
        if (GeometryMath.CrossProduct(forwardDirection, goalDirection).y < 0)
            clockwise = 1;

        if (makeRotate)
        {
            if((angleXY * Mathf.Rad2Deg) > 10)
                Body.transform.Rotate(0,angleXY * Mathf.Rad2Deg * clockwise * RotateSpeed *Time.deltaTime,0);
        }
    }
}