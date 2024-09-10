using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class WeaponBonus : Bonus
    {
        [SerializeField] private int _ammoBonus;
        [SerializeField] private WeaponType _weaponType;
        private Weapon _weapon;
        private WeaponController _weaponController;
        private Ammo _ammo;

        void Start()
        {
            _weaponController = (WeaponController)ServiceLocator.Instance.GetData(typeof(WeaponController));
            Subscribe();
            _weapon = _weaponController.GetWeaponByType(_weaponType);
            _weapon.gameObject.TryGetComponent(out _ammo);
        }

        void OnDestroy()
        {
            _touchTriger.OnTouch -= AddWeaponBonus;
        }

        private void Subscribe()
        {
            _touchTriger.OnTouch += AddWeaponBonus;
        }

        private void AddWeaponBonus()
        {
            _ammo.AddAmmo(_ammoBonus);
        }
    }
}