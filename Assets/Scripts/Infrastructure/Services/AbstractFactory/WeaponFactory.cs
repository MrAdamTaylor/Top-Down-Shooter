using System;
using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    private WeaponStaticData[] _weaponStaticDatas;
    private WeaponComponentHandler _weaponComponentHandler;
    private WeaponEffectsConteiner _weaponEffectsConteiner;

    private Dictionary<WeaponType, WeaponStaticData> _weaponDictionary;

    public WeaponFactory(IAsserts asserts)
    {
        _weaponEffectsConteiner = new WeaponEffectsConteiner(asserts);
    }

    public void CreateWeapons(Weapon[] weapon, Transform player)
    {
        ServiceLocator.Instance.BindData(typeof(AmmoStorage), new AmmoStorage());
        for (int i = 0; i < weapon.Length; i++)
        {
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
            }
        }
    }

    public void LoadData()
    {
        _weaponStaticDatas = Resources.LoadAll<WeaponStaticData>("StaticData/WeaponData");
        _weaponDictionary = _weaponStaticDatas.ToDictionary(x => x.WType, y => y);
        _weaponComponentHandler = new WeaponComponentHandler();
    }
}

public class WeaponEffectsConteiner
{
    private IAsserts _asserts;

    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;
    private LineRenderer _lineRenderer;

    public WeaponEffectsConteiner(IAsserts asserts)
    {
        _asserts = asserts;
        _particleSystem = _asserts.LoadParticle(Constants.IMPACT_PARTICLE_EFFECT);
        _trailRenderer = _asserts.LoadTrailRenderer(Constants.HOT_TRAIL_PATH);
        _lineRenderer = _asserts.LoadLineRenderer(Constants.LINE_RENDERER_PATH);
    }


    public ParticleSystem GetParticleEffect(string path = "", Transform position = null, Transform parent = null)
    {
        if (path.IsEmpty())
            return _particleSystem;
        if (position.IsNullBoolWarning(""))
            return _asserts.LoadParticle(path);
        else if(parent.IsNullBoolWarning())
                return _asserts.InstantiateParticle(path, position.position);
            else
                return _asserts.InstantiateParticleWithParent(path, position.position, parent);
    }

    public TrailRenderer GetTrailRenderer()
    {
        return _trailRenderer;
    }

    public LineRenderer GetLineRenderer()
    {
        return _lineRenderer;
    }


}

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