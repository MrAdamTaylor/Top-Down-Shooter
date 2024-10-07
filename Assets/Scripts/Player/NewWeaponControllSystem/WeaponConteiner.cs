using System;

namespace Scripts.Player.NewWeaponControllSystem
{
    public class WeaponConteiner
    {
        private Weapon[] _weapons;
        public int Count { get; private set; }

        public WeaponConteiner(Weapon[] returnWeapons)
        {
            _weapons = returnWeapons;
            Count = returnWeapons.Length;
        }
        
        public Weapon GetActiveWeapon()
        {
            Weapon weapon = null;
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].gameObject.activeSelf)
                {
                    weapon = _weapons[i];
                }
            }

            if (weapon == null)
            {
                throw new Exception("Not find active weapon");
            }
            else
            {
                return weapon;
            }
        }
        

        public Weapon GetByIndex(int i)
        {
            return _weapons[i];
        }

        private static WeaponType ReturnType(WeaponType weapon)
        {
            switch (weapon)
            {
                case WeaponType.Pistol:
                    return WeaponType.Pistol;
                case WeaponType.ShootGun:
                    return WeaponType.ShootGun;
                case WeaponType.Rifle:
                    return WeaponType.Rifle;
                default:
                    return WeaponType.Undefinded;
            }
        }
    }
}