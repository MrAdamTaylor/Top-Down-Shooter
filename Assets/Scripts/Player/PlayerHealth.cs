using System;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _current;
    [SerializeField] private float _maxValue;

    public Action<float> HealthChange;
    private HealthAdapter _healthAdapter;
    
    public void Construct(float value)
    {
        _maxValue = value;
        _current = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        if(_healthAdapter.IsNull())
            _healthAdapter = (HealthAdapter)ServiceLocator.Instance.GetData(typeof(HealthAdapter));

        if(_current <= 0)
            return;

        _current -= damage;
        _healthAdapter.UpdateValues(_current, _maxValue);
    }
}
