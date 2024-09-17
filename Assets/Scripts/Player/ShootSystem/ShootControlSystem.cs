using System;
using UnityEngine;

public class ShootControlSystem : MonoBehaviour
{
    [SerializeField] private CoomoonShootSystem _shootSystem;
    
    public Action ShootAction;
    
    private float _weaponDelay;
    private float _lastShootTime;

    public void Construct(WeaponStaticData data, WeaponEffectsConteiner weaponEffectsConteiner, CoomoonShootSystem shootSystem)
    {
        _weaponDelay = data.SpeedFireRange;
        _shootSystem = shootSystem;
        _shootSystem.Construct(data, weaponEffectsConteiner);
    }

    /*public void ConstructShootSystem(ShootData data)
    {
        _weaponDelay = data.Delay;
        _shootSystem.Construct(data);
    }*/


    public void Shoot()
    {
        if (_lastShootTime + _weaponDelay < Time.time)
        {
            _shootSystem.Shoot();
            ShootAction?.Invoke();
            _lastShootTime = Time.time;
        }
    }

    public void UpdateValues(WeaponCharacteristics characteristics)
    {
        _weaponDelay = characteristics.FireSpeedRange;
        _shootSystem.UpdateValues(characteristics);
    }
}
