using EnterpriceLogic.Utilities;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Player.NewWeaponControllSystem
{
    public class WeaponSwitcher : MonoBehaviour
    {
        private CurrentWeaponConstructor _currentWeaponConstructor;
        private WeaponConteiner _conteiner;
        private PlayerAnimator _playerAnimator;
        private int _selectedWeapon;

        private bool _isConstructed = false;

        public void Construct(WeaponProvider provider, CurrentWeaponConstructor constructor)
        {
            _currentWeaponConstructor = constructor;
            _conteiner = new WeaponConteiner(provider.ReturnWeapons());
            _conteiner.IsNullWithException("Error on load WeaponConteiner");
            _isConstructed = true;
            _playerAnimator = (PlayerAnimator)ServiceLocator.Instance.GetData(typeof(PlayerAnimator));
        }
        /*private void Awake()
        {
            _playerAnimator = FindObjectOfType<PlayerAnimator>();
        }*/
        private void Update()
        {
            if (_isConstructed)
            {
                int previousSelectedWeapon = _selectedWeapon;
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (_selectedWeapon >= _conteiner.Count - 1)
                    {
                        _selectedWeapon = 0;
                    }
                    else
                    {
                        _selectedWeapon++;
                    }
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (_selectedWeapon <= 0)
                    {
                        _selectedWeapon = _conteiner.Count - 1;
                    }
                    else
                    {
                        _selectedWeapon--;
                    }
                }
                if (previousSelectedWeapon != _selectedWeapon)
                {
                    SelectWeapon();
                }
            }
        }

        public Weapon.Weapon GetActiveWeapon()
        {
            return _conteiner.GetActiveWeapon();
        }

        private void SelectWeapon()
        {
            for (int i = 0; i < _conteiner.Count; i++)
            {
                if (i == _selectedWeapon)
                {
                    Weapon.Weapon weapon = _conteiner.GetByIndex(i);
                    weapon.gameObject.SetActive(true);
                    _currentWeaponConstructor.SwitchInput(weapon);
                    
                    if (_playerAnimator != null)
                    {
                        bool isBigGun = weapon.TypeWeapon == Weapon.WeaponType.ShootGun || weapon.TypeWeapon == Weapon.WeaponType.Rifle;
                        _playerAnimator.SetWeaponType(isBigGun);
                    }
                    else
                    {
                        Debug.LogError("_playerAnimator is not assigned in WeaponSwitcher.");
                    }
                }
                else
                {
                    Weapon.Weapon weapon = _conteiner.GetByIndex(i);
                    weapon.gameObject.SetActive(false);
                }
            }
        }

    }
}