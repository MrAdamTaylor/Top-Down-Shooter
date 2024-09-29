using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class WeaponBonus : Bonus
    {
        [SerializeField] private long _ammoBonus;
        [SerializeField] private WeaponType _weaponType;
        private Weapon _weapon;
        private WeaponController _weaponController;
        private AmmoController _ammoController;

        void Start()
        {
            _weaponController = (WeaponController)ServiceLocator.Instance.GetData(typeof(WeaponController));
            Subscribe();
            _weapon.gameObject.TryGetComponent(out _ammoController);
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
            _ammoController.AddAmmo(_ammoBonus);
        }
    }
}