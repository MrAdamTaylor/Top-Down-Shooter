using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateSystem : MonoBehaviour
{
    [SerializeField]private Transform _body;

    [Range(0,10)]
    [SerializeField]
    private float _rotateSpeed = 1.5f;

    private Transform _lookedObject;

    private bool _autoRotate = false;
    private bool _coroutineRotate = false;

    public void OnStart()
    {
        _autoRotate = true;
    }

    public void OnEnable()
    {
        Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        _lookedObject = player.transform;
    }

    public void Update()
    {
        if (_autoRotate)
        {
            AutoRotate();
        }
    }

    public void Stop()
    {
        StopCoroutine(Rotate());
        _coroutineRotate = false;
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
            StartCoroutine(Rotate());
            _coroutineRotate = true;
        }
    }
    
    private void CalculateAngle(bool makeRotate = false)
    {
        var transform1 = _body.transform;
        Vector3 forwardDirection = transform1.forward;
        Vector3 goalDirection = _lookedObject.transform.position - transform1.position;
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
                _body.transform.Rotate(0,angleXY * Mathf.Rad2Deg * clockwise * _rotateSpeed *Time.deltaTime,0);
        }
    }
}