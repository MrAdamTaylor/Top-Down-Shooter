using System;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using UI.MVC.Model;
using UnityEngine;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    [RequireComponent(typeof(ShootControlSystem), typeof(Weapon.Weapon))]
    public class AmmoController : MonoBehaviour
    {
        public Action<long, bool> ChangeAmmo;
    
        private long _ammoCount;
        private bool _isIfinity;
        private ShootControlSystem _shootControlSystem;

        private long _ammoWaste;
        private long _currentAmmo;
        private AmmoStorage _ammoStorage;

        public void Construct(ShootControlSystem shootControlSystem, AmmoCharacteristics characteristics)
        {
            _ammoWaste = characteristics.WastedAmmo == 0 ? Constants.STANDART_WASTE_WALUE : characteristics.WastedAmmo;
            _shootControlSystem = shootControlSystem;
            _ammoStorage = (AmmoStorage)ServiceLocator.Instance.GetCloneData(typeof(AmmoStorage));
            _isIfinity = characteristics.IsInfinity;
            _currentAmmo = _isIfinity ? -long.MaxValue : characteristics.StarterAmmo;
            _ammoStorage.Construct(_currentAmmo);
            _shootControlSystem.ShootAction += WasteAmmo;
        }

        private void OnDestroy()
        {
            _shootControlSystem.ShootAction -= WasteAmmo;
        }

        public bool CanShoot()
        {
            return _currentAmmo > 0 || _isIfinity;
        }

        public AmmoStorage ReturnStorage()
        {
            return _ammoStorage;
        }

        private void WasteAmmo()
        {
            if (_isIfinity) 
                return;
        
            _currentAmmo -= _ammoWaste;
            Debug.Log("Current Ammo: "+_currentAmmo);
            ChangeAmmo?.Invoke(_currentAmmo, _isIfinity);
            _ammoStorage.SpendAmmo(_ammoWaste);
        }
    }
}