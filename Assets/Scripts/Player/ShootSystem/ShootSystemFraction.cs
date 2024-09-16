using System.Collections;
using System.Collections.Generic;
using Enemies;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class ShootSystemFraction : CoomoonShootSystem
{
    [SerializeField] private float _distance;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private GameObject _shootTrash;

    //private ShootDataShootgun _weaponData;
    private List<Vector3> _directions;
    private Transform _bulletPoint;
    private int _ammountFraction;
    private float _angleFov;
    private int _damage;

    public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
    {
        WeaponStaticShootgun staticShootgun = (WeaponStaticShootgun)staticData;
        _layerMask = Constants.WEAPON_LAYER_MASK;
        _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
        _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
        _laser = conteiner.GetLineRenderer();
        _distance = staticShootgun.Distance;
        _ammountFraction = staticShootgun.AmountOfRaction;
        _angleFov = staticShootgun.FovAngle;
        _fadeDuration = staticShootgun.FadeDuration;
        _shootTrash = new GameObject("FractionTrash");
        _shootTrash.transform.parent = transform;
        _damage = staticData.Damage;
        _bulletPoint = staticData.BulletPoint;
    }

    /*public override void Construct(ShootData data)
    {
        _weaponData = (ShootDataShootgun)data;
    }*/

    public override void Shoot()
    {
        //base.Shoot();
        switch (_ammountFraction)
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
        Vector3 direction = _bulletPoint.transform.forward;
        _directions.Add(direction);
        Vector3 axisRight = _bulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_angleFov / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_bulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_angleFov / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        Vector3 axisRight2 = _bulletPoint.up;
        Quaternion axisRotationRight2 = Quaternion.AngleAxis(_angleFov / 4, axisRight2);
        Vector3 rotatedDirectionRight2 = axisRotationRight2 * direction;
        _directions.Add(rotatedDirectionRight2);
        Vector3 axisLeft2 = -_bulletPoint.up;
        Quaternion axisRotationLeft2 = Quaternion.AngleAxis(_angleFov / 4, axisLeft2);
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

            for (int i = 0; i < _ammountFraction; i++)
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
                    CreateLaser(_bulletPoint.position,hit.point);
                }
                else
                {
                    Debug.DrawRay(_bulletPoint.position, _directions[i] * _distance, Color.blue, 5f);
                    CreateLaser(_bulletPoint.position,_bulletPoint.position + _directions[i] * _distance);
                }
            }
            
        
    }

    void CreateLaser(Vector3 start,Vector3 end)
    {
        LineRenderer lr = Instantiate(_laser).GetComponent<LineRenderer>();
        lr.transform.parent = _shootTrash.transform;
        lr.SetPositions(new Vector3[2] {start, end});
        StartCoroutine(FadeLaser(lr));
    }

    IEnumerator FadeLaser(LineRenderer lr)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / _fadeDuration;
            lr.startColor = new Color(_laser.startColor.r,_laser.startColor.g,_laser.startColor.b, alpha);
            lr.endColor = new Color(_laser.endColor.r,_laser.endColor.g,_laser.endColor.b, alpha);
            yield return null;
        }
    }

    private void TwoAngleShoot()
    {
        _directions = new List<Vector3>();
        Vector3 direction = _bulletPoint.transform.forward;
        _directions.Add(direction);
        Vector3 axisRight = _bulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_angleFov / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_bulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_angleFov / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        ShootFraction();
    }
}