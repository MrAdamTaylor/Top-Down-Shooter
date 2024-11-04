using System;
using Infrastructure.Services;
using UnityEngine;

namespace Player.MovementSystem
{
    public interface IInputSystem : IService, IPlayerSystem
    {
        event Action<Vector2> OnMove;
    }
}