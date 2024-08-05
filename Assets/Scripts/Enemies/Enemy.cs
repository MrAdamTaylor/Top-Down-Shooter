using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed; 
    [SerializeField] protected int _health;

    [SerializeField] protected float _scores;
    [SerializeField] protected float _probability;

    private Health _healthComponent;
    
    public virtual void Awake()
    {
        try
        {
            _healthComponent = this.GetComponent<Health>();
            _healthComponent._maxHealth = _health;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public float ReturnSpeed()
    {
        return _speed;
    }

}