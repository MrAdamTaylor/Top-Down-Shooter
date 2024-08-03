using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage;
    [SerializeField] public Transform ShootPoint;
    [SerializeField] protected float _speed_fire_range;
}