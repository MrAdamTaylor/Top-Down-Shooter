using System;
using UnityEngine;

public class PlayableHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _current;
    [SerializeField] private float _maxValue;

    public Action<float> HealthChange;
    private HealthAdapter _healthAdapter;
    
    public void Construct(float current)
    {
        _current = current;
        _healthAdapter = (HealthAdapter)ServiceLocator.Instance.GetData(typeof(HealthAdapter));
    }

    public void TakeDamage(float damage)
    {
        if(_current <= 0)
            return;

        _current -= damage;
        _healthAdapter.UpdateValues(_current);
    }
}
