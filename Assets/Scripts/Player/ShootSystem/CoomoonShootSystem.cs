using System.Collections;
using Enemies;
using UnityEngine;

public class CoomoonShootSystem : MonoBehaviour, IShootSystem
{
    [SerializeField] protected Player _player;
    [SerializeField] protected ParticleSystem _shootingParticle;
    [SerializeField] protected ParticleSystem _impactParticle;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected TrailRenderer _trailRenderer;

     protected float _lastShootTime;
     protected float _realDistance = Constants.DEFAULT_MAXIMUM_FIRING_RANGE;
     protected float _realBulletSpeed = Constants.DEFAULT_BULLET_SPEED;

     private ShootData _weaponData;
     
     
     public virtual void Construct(ShootData data)
     {
         _weaponData = data;
     }

     public virtual void Shoot()
    {
        if (_lastShootTime + _weaponData.Delay < Time.time)
        {
            _shootingParticle.Play();
            Vector3 direction = GetDirection();
            if (Physics.Raycast(_weaponData.BulletPoint.position, direction, out RaycastHit hit, _realDistance, _layerMask))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Debug.Log($"Имя {hit.collider.gameObject.name}");

                    Checker checker = hit.collider.gameObject.GetComponent<Checker>();
                    Enemy enemy = checker.Enemy;
                    Health component = enemy.gameObject.GetComponent<Health>();
                    component.DealDamage(_weaponData.Damage);
                    //Debug.Log($"Отнять {_weaponData.Damage} единиц здоровья!");
                }

                TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                _lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, _weaponData.BulletPoint.position + GetDirection() * _realDistance, Vector3.zero, false));

                _lastShootTime = Time.time;
            }
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

            remainingDistance -= _realBulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Instantiate(_impactParticle, HitPoint, Quaternion.LookRotation(HitNormal));
        }
        Destroy(trail.gameObject, trail.time);
    }
    
    private Vector3 GetDirection()
    {
        Vector3 direction = _player.transform.forward;
        return direction;
    }
}


public class ShootDataGranadeLauncher : ShootData
{
    public ShootDataGranadeLauncher(int damage, Transform shootPoint, float speedFireRange) : base(damage, shootPoint, speedFireRange)
    {
    }
}