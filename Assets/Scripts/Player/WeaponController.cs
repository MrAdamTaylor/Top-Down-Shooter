using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponSwitching _weaponSwitching;
    [SerializeField] private PlayerUI _playerUI;
    private IMouseInput _inputSystem;

    private List<IMouseInput> _mouseInputs;
    private int _weaponsCount;
    private WeaponInputController _weaponInputController;
    private Ammo _ammo;
    
    private void Awake()
    {
        ServiceLocator.Instance.BindData(typeof(WeaponController), this);
        _weaponInputController = this.gameObject.AddComponent<WeaponInputController>();
        _weaponSwitching.Construct(this);
        _weaponsCount = _weaponSwitching.GetWeaponsGount();
        _weaponInputController.GetWeapons(_weaponSwitching.GetWeaponsComponent());
    }

    void Start()
    {
        FindClickSystem();
    }

    private void FindClickSystem()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _inputSystem = _weaponInputController.FindEqual(inputSystem);
            _inputSystem.OnFire += this.OnShoot;
            _ammo = weapon.GetComponent<Ammo>();
            _ammo.ChangeAmmo += _playerUI.UpdateAmmoText;
            _playerUI.UpdateAmmoText(weapon.GetComponent<Ammo>().GetAmmo(),weapon.GetComponent<Ammo>().IsInfinity());
        }
    }

    private void FindClickSystem(Weapon weapon)
    {
        Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
        _inputSystem = _weaponInputController.FindEqual(inputSystem);
        _inputSystem.OnFire += this.OnShoot;
        _ammo = weapon.GetComponent<Ammo>();
        _ammo.ChangeAmmo += _playerUI.UpdateAmmoText;
        _playerUI.UpdateAmmoText(weapon.GetComponent<Ammo>().GetAmmo(),weapon.GetComponent<Ammo>().IsInfinity());
    }

    public void SwitchInput(Weapon weaponObject)
    {
        _inputSystem.OnFire -= this.OnShoot;
        _inputSystem = null;
        _ammo.ChangeAmmo -= _playerUI.UpdateAmmoText;
        _ammo = null;
        FindClickSystem(weaponObject);
    }

    private void OnDestroy()
    {
        _inputSystem.OnFire -= this.OnShoot;
    }

    public void OnShoot()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            if (weapon.gameObject.GetComponent<Ammo>().CanShoot())
            {
                weapon.Fire();
            }
        }
        else
        {
            throw new Exception("Script of switching weapon disable!");
        }
    }

    public Weapon GetWeaponByType(WeaponType weaponType)
    {
        return _weaponSwitching.FindByType(weaponType);
    }
}