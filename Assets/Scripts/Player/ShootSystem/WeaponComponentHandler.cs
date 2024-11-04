using System;
using Player.MouseInput;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    public class WeaponComponentHandler
    {
        public void GetShootSystem(Transform obj, WeaponType dataWType)
        {
            switch (dataWType)
            {
                case WeaponType.Pistol:
                    obj.gameObject.AddComponent<ShootSystemOnly>().AddSelfBlockList();
                    break;
                case WeaponType.ShootGun:
                    obj.gameObject.AddComponent<ShootSystemFraction>().AddSelfBlockList();
                    break;
                case WeaponType.Rifle:
                    obj.gameObject.AddComponent<ShootSystemOnly>().AddSelfBlockList();
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
                    MouseInputClick inputClick = transform.gameObject.AddComponent<MouseInputClick>();
                    inputClick.AddSelfBlockList();
                    break;
                case WeaponInputType.OnTouchInput:
                    MouseInputTouch inputTouch = transform.gameObject.AddComponent<MouseInputTouch>();
                    inputTouch.AddSelfBlockList();
                    break;
                case WeaponInputType.Undefinded:
                    throw new Exception("Not find Input System in WeaponFabric.WeaponHandler");
            }
        }
    }
}