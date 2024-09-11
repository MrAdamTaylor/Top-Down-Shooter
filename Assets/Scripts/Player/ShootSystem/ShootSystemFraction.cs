using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class ShootSystemFraction : CoomoonShootSystem
{
    [SerializeField] private float _distance;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private GameObject _shootTrash;

    private ShootDataShootgun _weaponData;
    private List<Vector3> _directions;

    public override void Construct(ShootData data)
    {
        _weaponData = (ShootDataShootgun)data;
    }

    public override void Shoot()
    {
        //base.Shoot();
        switch (_weaponData.AmountFractions)
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
        Vector3 direction = _weaponData.BulletPoint.transform.forward;
        _directions.Add(direction);
        Vector3 axisRight = _weaponData.BulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_weaponData.FovAngle / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_weaponData.BulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_weaponData.FovAngle / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        Vector3 axisRight2 = _weaponData.BulletPoint.up;
        Quaternion axisRotationRight2 = Quaternion.AngleAxis(_weaponData.FovAngle / 4, axisRight2);
        Vector3 rotatedDirectionRight2 = axisRotationRight2 * direction;
        _directions.Add(rotatedDirectionRight2);
        Vector3 axisLeft2 = -_weaponData.BulletPoint.up;
        Quaternion axisRotationLeft2 = Quaternion.AngleAxis(_weaponData.FovAngle / 4, axisLeft2);
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

            for (int i = 0; i < _weaponData.AmountFractions; i++)
            {
                if (Physics.Raycast(_weaponData.BulletPoint.position, _directions[i], out RaycastHit hit,
                        _distance, _layerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        EnemyComponentProvider enemyComponentProvider = hit.collider.gameObject.GetComponent<EnemyComponentProvider>();
                        Enemy enemy = enemyComponentProvider.Enemy;
                        Health component = enemy.gameObject.GetComponent<Health>();
                        component.DealDamage(_weaponData.Damage);
                    }
                    Debug.DrawRay(_weaponData.BulletPoint.position, _directions[i] * _distance,Color.red, 5f);
                    CreateLaser(_weaponData.BulletPoint.position,hit.point);
                }
                else
                {
                    Debug.DrawRay(_weaponData.BulletPoint.position, _directions[i] * _distance, Color.blue, 5f);
                    CreateLaser(_weaponData.BulletPoint.position,_weaponData.BulletPoint.position + _directions[i] * _distance);
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
        Vector3 direction = _weaponData.BulletPoint.transform.forward;
        _directions.Add(direction);
        Vector3 axisRight = _weaponData.BulletPoint.up;
        Quaternion axisRotationRight = Quaternion.AngleAxis(_weaponData.FovAngle / 2, axisRight);
        Vector3 rotatedDirectionRight = axisRotationRight * direction;
        _directions.Add(rotatedDirectionRight);
        Vector3 axisLeft = -_weaponData.BulletPoint.up;
        Quaternion axisRotationLeft = Quaternion.AngleAxis(_weaponData.FovAngle / 2, axisLeft);
        Vector3 rotatedDirectionLeft = axisRotationLeft * direction;
        _directions.Add(rotatedDirectionLeft);
        ShootFraction();
    }
}