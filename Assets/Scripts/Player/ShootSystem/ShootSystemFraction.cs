using System.Collections.Generic;
using Enemies;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class ShootSystemFraction : CoomoonShootSystem
{
    private float _distance;
    private LineRenderer _laser;
    private float _fadeDuration = 0.3f;
    private GameObject _shootTrash;

    [SerializeField] private FractionShotCharacteristics _fractionShotCharacteristics;
    
    private List<Vector3> _directions;
    private Transform _bulletPoint;
    private int _damage;
    private ISpecialEffectFactory _specialEffectFactory;

    public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
    {
        WeaponStaticShootgun staticShootgun = (WeaponStaticShootgun)staticData;
        _layerMask = Constants.WEAPON_LAYER_MASK;
        _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
        _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
        _laser = conteiner.GetLineRenderer();
        _fractionShotCharacteristics.AmountFraction = staticShootgun.AmountOfRaction;
        _fractionShotCharacteristics.Angle = staticShootgun.AmountOfRaction;
        _fractionShotCharacteristics.Distance = staticShootgun.Distance;
        _fadeDuration = staticShootgun.FadeDuration;
        _shootTrash = new GameObject("FractionTrash")
        {
            transform =
            {
                parent = transform
            }
        };
        _damage = staticData.Damage;
        _bulletPoint = staticData.BulletPoint;
        _specialEffectFactory = (ISpecialEffectFactory)ServiceLocator.Instance.GetData(typeof(ISpecialEffectFactory));
    }
    
    public override void UpdateValues(WeaponCharacteristics characteristics)
    {
        _damage = characteristics.Damage;
    }

    public override void Shoot()
    {
        if (_fractionShotCharacteristics.Distance != 0)
            _distance = _fractionShotCharacteristics.Distance;
        switch (_fractionShotCharacteristics.AmountFraction)
        {
            case 3:
                TwoAngleShoot();
                break;
            case 5:
                FourAngleShoot();
                break;
        }
    }

    private void FourAngleShoot()
    {
        _directions = new List<Vector3>();
        Vector3 direction = _bulletPoint.forward;
        _directions.Add(direction);
        Vector3 axisRight = _bulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_bulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        Vector3 axisRight2 = _bulletPoint.up;
        Quaternion axisRotationRight2 = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 4, axisRight2);
        Vector3 rotatedDirectionRight2 = axisRotationRight2 * direction;
        _directions.Add(rotatedDirectionRight2);
        Vector3 axisLeft2 = -_bulletPoint.up;
        Quaternion axisRotationLeft2 = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 4, axisLeft2);
        Vector3 rotatedDirectionLeft2 = axisRotationLeft2 * direction;
        _directions.Add(rotatedDirectionLeft2);
        ShootFraction();
    }

    private void ShootFraction()
    {
            foreach (Transform child in _shootTrash.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _fractionShotCharacteristics.AmountFraction; i++)
            {
                if (Physics.Raycast(_bulletPoint.position, _directions[i], out RaycastHit hit,
                        _distance, _layerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        EnemyComponentProvider enemyComponentProvider = hit.collider.gameObject.GetComponent<EnemyComponentProvider>();
                        Enemy enemy = enemyComponentProvider.Enemy;
                        Health component = enemy.gameObject.GetComponent<Health>();
                        component.DealDamage(_damage);
                    }
                    Debug.DrawRay(_bulletPoint.position, _directions[i] * _distance,Color.red, 5f);
                    Vector3 endPosition = hit.point;
                    _specialEffectFactory.CreateLaser(this, _laser,  
                        _bulletPoint.position, 
                        endPosition,
                        _fadeDuration, _shootTrash.transform);
                }
                else
                {
                    Debug.DrawRay(_bulletPoint.position, _directions[i] * _distance, Color.blue, 5f);
                    Vector3 endPosition = hit.point;
                     _specialEffectFactory.CreateLaser(this, _laser, _bulletPoint.position, 
                         _bulletPoint.position + _directions[i] * _distance, _fadeDuration, _shootTrash.transform);
                }
            }
    }

    private void TwoAngleShoot()
    {
        _directions = new List<Vector3>();
        Vector3 direction = _bulletPoint.forward;
        _directions.Add(direction);
        Vector3 axisRight = _bulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_bulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_fractionShotCharacteristics.Angle / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        ShootFraction();
    }
}

[System.Serializable]
public struct FractionShotCharacteristics
{
    public int AmountFraction;
    public float Angle;
    public float Distance;
}