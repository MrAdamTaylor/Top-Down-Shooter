using System;
using System.Collections.Generic;
using UnityEngine;

//TODO - this code using before refactoring
public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponSwitching _weaponSwitching;
    private IMouseInput _inputSystem;

    private List<IMouseInput> _mouseInputs;
    private int _weaponsCount;
    private WeaponInputController _weaponInputController;
    private AmmoAdapter _ammoAdapter;
    
    void Awake()
    {
        ServiceLocator.Instance.BindData(typeof(WeaponController), this);
        _weaponInputController = gameObject.AddComponent<WeaponInputController>();
        _weaponSwitching.Construct(this);
        _weaponInputController.GetWeapons(_weaponSwitching.GetWeaponsComponent());
    }

    void Start()
    {
        _ammoAdapter = (AmmoAdapter)ServiceLocator.Instance.GetData(typeof(AmmoAdapter));
        FindClickSystem();
    }

    void OnDestroy()
    {
        _inputSystem.OnFire -= OnShoot;
    }

    public void SwitchInput(Weapon weaponObject)
    {
        _inputSystem.OnFire -= OnShoot;
        _inputSystem = null;
        FindClickSystem(weaponObject);
    }

    private void FindClickSystem()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
            _inputSystem = _weaponInputController.FindEqual(inputSystem);
            _inputSystem.OnFire += OnShoot;
        }
    }

    private void FindClickSystem(Weapon weapon)
    {
        Type inputSystem = weapon.gameObject.GetComponent<IMouseInput>().GetType();
        _inputSystem = _weaponInputController.FindEqual(inputSystem);
        _inputSystem.OnFire += OnShoot;
    }

    private void OnShoot()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            if (weapon.gameObject.GetComponent<AmmoController>().CanShoot())
            {
                weapon.Fire();
            }
        }
        else
        {
            throw new Exception("Script of switching weapon disable!");
        }
    }
    
}