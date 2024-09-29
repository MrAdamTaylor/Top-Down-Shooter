using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType TypeWeapon;
    
    [SerializeField] public Transform ShootPoint;

    private ShootControlSystem _shootSystem;

    [SerializeField] private WeaponCharacteristics Characteristics;
    
    public virtual void Construct(ShootControlSystem shootControlSystem, WeaponStaticData data)
    {
        _shootSystem = shootControlSystem;
        Characteristics.Damage = data.Damage;
        Characteristics.FireSpeedRange = data.SpeedFireRange;
    }

    public void Fire()
    {
        _shootSystem.UpdateValues(Characteristics);
        _shootSystem.Shoot();
    }
}

[Serializable]
public struct WeaponCharacteristics
{
    public int Damage;
    public float FireSpeedRange;
}
