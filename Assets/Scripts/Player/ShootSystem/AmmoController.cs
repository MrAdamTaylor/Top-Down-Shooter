using System;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

[RequireComponent(typeof(ShootControlSystem), typeof(Weapon))]
public class AmmoController : MonoBehaviour
{
    public Action<long, bool> ChangeAmmo;
    
    private long _ammoCount;
    private bool _infinity;
    private ShootControlSystem _shootControlSystem;

    private long _ammoWaste;
    private long _currentAmmo;
    private AmmoStorage _ammoStorage;
    private Weapon _weapon;

    public void Construct(ShootControlSystem shootControlSystem, AmmoCharacteristics characteristics)
    {
        if (characteristics.WastedAmmo.IsNull())
            _ammoWaste = Constants.STANDART_WASTE_WALUE;
        else
            _ammoWaste = characteristics.WastedAmmo;

        _shootControlSystem = shootControlSystem;
        _weapon = gameObject.GetComponent<Weapon>();
        _ammoStorage = (AmmoStorage)ServiceLocator.Instance.GetCloneData(typeof(AmmoStorage));
        _infinity = characteristics.IsInfinity;
        if (_infinity)
            _currentAmmo = -long.MaxValue;
        else
            _currentAmmo = characteristics.StarterAmmo;
        _ammoStorage.Construct(_currentAmmo);
        _shootControlSystem.ShootAction += WasteAmmo;
    }

    void OnDestroy()
    {
        _shootControlSystem.ShootAction -= WasteAmmo;
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

    public void AddAmmo(long ammoBonus)
    {
        if (_infinity)
        {
            return;
        }
        else
        {
            ChangeAmmo?.Invoke(_currentAmmo, _infinity);
            _currentAmmo += ammoBonus;
            _ammoStorage.AddAmmo(ammoBonus);
        }
    }

    public Weapon GetWeaponType()
    {
        return _weapon;
    }

    public AmmoStorage ReturnStorage()
    {
        return _ammoStorage;
    }

    private void WasteAmmo()
    {
        if (!_infinity)
        {
            _currentAmmo -= _ammoWaste;
            Debug.Log("Current Ammo: "+_currentAmmo);
            ChangeAmmo?.Invoke(_currentAmmo, _infinity);
            _ammoStorage.SpendAmmo(_ammoWaste);
        }
        else
        {
            return;
        }
    }
}