using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPistol : MonoBehaviour
{
    public int Damage;
    [SerializeField] public Transform ShootPoint;
    [SerializeField] private float _speed_fire_range;

    [SerializeField] private ShootControlSystem _shootControlSystem;

    public void Awake()
    {
        ShootData data = new ShootData(Damage, ShootPoint, _speed_fire_range);
        _shootControlSystem.SetShootData(data);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        _shootControlSystem.Shoot();
    }
}
