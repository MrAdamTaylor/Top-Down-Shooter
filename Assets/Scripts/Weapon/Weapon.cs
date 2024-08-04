using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;
    [SerializeField] public Transform ShootPoint;
    [SerializeField] protected float _speed_fire_range;

    [SerializeField] protected CoomoonShootSystem _shootSystem;

    public virtual void Awake()
    {
        ShootData data = new ShootData(Damage, ShootPoint, _speed_fire_range);
        _shootSystem.Construct(data);
    }

    public void Fire()
    {
        _shootSystem.Shoot();
    }
}