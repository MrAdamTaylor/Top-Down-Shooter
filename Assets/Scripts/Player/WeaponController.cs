using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponSwitching _weaponSwitching;
    private IMouseInput _inputSystem;

    private List<IMouseInput> _mouseInputs;
    private int _weaponsCount;
    private WeaponInputController _weaponInputController;
    
    private void Awake()
    {
        ServiceLocator.Instance.BindData(typeof(WeaponController), this);
        _weaponInputController = this.gameObject.AddComponent<WeaponInputController>();
    }

    void Start()
    {
        FindClickSystem();
        _weaponSwitching.Construct(this);
        _weaponsCount = _weaponSwitching.GetWeaponsGount();
        _weaponInputController.GetWeapons(_weaponSwitching.GetWeaponsComponent());
    }

    private void FindClickSystem()
    {
        if (_weaponSwitching != null)
        {
            Weapon weapon = _weaponSwitching.GetActiveWeapon();
            IMouseInput inputSystem = weapon.gameObject.GetComponent<MouseInputClick>();
            if (inputSystem != null)
            {
                _inputSystem = this.AddComponent<MouseInputClick>();
            }
            else
            {
                _inputSystem = this.AddComponent<MouseInputTouch>();
            }
            _inputSystem.OnFire += this.OnShoot;
        }
    }

    private void FindClickSystem(Weapon weapon)
    {
        IMouseInput inputSystem = weapon.gameObject.GetComponent<MouseInputClick>();
        if (inputSystem != null)
        {
            _inputSystem = this.AddComponent<MouseInputClick>();
        }
        else
        {
            _inputSystem = this.AddComponent<MouseInputTouch>();
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
            throw new Exception("Script of switching weapon disable!");
        }
    }
}

public class WeaponInputController : MonoBehaviour
{
    private List<IMouseInput> _mouseInputs = new List<IMouseInput>(); 
    
    private void Awake()
    {
        ServiceLocator.Instance.BindData(typeof(WeaponInputController), this);
    }

    public void GetWeapons(Weapon[] getWeaponsComponent)
    {
        for (int i = 0; i < getWeaponsComponent.Length; i++)
        {
            IMouseInput inputSystem = getWeaponsComponent[i].gameObject.GetComponent<MouseInputClick>();
            if (inputSystem != null)
            {
                MouseInputClick mouseClickSystem = this.AddComponent<MouseInputClick>();
                _mouseInputs.Add(mouseClickSystem);
            }
            else
            {
                MouseInputTouch mouseTouchSystem = this.AddComponent<MouseInputTouch>();
                _mouseInputs.Add(mouseTouchSystem);
            }
        }

        _mouseInputs = _mouseInputs.DistinctBy(x => x.GetType()).ToList();

        /*for (int i = 0; i < _mouseInputs.Count; i++)
        {
            Debug.Log($"System is "+_mouseInputs[i]);
        }*/
    }
}
