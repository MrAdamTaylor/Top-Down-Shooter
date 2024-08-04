using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponSwitching _weaponSwitching;
    private IMouseInput _inputSystem;
    void Start()
    {
        FindClickSystem();
        _weaponSwitching.Construct(this);
    }

    private void FindClickSystem()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            IMouseInput inputSystem = weapon.gameObject.GetComponent<MouseInputClick>();
            if (inputSystem != null)
            {
                //inputSystem  = weapon.gameObject.GetComponent<MouseInputTouch>();
                _inputSystem = this.AddComponent<MouseInputClick>();
                Debug.Log("Система c зажатием клавиши найдена в "+weapon.gameObject.name);
            }
            else
            {
                _inputSystem = this.AddComponent<MouseInputTouch>();
                //inputSystem  = weapon.gameObject.GetComponent<MouseInputClick>();
                Debug.Log("Система c одиночным нажатием найдена в "+weapon.gameObject.name);
            }
            _inputSystem.OnFire += this.OnShoot;
        }
    }

    private void FindClickSystem(Weapon weapon)
    {
        IMouseInput inputSystem = weapon.gameObject.GetComponent<MouseInputClick>();
        if (inputSystem != null)
        {
            //inputSystem  = weapon.gameObject.GetComponent<MouseInputTouch>();
            _inputSystem = this.AddComponent<MouseInputClick>();
            Debug.Log("Система c зажатием клавиши найдена в "+weapon.gameObject.name);
        }
        else
        {
            _inputSystem = this.AddComponent<MouseInputTouch>();
            //inputSystem  = weapon.gameObject.GetComponent<MouseInputClick>();
            Debug.Log("Система c одиночным нажатием найдена в "+weapon.gameObject.name);
        }
        _inputSystem.OnFire += this.OnShoot;
    }

    public void SwitchInput(Weapon weaponObject)
    {
        _inputSystem.OnFire -= this.OnShoot;
        _inputSystem = null;
        FindClickSystem(weaponObject);
    }

    void Update()
    {
        
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
            weapon.Fire();
        }
        else
        {
            throw new Exception("Скрипт смены оружия отключён!");
        }
    }
}
