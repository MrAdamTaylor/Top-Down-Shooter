using System;
using UnityEngine;

public class SpeedFinder : AIComponent
{
    private Vector3 _lastPosition;
    private float _speed;
    public Vector3 Velocity { get; set; }

    private Vector3 offset;

    public override void OnStart()
    {
        base.OnStart();
    }

    private void Update()
    {
        _speed = (transform.position - _lastPosition).magnitude / Time.deltaTime;
        _lastPosition = transform.position;
        transform.position += offset * Time.deltaTime;
    }

    public Vector3 GetVelocity()
    {
        return _lastPosition;
    }

    public void SetVelocity(Vector3 intersect)
    {
        offset = intersect;
    }
}