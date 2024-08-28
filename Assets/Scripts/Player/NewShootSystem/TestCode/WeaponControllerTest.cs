using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerTest : MonoBehaviour
{
    [SerializeField] private NewPistol _weapon;
    [SerializeField] private MouseInputTouch _mouseInputClick;

    private void Awake()
    {
        _mouseInputClick.OnFire += OnShoot;
    }

    private void OnShoot()
    {
        _weapon.Fire();
    }
}
