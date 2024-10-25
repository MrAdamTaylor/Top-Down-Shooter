using System;
using System.Collections.Generic;
using System.Linq;
using Player.MouseInput;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.NewWeaponControllSystem
{
    public class WeaponInputHandler
    {
        private List<IMouseInput> _mouseInputs = new();

        private GameObject _gameObject;

        public WeaponInputHandler(WeaponController controller, Weapon.Weapon[] weapon)
        {
            _gameObject = controller.gameObject;
            GetWeapons(weapon);
        }

        private void GetWeapons(Weapon.Weapon[] getWeaponsComponent)
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
                    _mouseInputs[i] = null;
                    _gameObject.AddComponent<MouseInputClick>();
                    _mouseInputs[i] = _gameObject.GetComponent<MouseInputClick>();
                }
                if (_mouseInputs[i].GetType() == typeof(MouseInputTouch))
                {
                    _mouseInputs[i] = null;
                    _gameObject.AddComponent<MouseInputTouch>();
                    _mouseInputs[i] = _gameObject.GetComponent<MouseInputTouch>();
                }
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
}