using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootSystem : CommonShootSystem
{
    [SerializeField] private ParticleSystem _shootingParticle;
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _distance;

    private float _lastShootTime;
    
    public override void Shoot()
    {
        
            Vector3 direction = GetDirection();
            if (Physics.Raycast(_weaponData.BulletPoint.position, direction, out RaycastHit hit, _distance, _layerMask))
            {
                TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                _lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, _weaponData.BulletPoint.position + GetDirection() * _distance, Vector3.zero, false));

                _lastShootTime = Time.time;
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