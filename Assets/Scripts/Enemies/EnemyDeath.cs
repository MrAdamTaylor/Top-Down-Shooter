using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyDeath : MonoBehaviour
    {
        private const float DEATH_COOLDOWN = 3f;
    
        public Action DeathAction;
        public Action<GameObject> ObjectDeathAction;
    
        private EnemyAnimator _animator;
        private EnemyHealth _enemyHealth;
        private EnemyController _enemyController;
        private float _deathCooldown;
        private bool _isDeathCooldown = false;

        public void Construct(EnemyHealth health,EnemyAnimator animator)
        {
            _deathCooldown = DEATH_COOLDOWN;
            _enemyHealth = health;
            _enemyHealth.NoHealthAction += Death;
            _animator = animator;
        }

        private void Update()
        {
            if (_isDeathCooldown)
            {
                _deathCooldown -= Time.deltaTime;
            }

            if (_deathCooldown <= 0)
            {
                ObjectDeathAction?.Invoke(gameObject);
                _isDeathCooldown = false;
                _deathCooldown = DEATH_COOLDOWN;
            }
        }

        private void OnDestroy()
        {
            _enemyHealth.NoHealthAction -= Death;
        }

        private void Death()
        {
            Debug.Log($"<color=red>Enemy Death </color>");
            DeathAction?.Invoke();
            _animator.PlayDeath();
            _isDeathCooldown = true;
        }
    }
}

