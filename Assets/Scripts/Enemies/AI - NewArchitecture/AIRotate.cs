using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class AIRotate : AIComponent
{
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _lookedObject;
    [SerializeField] private float _rotateSpeed = 2;
    
    public void MakeRotate(bool makeRotate = false)
    {
        Transform transform1 = _body;
        Vector3 forwardDirection = transform1.forward;
        Vector3 goalDirection = _lookedObject.position - transform1.position;
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