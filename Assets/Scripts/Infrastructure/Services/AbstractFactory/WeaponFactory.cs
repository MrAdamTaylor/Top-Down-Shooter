using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    private WeaponStaticData[] _weaponStaticDatas;
    private WeaponComponentHandler _weaponComponentHandler;
    private WeaponEffectsConteiner _weaponEffectsConteiner;

    private Dictionary<WeaponType, WeaponStaticData> _weaponDictionary;

    private WeaponData _data;
    
    public WeaponFactory(IAsserts asserts)
    {
        _weaponEffectsConteiner = new WeaponEffectsConteiner(asserts);
        _data = new WeaponData();
    }

    public void Construct()
    {
        _weaponStaticDatas = Resources.LoadAll<WeaponStaticData>("StaticData/WeaponData");
        _weaponDictionary = _weaponStaticDatas.ToDictionary(x => x.WType, y => y);
        _weaponComponentHandler = new WeaponComponentHandler();
    }

    public void CreateWeapons(Weapon[] weapon, Transform player)
    {
        ServiceLocator.Instance.BindData(typeof(AmmoStorage), new AmmoStorage());
        for (int i = 0; i < weapon.Length; i++)
        {
            _data.AddWeaponTypeWithIndex(weapon[i].TypeWeapon,i);
            WeaponStaticData data = _weaponDictionary[weapon[i].TypeWeapon];
            data.BulletPoint = weapon[i].transform.Find(Constants.WEAPON_POINTSHOOT_NAME);
            ShootControlSystem shootControlSystem = weapon[i].AddComponent<ShootControlSystem>();
            weapon[i].Construct(shootControlSystem, data);
            _weaponComponentHandler.GetShootSystem(weapon[i].transform, data.WType);
            CoomoonShootSystem shootSystem = weapon[i].gameObject.GetComponent<CoomoonShootSystem>();
            shootControlSystem.Construct(data, _weaponEffectsConteiner, shootSystem);
            _weaponComponentHandler.GetInputSytem(weapon[i].transform, data.InpType);
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
        ServiceLocator.Instance.BindData(typeof(WeaponData), _data);
    }
}

public class WeaponData
{
    private Dictionary<int, WeaponType> _weaponTypes = new();
    private Dictionary<int, AmmoController> _weaponAmmo = new();
    
    
    public void AddWeaponTypeWithIndex(WeaponType weaponType, int index)
    {
        _weaponTypes.Add(index,weaponType);
    }

    public void AddAmmoWithIndex(int index, AmmoController controller=null)
    {
        if(controller != null)
            _weaponAmmo.Add(index, controller);
    }

    public Dictionary<int, (WeaponType, AmmoController)> GetAmmoData()
    {
        if (_weaponAmmo.Count != 0)
        {
            Dictionary<int, (WeaponType, AmmoController)> _ammoDictionary = new();
            int k = 0;
            for (int i = 0; i < _weaponTypes.Count; i++)
            {
                if (_weaponAmmo.Keys.Contains(i))
                {
                    Debug.Log("Ammo Dictionary added Weapon Ammo: "+ _weaponAmmo[i] + " added Weapon Type"+_weaponTypes);
                    _ammoDictionary.Add(k, (_weaponTypes[i], _weaponAmmo[i]));
                    k++;
                }
            }

            return _ammoDictionary;
        }
        else
        {
            throw new WarningException("Not AmmoData");
        }
        
    }
}