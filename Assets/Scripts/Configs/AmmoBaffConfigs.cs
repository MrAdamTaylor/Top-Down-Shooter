using System;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Configs
{
    [CreateAssetMenu(fileName = "BaffAmmo", menuName = "Baff/Ammo")]
    public class AmmoBaffConfigs : BafConfigs
    {
        public List<AmmoStructConfig> AmmoStructConfigs = new () {new AmmoStructConfig(WeaponType.Rifle, 50), new AmmoStructConfig(WeaponType.ShootGun, 20)};
    }
    
    [Serializable]
    public struct AmmoStructConfig
    {

        public WeaponType WeaponT;
        public int AmmoCount;
        public AmmoStructConfig(WeaponType type, int ammoCount)
        {
            WeaponT = type;
            AmmoCount = ammoCount;
        }
        
    }
}