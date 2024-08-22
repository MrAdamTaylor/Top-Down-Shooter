using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Mechanics;
using Mechanics.Spawners.NewArchitecture;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private RotateSystem _rotateSystem;
    [SerializeField] private MoveTo _moveSystem;
    [SerializeField] private EnemyDeath _death;
    
    private EnemySpawner _spawner;

    public void Construct(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    public void Start()
    {
        if (_rotateSystem != null)
        {
            _rotateSystem.OnStart();
        }

        if (_moveSystem != null)
        {
            _moveSystem.OnStart(_enemy.ReturnSpeed()*Constants.NPC_SPEED_MULTIPLYER);
            _moveSystem.Move();
        }
    }

    public void OnEnable()
    {
        _moveSystem.Move();
        _rotateSystem.OnStart();
    }

    public void SubscribeDeath()
    {
        _death.OnDeath += ReturnPool;
    }

    private void ReturnPool()
    {
        _spawner.ReturnPool(this.gameObject);
    }

    
    public void OnDestroy()
    {
        _death.OnDeath -= ReturnPool;
    }

    public void OnDisable()
    {
        _moveSystem.StopMove();
        _rotateSystem.Stop();
    }
}
