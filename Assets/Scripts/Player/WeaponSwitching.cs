using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public List<Weapon> weapons;
    private int selectedWeapon;

    private WeaponController _weaponController;

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weapons.Count - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Count-1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int j = 0;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
            {
                weapons[i].gameObject.SetActive(true);
                _weaponController.SwitchInput(weapons[i]);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
            j++;
        }
    }

    public Weapon GetActiveWeapon()
    {
        Weapon weapon = null;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].gameObject.activeSelf)
            {
                weapon = weapons[i];
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

    public void Construct(WeaponController weaponController)
    {
        _weaponController = weaponController;
    }
}