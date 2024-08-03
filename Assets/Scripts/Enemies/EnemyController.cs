using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private RotateSystem _rotateSystem;
    [SerializeField] private MoveTo _moveSystem;

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
}
