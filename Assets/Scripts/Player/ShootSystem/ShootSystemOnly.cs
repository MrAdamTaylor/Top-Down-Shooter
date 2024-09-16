using System.Collections;
using Enemies;
using EnterpriceLogic.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class ShootSystemOnly : CoomoonShootSystem
{
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _distance;

    private Transform _bulletPoint;
    private int _damage;
    
    public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
    {
        _layerMask = Constants.WEAPON_LAYER_MASK;
        _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
        _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
        _impactParticle = conteiner.GetParticleEffect();
        _trailRenderer = conteiner.GetTrailRenderer();
        _bulletSpeed = Constants.DEFAULT_BULLET_SPEED;
        _distance = Constants.DEFAULT_BULLET_DISTANCE;
        _damage = staticData.Damage;
        _bulletPoint = staticData.BulletPoint;
    }

    public override void Shoot()
    {
        Vector3 direction = GetDirection();
        if (Physics.Raycast(_bulletPoint.position, direction, out RaycastHit hit, _distance, _layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyComponentProvider enemyComponentProvider = hit.collider.gameObject.GetComponent<EnemyComponentProvider>();
                Enemy enemy = enemyComponentProvider.Enemy;
                Health component = enemy.gameObject.GetComponent<Health>();
                component.DealDamage(_damage);
            }
            TrailRenderer trail = Instantiate(_trailRenderer, _bulletPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
        }
        else
        {
            TrailRenderer trail = Instantiate(_trailRenderer, _bulletPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, _bulletPoint.position + GetDirection() * _distance, Vector3.zero, false));
        }
    }
    
    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= _bulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            
            Instantiate(_impactParticle, HitPoint, Quaternion.LookRotation(HitNormal));
        }
        Destroy(trail.gameObject, trail.time);
    }
}