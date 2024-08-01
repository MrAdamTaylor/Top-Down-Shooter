using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed; 
    [SerializeField] protected float _health;

    public float ReturnSpeed()
    {
        return _speed;
    }

}