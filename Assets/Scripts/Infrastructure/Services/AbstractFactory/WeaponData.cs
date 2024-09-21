using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class WeaponData
{
    private Dictionary<int, WeaponType> _weaponTypes = new();
    private Dictionary<int, AmmoController> _weaponAmmo = new();
    
    
    public void AddWeaponTypeWithIndex(WeaponType weaponType, int index)
    {
        _weaponTypes.Add(index,weaponType);
    }

    public void AddAmmoWithIndex(int index, AmmoController controller=null)
    {
        if(controller != null)
            _weaponAmmo.Add(index, controller);
    }

    public Dictionary<int, (WeaponType, AmmoController)> GetAmmoData()
    {
        if (_weaponAmmo.Count != 0)
        {
            Dictionary<int, (WeaponType, AmmoController)> _ammoDictionary = new();
            int k = 0;
            for (int i = 0; i < _weaponTypes.Count; i++)
            {
                if (_weaponAmmo.Keys.Contains(i))
                {
                    Debug.Log("Ammo Dictionary added Weapon Ammo: "+ _weaponAmmo[i] + " added Weapon Type"+_weaponTypes);
                    _ammoDictionary.Add(k, (_weaponTypes[i], _weaponAmmo[i]));
                    k++;
                }
            }

            return _ammoDictionary;
        }
        else
        {
            throw new WarningException("Not AmmoData");
        }
        
    }
}