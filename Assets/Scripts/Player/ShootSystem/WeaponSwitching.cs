using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    private int _selectedWeapon;
    private int _weaponCount = 0;

    private WeaponController _weaponController;

    void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_selectedWeapon >= _weapons.Count - 1)
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
                _selectedWeapon = _weapons.Count-1;
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

    public void Construct(WeaponController weaponController)
    {
        _weaponController = weaponController;
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weaponCount++;
        }
    }

    public Weapon[] GetWeaponsComponent()
    {
        Weapon[] weapons = new Weapon[_weaponCount];
        
        for (int i = 0; i < _weapons.Count; i++)
        {
            Weapon weapon = _weapons[i].gameObject.GetComponent<Weapon>();
            weapons[i] = weapon;
        }

        return weapons;
    }

    public Weapon GetActiveWeapon()
    {
        Weapon weapon = null;
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i].gameObject.activeSelf)
            {
                weapon = _weapons[i];
            }
        }

        if (weapon == null)
        {
            throw new Exception("No active weapon");
        }
        else
        {
            return weapon;
        }
    }

    public Weapon FindByType(WeaponType weaponType)
    {
        Weapon weapon = null;
        for (int i = 0; i < _weapons.Count; i++)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    if (_weapons[i].GetType() == typeof(Pistol))
                        weapon = _weapons[i];
                    break;
                case WeaponType.ShootGun:
                    if (_weapons[i].GetType() == typeof(Shootgun))
                        weapon= _weapons[i];
                    break;
                case WeaponType.Rifle:
                    if (_weapons[i].GetType() == typeof(Rifle))
                        weapon = _weapons[i];
                    break;
                default:
                    throw new Exception("Don't known type of weapon");
            }
        }
        return weapon;
    }

    public WeaponType FindByClass(Weapon weapon)
    {
        switch (weapon)
        {
            case Pistol:
                return WeaponType.Pistol;
                break;
             case   Shootgun:
                return WeaponType.ShootGun;
                break;
             case Rifle:
                return WeaponType.Rifle;
                break;
             default:
                return WeaponType.Undefinded;
        }
    }

    private void SelectWeapon()
    {
        //int j = 0;
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (i == _selectedWeapon)
            {
                _weapons[i].gameObject.SetActive(true);
                _weaponController.SwitchInput(_weapons[i]);
            }
            else
            {
                _weapons[i].gameObject.SetActive(false);
            }
            //j++;
        }
    }
}