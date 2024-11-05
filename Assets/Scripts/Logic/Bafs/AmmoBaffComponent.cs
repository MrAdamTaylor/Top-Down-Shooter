using System.Collections.Generic;
using Configs;
using Enemies;
using UI.MVC.Model;
using UnityEngine;
using Weapon;

namespace Infrastructure.StateMachine.States
{
    internal class AmmoBaffComponent : MonoBehaviour, IBaffComponent
    {
        private GameObject _playerGameObj;
        private AmmoBaffConfigs _ammoBaffConfigs;

        private Dictionary<WeaponType, AmmoStorage> _dictionary;
        public void Construct(Player.Player player, BafConfigs bafConfigs)
        {
            _ammoBaffConfigs = (AmmoBaffConfigs)bafConfigs;
            _playerGameObj = player.gameObject;
            _dictionary = (Dictionary<WeaponType, AmmoStorage>)ServiceLocator.ServiceLocator.Instance.
                GetData(typeof(Dictionary<WeaponType, AmmoStorage>));
        }

        public void AddBaff()
        {
            for (int i = 0; i < _ammoBaffConfigs.AmmoStructConfigs.Count; i++)
            {
                AmmoStorage storage = _dictionary[_ammoBaffConfigs.AmmoStructConfigs[i].WeaponT];
                storage.AddAmmo(_ammoBaffConfigs.AmmoStructConfigs[i].AmmoCount);
            }

            Destroy(gameObject);
        }
    }
}