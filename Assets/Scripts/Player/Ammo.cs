using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int _ammoCount;
    [SerializeField] private bool _infinity;
    private int _currentAmmo;
    
    public string SetCurrentAmmo()
    {
        if (_infinity)
        {
            return "Infinity";
        }
        else
        {
            return Convert.ToString(_currentAmmo);
        }
    }

    private void Start()
    {
        _currentAmmo = _ammoCount;
    }

    public void WasteAmmo()
    {
        if(!_infinity)
            _currentAmmo -= 1;
    }

    public bool CanShoot()
    {
        if (_currentAmmo > 0 || _infinity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddAmmo(int ammoBonus)
    {
        _currentAmmo += ammoBonus;
    }
}