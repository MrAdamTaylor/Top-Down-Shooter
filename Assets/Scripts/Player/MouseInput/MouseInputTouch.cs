using System;
using UnityEngine;

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
}