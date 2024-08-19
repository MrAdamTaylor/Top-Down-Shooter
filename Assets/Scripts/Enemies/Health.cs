using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    [HideInInspector] public int MaxHealth;
    private int CurrentHealth { get; set; }

    private Death _death;
    public void Awake()
    {
        _death = this.GetComponent<Death>();
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
