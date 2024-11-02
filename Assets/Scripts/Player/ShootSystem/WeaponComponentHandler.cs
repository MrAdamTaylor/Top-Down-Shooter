using System;
using Player.MouseInput;
using Player.ShootSystem;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Infrastructure.Services.AbstractFactory
{
    public class WeaponComponentHandler
    {
        public void GetShootSystem(Transform obj, WeaponType dataWType)
        {
            switch (dataWType)
            {
                case WeaponType.Pistol:
                    obj.gameObject.AddComponent<ShootSystemOnly>();
                    break;
                case WeaponType.ShootGun:
                    obj.gameObject.AddComponent<ShootSystemFraction>();
                    break;
                case WeaponType.Rifle:
                    obj.gameObject.AddComponent<ShootSystemOnly>();
                    break;
                case WeaponType.Undefinded:
                    throw new Exception("Not find Weapon Type in WeaponFabric.WeaponHandler");
            }
        }

        public void GetInputSytem(Transform transform, WeaponInputType dataInpType)
        {
            switch (dataInpType)
            {
                case WeaponInputType.OnClickInput:
                    transform.gameObject.AddComponent<MouseInputClick>();
                    break;
                case WeaponInputType.OnTouchInput:
                    transform.gameObject.AddComponent<MouseInputTouch>();
                    break;
                case WeaponInputType.Undefinded:
                    throw new Exception("Not find Input System in WeaponFabric.WeaponHandler");
            }
        }
    }
}