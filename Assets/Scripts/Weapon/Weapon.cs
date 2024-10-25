using System;
using Player.ShootSystem;
using UnityEngine;
using Weapon.StaticData;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        public WeaponType TypeWeapon;
    
        [SerializeField] public Transform ShootPoint;
    
        private ShootControlSystem _shootSystem;

        [SerializeField] private WeaponCharacteristics Characteristics;
    
        public virtual void Construct(ShootControlSystem shootControlSystem, WeaponStaticData data)
        {
            _shootSystem = shootControlSystem;
            Characteristics.Damage = data.Damage;
            Characteristics.FireSpeedRange = data.SpeedFireRange;
        }

        public void Fire()
        {
            _shootSystem.UpdateValues(Characteristics);
            _shootSystem.Shoot();
        }

        public Vector3 GetShootPosition()
        {
            Matrix4x4 matrix = transform.localToWorldMatrix;
            Vector3 position = matrix.GetPosition();
            position = position + transform.forward;
            return position;
        }
    }

    [Serializable]
    public struct WeaponCharacteristics
    {
        public int Damage;
        public float FireSpeedRange;
    }
}