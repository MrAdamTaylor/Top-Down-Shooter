using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    
    public float Speed = 2.5f;
    
    private float _innerSpeed;

    public void Awake()
    {
        Debug.Log(this);
        _innerSpeed = Speed;
        ServiceLocator.Instance.BindData(typeof(Player),this);
    }

    public void Move(Vector3 offset)
    {
        this.transform.position += offset * this._innerSpeed;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    public void SwitchSpeed(float speedChange)
    {
        _innerSpeed = Speed - (Speed * speedChange);
    }
}
