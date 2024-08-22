using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(EnemyDeath))]
public class Health : MonoBehaviour
{
    [HideInInspector] public int MaxHealth;
    private int CurrentHealth { get; set; }

    private EnemyDeath _death;
    public void Awake()
    {
        _death = this.GetComponent<EnemyDeath>();
        ServiceLocator.Instance.BindData(typeof(Health), this);
    }

    public void DealDamage(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            _death.MakeDeath();
        }
    }

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }
}
