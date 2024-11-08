using Configs;
using Player;
using UnityEngine;

namespace Logic.Bafs
{
    public class HealthBaffComponent : MonoBehaviour, IBaffComponent
    {
        private GameObject _playerGameObj;
        private HealthBaffConfigs _ammoBaffConfigs;
        public void Construct(Player.Player player, BafConfigs bafConfigs)
        {
            _ammoBaffConfigs = (HealthBaffConfigs)bafConfigs;
            _playerGameObj = player.gameObject;
        }

        public void AddBaff()
        {
            PlayerHealth health = _playerGameObj.GetComponent<PlayerHealth>();
            health.AddHealth(_ammoBaffConfigs.HealthUp);
            Destroy(gameObject);
        }
    }
}