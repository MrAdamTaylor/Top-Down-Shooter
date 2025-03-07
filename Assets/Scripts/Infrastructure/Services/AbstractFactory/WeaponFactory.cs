using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Player.ShootSystem;
using UI.MVC.Model;
using Unity.VisualScripting;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Infrastructure.Services.AbstractFactory
{
    public class WeaponFactory : IWeaponFactory
    {
        private WeaponStaticData[] _weaponStaticDatas;
        private WeaponComponentHandler _weaponComponentHandler;
        private WeaponEffectsConteiner _weaponEffectsConteiner;
        private Dictionary<WeaponType, WeaponStaticData> _weaponDictionary;
        private WeaponData _data;
    
        public WeaponFactory(AssertBuilder asserts)
        {
            _weaponEffectsConteiner = new WeaponEffectsConteiner(asserts);
            _data = new WeaponData();
            _weaponStaticDatas = Resources.LoadAll<WeaponStaticData>(PrefabPath.WEAPON_DATA_PATH);
            _weaponDictionary = _weaponStaticDatas.ToDictionary(x => x.WType, y => y);
            _weaponComponentHandler = new WeaponComponentHandler();
        }

        public void CreateAll(Weapon.Weapon[] weapon)
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(AmmoStorage), new AmmoStorage());
            for (int i = 0; i < weapon.Length; i++)
            {
                _data.AddWeaponTypeWithIndex(weapon[i].TypeWeapon,i);
                WeaponStaticData data = _weaponDictionary[weapon[i].TypeWeapon];
                data.BulletPoint = weapon[i].transform.Find(PrefabPath.WEAPON_POINTSHOOT_NAME);
                data.ShootPosition = weapon[i].GetShootPosition();
            
                ShootControlSystem shootControlSystem = weapon[i].AddComponent<ShootControlSystem>();
                weapon[i].Construct(shootControlSystem, data);
                _weaponComponentHandler.GetShootSystem(weapon[i].transform, data.WType);
                
            
                CoomoonShootSystem shootSystem = weapon[i].gameObject.GetComponent<CoomoonShootSystem>();
                shootControlSystem.Construct(data, _weaponEffectsConteiner, shootSystem);
                _weaponComponentHandler.GetInputSytem(weapon[i].transform, data.InpType);


                AudioPlayerComponent playerComponent = weapon[i].gameObject.AddComponent<AudioPlayerComponent>();
                playerComponent.Construct(shootControlSystem, data);
            
                if (data.IsMuzzle)
                {
                    MuzzleFlashEffect muzzleFlashEffect = weapon[i].AddComponent<MuzzleFlashEffect>();
                    muzzleFlashEffect.Construct(shootControlSystem, _weaponEffectsConteiner, data.BulletPoint);
                }
            
                if (data.IsAmmo)
                {
                    AmmoController controller = weapon[i].AddComponent<AmmoController>();
                    controller.Construct(shootControlSystem, data.AmmoValues);
                    _data.AddAmmoWithIndex(i, controller);
                }
                else
                {
                    _data.AddAmmoWithIndex(i);
                }
            }
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(WeaponData), _data);
        }
    }
}