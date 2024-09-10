using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class AIMover : AIPart
{
    [SerializeField] private Transform _goal;
    [SerializeField] private Transform _followedTransform;
    [SerializeField] private float _speed = 2f;
    private Vector3 _tempPosition;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;
    private Vector3 _lastVelosity;

    public void CalculateDirection()
    {
        _tempPosition = _followedTransform.position.ExcludeY(); 
        _direction = _goal.position.ExcludeY() - _tempPosition.ExcludeY();
        _velocity = _direction.normalized * _speed;
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }

    public void DoStep()
    {
        _followedTransform.position += _velocity * Time.deltaTime;
        _lastVelosity = _velocity;
    }

    public Vector3 GetLastVelocity()
    {
        return _lastVelosity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }
}