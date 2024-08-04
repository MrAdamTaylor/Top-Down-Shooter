using System;
using System.Collections;
using UnityEngine;

public class MouseInputClick : MonoBehaviour, IMouseInput
{
    public event Action OnFire;
    
    public void Update()
    {
        this.HandleMouse();
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