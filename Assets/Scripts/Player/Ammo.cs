using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Ammo : MonoBehaviour
{
    public Action<int, bool> ChangeAmmo;
    
    [SerializeField] private int _ammoCount;
    [SerializeField] private bool _infinity;
    [FormerlySerializedAs("_shootControl")] [SerializeField] private ShootControlSystem _shootControlSystem;
    private int _currentAmmo;
    
    public string SetCurrentAmmo()
    {
        if (_infinity)
        {
            return "Infinity";
        }
        else
        {
            return Convert.ToString(_currentAmmo);
        }
    }

    public void WasteAmmo()
    {
        if(!_infinity)
            _currentAmmo -= 1;
        ChangeAmmo?.Invoke(_currentAmmo, _infinity);
    }

    public bool CanShoot()
    {
        if (_currentAmmo > 0 || _infinity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddAmmo(int ammoBonus)
    {
        Debug.Log("Добавлены патроны в размере: "+ammoBonus);
        _currentAmmo += ammoBonus;
        Debug.Log(_currentAmmo + "После добавления");
        ChangeAmmo?.Invoke(_currentAmmo, _infinity);
    }

    public int GetAmmo()
    {
        return _currentAmmo;
    }

    private void Awake()
    {
        _currentAmmo = _ammoCount;
    }

    private void Start()
    {
        Debug.Log(_shootControlSystem.gameObject.name);
        _shootControlSystem.ShootAction += WasteAmmo;
    }

    private void OnDestroy()
    {
        _shootControlSystem.ShootAction -= WasteAmmo;
    }

    public bool IsInfinity()
    {
        return _infinity;
    }
}