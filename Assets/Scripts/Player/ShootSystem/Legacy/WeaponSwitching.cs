using System;
using System.Collections.Generic;
using UnityEngine;

//TODO - this code using before refactoring
public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    private int _selectedWeapon;
    private int _weaponCount = 0;

    private WeaponController _weaponController;

    public void Construct(WeaponController weaponController)
    {
        _weaponController = weaponController;
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weaponCount++;
        }
    }

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

    private void SelectWeapon()
    {
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
        }
    }
}