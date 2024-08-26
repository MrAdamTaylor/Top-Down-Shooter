using System;

namespace Mechanics.BafMechaniks
{
    public class WeaponBonus : Bonus
    {
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
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            _touchTriger.OnTouch -= AddWeaponBonus;
        }
    }
}