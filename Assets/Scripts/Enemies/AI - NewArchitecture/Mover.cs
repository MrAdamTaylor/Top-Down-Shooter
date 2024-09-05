using UnityEngine;

public class Mover : AIComponent
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    
    private Vector3 _desiredVelocity;
    private bool _reached;

    public override void OnStart()
    {
        ComputeDesiredVelocity();
    }

    public Vector3 ComputeDesiredVelocity()
    {
        if (!_reached)
        {
            Vector3 diff3 = _target.position - transform.position;
            _desiredVelocity = diff3;
            Debug.Log("Dezired Velocity is "+_desiredVelocity + " for "+this.gameObject.name);
            _desiredVelocity.Normalize();
            Debug.Log("Dezired Velocity after normalize "+_desiredVelocity + "for " + this.gameObject.name);
            Vector3 VMax = new Vector3(_speed, 0f, _speed);
            _desiredVelocity.Scale(VMax);
            Debug.Log("Dezired Velocity after scale VMax"+_desiredVelocity+ "for " + this.gameObject.name);
        }
        else
        {
            _desiredVelocity = Vector2.zero;
        }
        return _desiredVelocity;
    }
    
    public Vector3 GetVelocity()
    {
        return _desiredVelocity;
    }
}