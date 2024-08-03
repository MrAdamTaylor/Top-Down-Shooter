using System;
using System.Collections;
using UnityEngine;

public class NewShootSystem : MonoBehaviour
{
    [Range(0.1f, 10f)][SerializeField] private float _delay = 0.5f;
    [Range(0.5f, 50f)][SerializeField] private float _distance = 40f;
    
    [SerializeField] private ParticleSystem _shootingSystem;

    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] private TrailRenderer _bulletTrail;

    [SerializeField] private ParticleSystem _impactSystem;

    [SerializeField] private LayerMask Mask;
    
    [SerializeField]
    private float _bulletSpeed = 100;

    private float _lastShootTime;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Shoot");
            Shoot();
        }
    }

    public void Shoot()
    {
        if (_lastShootTime + _delay < Time.time)
        {
            _shootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(_bulletSpawn.position, direction, out RaycastHit hit, _distance, Mask))
            {
                TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawn.position, Quaternion.identity);
                Debug.Log("Запуск корутины");
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                _lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawn.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, _bulletSpawn.position + GetDirection() * 100, Vector3.zero, false));

                _lastShootTime = Time.time;
            }
        }
        else
        {
            //StopCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
            //Debug.Log($"Стрелок!  {Time.deltaTime}");
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
            Instantiate(_impactSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(trail.gameObject, trail.time);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        return direction;
    }
}