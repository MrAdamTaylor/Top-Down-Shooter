using Configs;
using Enemies;
using Player;
using UnityEngine;

namespace Infrastructure.StateMachine.States
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