using System.Collections;
using UnityEngine;

public class CoomoonShootSystem : MonoBehaviour, IShootSystem
{
    [Range(0.1f,5f)][SerializeField] protected float _delay;
    [SerializeField] protected ParticleSystem _shootingParticle;
    [SerializeField] protected Transform _bulletPoint;
    [SerializeField] protected ParticleSystem _impactParticle;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected TrailRenderer _trailRenderer;

     protected float _lastShootTime;
     protected float _realDistance = Constants.DEFAULT_MAXIMUM_FIRING_RANGE;
     protected float _realBulletSpeed = Constants.DEFAULT_BULLET_SPEED;

    
    public virtual void Shoot()
    {
        if (_lastShootTime + _delay < Time.time)
        {
            _shootingParticle.Play();
            Vector3 direction = transform.forward;
            if (Physics.Raycast(_bulletPoint.position, direction, out RaycastHit hit, _realDistance, _layerMask))
            {
                TrailRenderer trail = Instantiate(_trailRenderer, _bulletPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                _lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(_trailRenderer, _bulletPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, _bulletPoint.position + transform.forward * _realDistance, Vector3.zero, false));

                _lastShootTime = Time.time;
            }
        }
    }
    
    protected IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
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
}