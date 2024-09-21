using System;
using EnterpriceLogic.Utilities;
using UnityEngine;

namespace Scripts.Player.NewWeaponControllSystem
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponSwitcher _weaponSwitcher;
        private WeaponInputHandler _weaponInputHandler;
        private Weapon _weapon;
        private IMouseInput _mouseInput;
        private AmmoController _ammoController;
        private AmmoAdapter _ammoAdapter;
        
        public void Construct(Weapon getActiveWeapon)
        {
            _weapon = getActiveWeapon;
            _mouseInput = _weapon.gameObject.GetComponent<IMouseInput>();
            _mouseInput.OnFire += OnShoot;
            _ammoController = _weapon.GetComponent<AmmoController>();
        }

        public void Construct(Weapon[] getActiveWeapon, WeaponSwitcher weaponSwitcher)
        {
            _weaponInputHandler = new WeaponInputHandler(this, getActiveWeapon);
            _weaponSwitcher = weaponSwitcher;
            ConstructActiveWeapon();
        }

        private void OnDestroy()
        {
            _mouseInput.OnFire -= OnShoot;
        }

        public void CleanInputSystem()
        {
            _mouseInput.OnFire -= OnShoot;
            _mouseInput = null;
        }

        public void ConnectInputToWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _ammoController = _weapon.GetComponent<AmmoController>();
            Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _mouseInput = _weaponInputHandler.FindEqual(inputSystem);
            _mouseInput.OnFire += OnShoot;
            if (!_ammoAdapter.IsNull())
            {
                _ammoAdapter.UpdateUI(_weapon.TypeWeapon);
                _ammoAdapter.UpdatePicture(_weapon.TypeWeapon);
            }
        }

        public void ConstructUI(AmmoAdapter ammoAdapter)
        {
            _ammoAdapter = ammoAdapter;
            _ammoAdapter.UpdateUI(_weapon.TypeWeapon);
            _ammoAdapter.UpdatePicture(_weapon.TypeWeapon);
        }

        /*public (WeaponType, AmmoStorage) GetWeaponAmmo()
        {
            (WeaponType, AmmoStorage)result = ()
        }*/


        private void ConstructActiveWeapon()
        {
            _weapon = _weaponSwitcher.GetActiveWeapon();
            Type inputSystem = _weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _mouseInput = _weaponInputHandler.FindEqual(inputSystem);
            _mouseInput.OnFire += OnShoot;
        }

        private void OnShoot()
        {
            if (_ammoController.IsNull())
            {
                _weapon.Fire();
            }
            else
            {
                if (_ammoController.CanShoot())
                {
                    _weapon.Fire();
                    if(!_ammoAdapter.IsNull())
                        _ammoAdapter.UpdateUI(_weapon.TypeWeapon);
                }

            }
        }
    }
}
