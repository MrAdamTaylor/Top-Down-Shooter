using System;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Player.MouseInput
{
    public class MouseInputTouch : MonoBehaviour, IMouseInput
    {
        public event Action OnFire;

        private void Update()
        {
            this.HandleMouse();
        }

        private void HandleMouse()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                this.Fire();
            }
        }

        private void Fire()
        {
            this.OnFire?.Invoke();
        }

        public void AddSelfBlockList()
        {
            Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            player.AddBlockList((MonoBehaviour)this);
        }
    }
}