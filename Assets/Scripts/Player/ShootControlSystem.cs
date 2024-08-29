using System;
using UnityEngine;

public class ShootControlSystem : MonoBehaviour
{
    public Action ShootAction;
    [SerializeField] private CoomoonShootSystem _shootSystem;
    private float _weaponDelay;
    private float _lastShootTime;
    public void ConstructShootSystem(ShootData data)
    {
        _weaponDelay = data.Delay;
        _shootSystem.Construct(data);
    }

    public void Shoot()
    {
        if (_lastShootTime + _weaponDelay < Time.time)
        {
            _shootSystem.Shoot();
            ShootAction?.Invoke();
            _lastShootTime = Time.time;
        }
    }
}
