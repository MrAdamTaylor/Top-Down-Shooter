using System;

namespace UI.MVC.Model
{
    [Serializable]
    public class AmmoStorage
    {
        public event Action<long> OnAmmoChanged;
        
        public long Ammo { get; private set; }

        public void Construct(long ammo)
        {
            Ammo = ammo;
        }

        public void UpdateScreen(long ammo = 0L)
        {
            Ammo += ammo;
            OnAmmoChanged?.Invoke(Ammo);
        }

        public void AddAmmo(long current)
        {
            Ammo += current;
            OnAmmoChanged?.Invoke(Ammo);
        }

        public void SpendAmmo(long current)
        {
            Ammo -= current;
            OnAmmoChanged?.Invoke(Ammo);
        }
    }
}