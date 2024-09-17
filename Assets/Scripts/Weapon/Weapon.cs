using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //TODO - понадобится для будущего рефактора
    public WeaponType TypeWeapon;
    
    //public int Damage;
    [SerializeField] public Transform ShootPoint;
    //[SerializeField] protected float _speed_fire_range;

    [SerializeField] protected ShootControlSystem _shootSystem;

    [SerializeField] private WeaponCharacteristics Characteristics;
    
    public virtual void Construct(ShootControlSystem shootControlSystem, WeaponStaticData data)
    {
        _shootSystem = shootControlSystem;
        Characteristics.Damage = data.Damage;
        Characteristics.FireSpeedRange = data.SpeedFireRange;
    }

    /*public virtual void Awake()
    {
        ShootData data = new ShootData(Damage, ShootPoint, _speed_fire_range);
        _shootSystem.ConstructShootSystem(data);
    }*/

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
