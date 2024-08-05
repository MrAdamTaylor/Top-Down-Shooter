using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    public int _maxHealth;
    private int CurrentHealth { get; set; }

    private Death _death;
    public void Awake()
    {
        _death = this.GetComponent<Death>();
    }

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }
    
    public void DealDamage(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
        {
            _death.MakeDeath();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
