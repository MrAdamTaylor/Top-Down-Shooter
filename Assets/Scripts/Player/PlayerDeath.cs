using System;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public Action PlayerDefeat;
        private PlayerHealth _playerHealth;
        private PlayerAnimator _playerAnimator;

        private bool _isDie;
        
        public void Construct(PlayerHealth playerHealth, PlayerAnimator playerAnimator)
        {
            _playerHealth = playerHealth;
            _playerHealth.DeathAction += Death;
            _playerAnimator = playerAnimator;
            ServiceLocator.Instance.BindData(typeof(PlayerDeath), this);
        }

        private void Death()
        {
            if(_isDie)
                return;
            _isDie = true;
            PlayerDefeat?.Invoke();
            _playerAnimator.PlayDeath();
        }
    }
}