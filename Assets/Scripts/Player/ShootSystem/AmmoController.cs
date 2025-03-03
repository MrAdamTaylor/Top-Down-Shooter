using EnterpriceLogic.Constants;
using Infrastructure.BootstrapLogic;
using Infrastructure.ServiceLocator;
using UI.MVC.Model;
using UnityEngine;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    [RequireComponent(typeof(ShootControlSystem), typeof(Weapon.Weapon))]
    public class AmmoController : MonoBehaviour
    {
    
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
            _ammoStorage.OnAmmoChanged += SetAmmo;
            _shootControlSystem.ShootAction += WasteAmmo;
        }

        private void SetAmmo(long obj)
        {
            _currentAmmo = obj;
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
            _ammoStorage.SpendAmmo(_ammoWaste);
        }
    }
}