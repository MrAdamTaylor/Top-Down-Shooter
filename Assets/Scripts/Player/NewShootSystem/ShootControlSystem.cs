using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControlSystem : MonoBehaviour
{
    public Action ShootAction;
    [SerializeField] private CommonShootSystem _shootSystem;

    private ShootData _weaponData;
    
    private float _lastShootTime;
    private float _realDistance = Constants.DEFAULT_MAXIMUM_FIRING_RANGE;
    private float _realBulletSpeed = Constants.DEFAULT_BULLET_SPEED;

    private bool _isShooting;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (_lastShootTime + _weaponData.Delay < Time.time)
        {
            Debug.Log("Производиться выстрел");
            _shootSystem.Shoot();
            ShootAction?.Invoke();
            _lastShootTime = Time.time;
        }
    }

    public void SetShootData(ShootData data)
    {
        _weaponData = data;
        _shootSystem.Construct(_weaponData);
    }
}
