using System;
using UnityEngine;

internal class AxisInputSystem : MonoBehaviour, IInputSystem
{
    public event Action<Vector2> OnMove;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector2 _moveDirection;
    
    private void Move(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }
    
    public void Update()
    {
        GetInputValues();
        _moveDirection =  Vector2.up * _verticalInput + Vector2.right * _horizontalInput;
        Move(_moveDirection);
    }

    private void GetInputValues()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
}