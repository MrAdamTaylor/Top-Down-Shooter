using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private float _max;
    private float _current;
    private EnemyAnimator _animator;

    public Action NoHealthAction;
    public Action TakeDamageAction;

    public void Construct(float healt, EnemyAnimator animator)
    {
        _animator = animator;
        _max = healt;
        _current = _max;
    }

    public void TakeDamage(float damage)
    {
        if (_current <= 0)
        {
            NoHealthAction?.Invoke();
            return;
        }
        _current -= damage;
        Debug.Log($"<color=green>Enemy current is {_current} </color>");
    }
}