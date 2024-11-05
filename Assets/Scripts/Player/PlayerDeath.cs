using System;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        private const float COOLDOWN_TIME = 3f;
        
        public Action PlayerDefeat;
        public Action PlayerDefeatAction;
        //public Action PlayerDefeatAfterCooldown;
        private PlayerHealth _playerHealth;
        private PlayerAnimator _playerAnimator;
        private bool _isDeathCooldown;
        private float _deathCooldown;


        public bool IsDie { get; private set; }

        public void Construct(PlayerHealth playerHealth, PlayerAnimator playerAnimator)
        {
            _playerHealth = playerHealth;
            _playerHealth.DeathAction += Death;
            _playerAnimator = playerAnimator;
            _deathCooldown = COOLDOWN_TIME;
            PlayerDefeatAction += _playerHealth.CanReload;
            ServiceLocator.Instance.BindData(typeof(PlayerDeath), this);
        }

        private void Update()
        {
            if (_isDeathCooldown)
            {
                _deathCooldown -= Time.deltaTime;
            }
            
            if (_deathCooldown <= 0)
            {
                //ObjectDeathAction?.Invoke(gameObject);
                
                _isDeathCooldown = false;
                _deathCooldown = COOLDOWN_TIME;
                PlayerDefeatAction?.Invoke();
            }
        }

        public void Alive()
        {
            IsDie = false;
        }

        private void Death()
        {
            if(IsDie)
                return;
            IsDie = true;
            _isDeathCooldown = true;
            PlayerDefeat?.Invoke();
            _playerAnimator.PlayDeath();
        }
    }
}