using System;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Action DeathAction;
    
    private EnemyAnimator _animator;
    private EnemyHealth _enemyHealth;

    public void Construct(EnemyHealth health,EnemyAnimator animator)
    {
        _enemyHealth = health;
        _enemyHealth.NoHealthAction += Death;
        _animator = animator;
    }

    void OnDestroy()
    {
        _enemyHealth.NoHealthAction -= Death;
    }

    private void Death()
    {
        Debug.Log($"<color=red>Enemy Death </color>");
        DeathAction?.Invoke();
        _animator.PlayDeath();
    }
}