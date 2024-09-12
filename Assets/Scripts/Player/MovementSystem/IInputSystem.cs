using System;
using UnityEngine;

public interface IInputSystem : IService
{
    event Action<Vector2> OnMove;
}