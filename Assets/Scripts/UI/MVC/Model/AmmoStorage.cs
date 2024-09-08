using System;
using UnityEngine;

public class AmmoStorage
{
    public event Action<long> OnAmmoChanged;
        
    public long Ammo { get; private set; }
    
    public AmmoStorage(long ammo)
    {
        Ammo = ammo;
    }
    
    public void AddAmmo(long current)
    {
        Ammo += current;
        Debug.Log("Current Ammo: "+Ammo);
        OnAmmoChanged?.Invoke(Ammo);
    }

    public void SpendAmmo(long current)
    {
        Ammo -= current;
        Debug.Log("Current Ammo: "+Ammo);
        OnAmmoChanged?.Invoke(Ammo);
    }
}