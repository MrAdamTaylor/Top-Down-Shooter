using System;
using UnityEngine;

public class MouseInputClick : MonoBehaviour, IMouseInput
{
    public event Action OnFire;
    
    void Update()
    {
        HandleMouse();
    }

    private void HandleMouse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.Fire();
        }
    }

    private void Fire()
    {
        this.OnFire?.Invoke();
    }
}