using System;

namespace Player.MouseInput
{
    public interface IMouseInput : IPlayerSystem
    {
        public event Action OnFire;
    }
}