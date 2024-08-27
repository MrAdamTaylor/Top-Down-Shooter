using System;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class WeaponBonus : Bonus
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private int _ammoBonus;
        private WeaponController _weaponController;

        private void Start()
        {
            _weaponController = (WeaponController)ServiceLocator.Instance.GetData(typeof(WeaponController));
            Subscribe();
        }

        private void Subscribe()
        {
            _touchTriger.OnTouch += AddWeaponBonus;
        }

        private void AddWeaponBonus()
        {
            _weapon.gameObject.AddComponent<Ammo>().AddAmmo(_ammoBonus);
        }

        private void OnDestroy()
        {
            _touchTriger.OnTouch -= AddWeaponBonus;
        }
    }
}