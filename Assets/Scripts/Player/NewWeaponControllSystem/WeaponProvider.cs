using System;
using UnityEngine;

public class WeaponProvider : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;

    public Weapon[] ReturnWeapons()
    {
        Weapon[] weapons = new Weapon[_weapons.Length];

        for (int i = 0; i < _weapons.Length; i++)
        {
            Weapon weapon = _weapons[i].gameObject.GetComponent<Weapon>();
            weapons[i] = weapon;
        }

        return weapons;
    }
}
