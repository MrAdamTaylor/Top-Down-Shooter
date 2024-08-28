using System;
using UnityEngine;

public class ShootSystemOnly : CoomoonShootSystem
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _distance;

    private void Start()
    {
        _realBulletSpeed = this._bulletSpeed;
        _realDistance = _distance;
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}



public class MouseInputSystem
{
    
}