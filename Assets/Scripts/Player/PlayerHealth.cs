using System;
using Enemies;
using Infrastructure.ServiceLocator;
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
    
        public void Construct(float value)
        {
            _maxValue = value;
            _current = _maxValue;
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
    }
}
