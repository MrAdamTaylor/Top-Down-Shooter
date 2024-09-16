using System;
using System.Collections.Generic;
using System.Linq;
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
        for (int i = 0; i < weapon.Length; i++)
        {
            WeaponStaticData data = _weaponDictionary[weapon[i].TypeWeapon];
            data.BulletPoint = weapon[i].transform.Find("Point");
            ShootControlSystem shootControlSystem = weapon[i].AddComponent<ShootControlSystem>();
            weapon[i].Construct(shootControlSystem);
            _weaponComponentHandler.GetShootSystem(weapon[i].transform, data.WType);
            CoomoonShootSystem shootSystem = weapon[i].gameObject.GetComponent<CoomoonShootSystem>();
            shootControlSystem.Construct(data, _weaponEffectsConteiner, shootSystem);
            _weaponComponentHandler.GetInputSytem(weapon[i].transform, data.InpType);
            if (data.IsMuzzle)
            {
                weapon[i].AddComponent<MuzzleFlashEffect>();
            }
            if (data.IsAmmo)
            {
                weapon[i].AddComponent<AmmoController>();
                weapon[i].Construct(shootControlSystem);
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
        _particleSystem = _asserts.InstantiateParticle(Constants.IMPACT_PARTICLE_EFFECT);
        _trailRenderer = _asserts.InstantiateTrailRenderer(Constants.HOT_TRAIL_PATH);
        _lineRenderer = _asserts.InstantiateLineRenderer(Constants.LINE_RENDERER_PATH);
    }


    public ParticleSystem GetParticleEffect()
    {
        return _particleSystem;
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