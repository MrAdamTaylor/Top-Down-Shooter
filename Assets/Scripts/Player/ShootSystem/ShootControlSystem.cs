using System;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    public class ShootControlSystem : MonoBehaviour
    {
        private CoomoonShootSystem _shootSystem;
    
        public Action ShootAction;
    
        private float _weaponDelay;
        private float _lastShootTime;

        public void Construct(WeaponStaticData data, WeaponEffectsConteiner weaponEffectsConteiner, CoomoonShootSystem shootSystem)
        {
            _weaponDelay = data.SpeedFireRange;
            _shootSystem = shootSystem;
            _shootSystem.Construct(data, weaponEffectsConteiner);
            
        }

        public void Shoot()
        {
            if (_lastShootTime + _weaponDelay < Time.time)
            {
                _shootSystem.Shoot();
                ShootAction?.Invoke();
                _lastShootTime = Time.time;
            }
        }

        public void UpdateValues(WeaponCharacteristics characteristics)
        {
            _weaponDelay = characteristics.FireSpeedRange;
            _shootSystem.UpdateValues(characteristics);
        }

        /*public void AddSelfBlockList()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            player.AddBlockList(this);
        }*/
    }
}
