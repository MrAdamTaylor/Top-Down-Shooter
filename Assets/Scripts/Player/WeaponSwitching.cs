using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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

    public int GetWeaponsGount()
    {
        return _weaponCount;
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

    void SelectWeapon()
    {
        int j = 0;
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
            j++;
        }
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
            throw new Exception("Нет активного оружия");
        }
        else
        {
            return weapon;
        }
    }
}