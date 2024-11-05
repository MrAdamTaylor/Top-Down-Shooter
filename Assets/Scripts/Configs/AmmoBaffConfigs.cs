using System;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Configs
{
    [CreateAssetMenu(fileName = "BaffAmmo", menuName = "Baff/Ammo")]
    public class AmmoBaffConfigs : BafConfigs
    {
        [SerializeField] private List<AmmoStructConfig> AmmoStructConfigs;
    }
    
    [Serializable]
    public struct AmmoStructConfig
    {
        public WeaponType WeaponT;
        public int AmmoCount;
    }
}