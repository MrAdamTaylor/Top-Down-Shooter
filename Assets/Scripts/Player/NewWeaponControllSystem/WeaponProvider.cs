using UnityEngine;

namespace Player.NewWeaponControllSystem
{
    public class WeaponProvider : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon[] _weapons;

        public Weapon.Weapon[] ReturnWeapons()
        {
            Weapon.Weapon[] weapons = new Weapon.Weapon[_weapons.Length];

            for (int i = 0; i < _weapons.Length; i++)
            {
                Weapon.Weapon weapon = _weapons[i].gameObject.GetComponent<Weapon.Weapon>();
                weapons[i] = weapon;
            }

            return weapons;
        }
    }
}
