using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAmmo : MonoBehaviour
{
    [SerializeField] private ShootControlSystem _shootControlSystem;

    private int _ammo;

    public Action<int> ChangeAmmo;
    
    private void Start()
    {
        _ammo = 0;
        _shootControlSystem.ShootAction += UpdateAmmo;
    }

    private void UpdateAmmo()
    {
        _ammo += 1;
        ChangeAmmo?.Invoke(_ammo);
    }
}
