using System;
using Infrastructure.ServiceLocator;
using UI.MVC.Model;
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
        private ScoresStorage _scoresStorage;
        private int _order;

        public void Construct(EnemyHealth health,EnemyAnimator animator, int order=1)
        {
            _order = order;
            _deathCooldown = DEATH_COOLDOWN;
            _enemyHealth = health;
            _enemyHealth.NoHealthAction += Death;
            _animator = animator;
            _scoresStorage = (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage));
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
            //Debug.Log($"<color=red>Enemy Death </color>");
            _scoresStorage.AddScores(_order);
            DeathAction?.Invoke();
            _animator.PlayDeath();
            _isDeathCooldown = true;
        }
    }
}

