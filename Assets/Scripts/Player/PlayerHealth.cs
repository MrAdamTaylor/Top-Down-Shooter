using System;
using Infrastructure.ServiceLocator;
using Logic;
using UI.MVC.Presenters;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;
        [SerializeField] private float _maxValue;

        public Action DeathAction;
        public Action<float> HealthChange;
        private HealthAdapter _healthAdapter;
        private bool _isReadyForRevive;
    
        public void Construct(float value)
        {
            _maxValue = value;
            _current = _maxValue;
            ServiceLocator.Instance.BindData(typeof(PlayerHealth), this);
        }

        public void Revive()
        {
            if (!_isReadyForRevive) 
                return;
            
            _current = _maxValue;
            _healthAdapter.UpdateValues(_current, _maxValue);
            _isReadyForRevive = false;
        }

        public void TakeDamage(float damage)
        {
            _healthAdapter ??= (HealthAdapter)ServiceLocator.Instance.GetData(typeof(HealthAdapter));

            if (_current <= 0)
            {
                DeathAction?.Invoke();
            }

            _current -= damage;
            _healthAdapter.UpdateValues(_current, _maxValue);
        }

        public void CanReload()
        {
            _isReadyForRevive = true;
        }
    }
}
