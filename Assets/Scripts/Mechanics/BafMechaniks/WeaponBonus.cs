using System;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle,
    ShootGun,
    Undefinded
}

namespace Mechanics.BafMechaniks
{
    public class WeaponBonus : Bonus
    {
        [SerializeField] private int _ammoBonus;
        [SerializeField] private WeaponType _weaponType;
        private Weapon _weapon;
        private WeaponController _weaponController;
        private Ammo _ammo;

        private void Start()
        {
            _weaponController = (WeaponController)ServiceLocator.Instance.GetData(typeof(WeaponController));
            Subscribe();
            _weapon = _weaponController.GetWeaponByType(_weaponType);
            Debug.Log(_weapon.gameObject.name + " is Aded to WeaponBonus");
            _weapon.gameObject.TryGetComponent<Ammo>(out _ammo);
        }

        private void Subscribe()
        {
            _touchTriger.OnTouch += AddWeaponBonus;
        }

        private void AddWeaponBonus()
        {
            _ammo.AddAmmo(_ammoBonus);
        }

        private void OnDestroy()
        {
            _touchTriger.OnTouch -= AddWeaponBonus;
        }
    }
}