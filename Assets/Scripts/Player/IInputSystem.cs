using System;
using UnityEngine;

internal interface IInputSystem
{
    event Action<Vector2> OnMove;
}