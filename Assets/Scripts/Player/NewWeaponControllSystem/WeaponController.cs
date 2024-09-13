using System;
using UnityEngine;

namespace Scripts.Player.NewWeaponControllSystem
{
    public class WeaponController : MonoBehaviour
    {
        private WeaponSwitcher _weaponSwitcher;
        private WeaponInputHandler _weaponInputHandler;
        private Weapon _weapon;
        private IMouseInput _mouseInput;
        
        public void Construct(Weapon getActiveWeapon)
        {
            _weapon = getActiveWeapon;
            _mouseInput = _weapon.gameObject.GetComponent<IMouseInput>();
            _mouseInput.OnFire += OnShoot;
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
            Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _mouseInput = _weaponInputHandler.FindEqual(inputSystem);
            _mouseInput.OnFire += OnShoot;
        }

        private void ConstructActiveWeapon()
        {
            _weapon = _weaponSwitcher.GetActiveWeapon();
            Type inputSystem = _weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _mouseInput = _weaponInputHandler.FindEqual(inputSystem);
            _mouseInput.OnFire += OnShoot;
        }

        private void OnShoot()
        {
            _weapon.Fire();
        }
    }
}
