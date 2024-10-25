using System;
using Infrastructure.Services;
using UnityEngine;

namespace Player.MovementSystem
{
    public interface IInputSystem : IService
    {
        event Action<Vector2> OnMove;
    }
}