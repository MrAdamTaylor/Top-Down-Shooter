using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponInputController : MonoBehaviour
{
    private List<IMouseInput> _mouseInputs = new List<IMouseInput>();

    private List<List<int>> _indexes = new List<List<int>>();
    
    private void Awake()
    {
        ServiceLocator.Instance.BindData(typeof(WeaponInputController), this);
    }

    public void GetWeapons(Weapon[] getWeaponsComponent)
    {
        for (int i = 0; i < getWeaponsComponent.Length; i++)
        {
            IMouseInput inputSystem = getWeaponsComponent[i].gameObject.GetComponent<IMouseInput>();
            _mouseInputs.Add(inputSystem);
        }

        _mouseInputs = _mouseInputs.DistinctBy(x => x.GetType()).ToList();
        for (int i = 0; i < _mouseInputs.Count; i++)
        {
            if (_mouseInputs[i].GetType() == typeof(MouseInputClick))
            {
                _mouseInputs[i] = this.AddComponent<MouseInputClick>();
            }
            if (_mouseInputs[i].GetType() == typeof(MouseInputTouch))
            {
                _mouseInputs[i] = this.AddComponent<MouseInputTouch>();
            }

            Debug.Log(_mouseInputs[i]);
        }
    }

    public IMouseInput FindEqual(Type type)
    {
        IMouseInput mouseInput = null;
        for (int i = 0; i < _mouseInputs.Count; i++)
        {
            if (_mouseInputs[i].GetType() == type)
            {
                mouseInput = _mouseInputs[i];
            }
        }

        return mouseInput;
    }
}