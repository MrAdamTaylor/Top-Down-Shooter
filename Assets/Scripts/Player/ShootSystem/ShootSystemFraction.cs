using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Math;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

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
            case 7:
                SevenAngleShoot();
                break;
        }
    }

    private void SevenAngleShoot()
    {
        throw new System.NotImplementedException();
    }

    private void FourAngleShoot()
    {
        _shootingParticle.Play();
        TrailRenderer[] trailRenderers = new TrailRenderer[_weaponData.AmountFractions];
        Vector3[] vecPos = new Vector3[_weaponData.AmountFractions];
        Vector3[] vecNorm = new Vector3[_weaponData.AmountFractions];
        bool[] bools = new bool[_weaponData.AmountFractions];
        //Debug.Log(_lastShootTime+_weaponData.Delay);
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
        _realDistance = _distance;
        
        if (_lastShootTime + _weaponData.Delay < Time.time)
        {
            foreach (Transform child in _shootTrash.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _weaponData.AmountFractions; i++)
            {
                //Debug.Log(_directions[i]);
                if (Physics.Raycast(_weaponData.BulletPoint.position, _directions[i], out RaycastHit hit,
                        _realDistance, _layerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {Checker checker = hit.collider.gameObject.GetComponent<Checker>();
                        Enemy enemy = checker.Enemy;
                        Health component = enemy.gameObject.GetComponent<Health>();
                        component.DealDamage(_weaponData.Damage);
                        //Debug.Log($"Имя {hit.collider.gameObject.name}");
                        /*Health component = gameObject.GetComponent<Health>();
                        component.DealDamage(_weaponData.Damage);*/
                    }
                    //Debug.Log("Есть контакт");
                    Debug.DrawRay(_weaponData.BulletPoint.position, _directions[i] * _realDistance,Color.red, 5f);
                    //TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);
                    //trailRenderers[i] = trail;
                    //vecPos[i] = hit.point;
                    //vecNorm[i] = hit.normal;
                    //bools[i] = true;

                    //StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                    //_lastShootTime = Time.time;
                    CreateLaser(_weaponData.BulletPoint.position,hit.point);
                }
                else
                {
                    Debug.DrawRay(_weaponData.BulletPoint.position, _directions[i] * _realDistance, Color.blue, 5f);
                    //Debug.Log("Нет контакта!");
                    //TrailRenderer trail = Instantiate(_trailRenderer, _weaponData.BulletPoint.position, Quaternion.identity);
                    //trailRenderers[i] = trail;
                    //vecPos[i] = _weaponData.BulletPoint.position + _directions[i] * _realDistance;
                    //vecNorm[i] = Vector3.zero;
                    //bools[i] = false;
                    //StartCoroutine(SpawnTrail(trail, _weaponData.BulletPoint.position + _directions[i] * _realDistance, Vector3.zero, false));

                    //_lastShootTime = Time.time;
                    CreateLaser(_weaponData.BulletPoint.position,_weaponData.BulletPoint.position + _directions[i] * _realDistance);
                }
            }

            
            //StartCoroutine(SpawnFiveTrails(trailRenderers, vecPos, vecNorm, bools));
            _lastShootTime = Time.time;
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
        //Destroy(lr);
    }

    /*private IEnumerator SpawnFiveTrails(TrailRenderer[] trails, Vector3[] hitPoint, Vector3[] HitNormal, bool[] MadeImpact)
    {
        Queue<float> remainingDistances = new Queue<float>();
        List<float> distances = new List<float>();
        List<Vector3> startPositions = new List<Vector3>();
        List<float> Endeddistances = new List<float>();
        for (int i = 0; i < _weaponData.AmountFractions; i++)
        {
            Vector3 startPosition = trails[i].transform.position;
            startPositions.Add(startPosition);
            float distance = Vector3.Distance(trails[i].transform.position, hitPoint[i]);
            Endeddistances.Add(distance);
            float remainingDistance = distance;
            distances.Add(remainingDistance);
        }

        distances.QuickSort(0, _weaponData.AmountFractions-1, startPositions, Endeddistances);
        //distances.Sort();

        for (int i = 0; i < _weaponData.AmountFractions; i++)
        {
            remainingDistances.Enqueue(distances[i]);
        }

        while (remainingDistances.Count > 0)
        {
            for (int i = 0; i < _weaponData.AmountFractions; i++)
            {
                trails[i].transform.position = Vector3.Lerp(startPositions[i], hitPoint[i], 1 - (distances[i] / Endeddistances[i]));

                distances[i] -= _realBulletSpeed * Time.deltaTime;
                if (distances[i] <= 0)
                {
                    remainingDistances.Dequeue();
                }
            }
            yield return null;
        }

        for (int i = 0; i < _weaponData.AmountFractions; i++)
        {
            trails[i].transform.position = hitPoint[i];
            if (MadeImpact[i])
            {
                Instantiate(_impactParticle, hitPoint[i], Quaternion.LookRotation(HitNormal[i]));
            }
            Destroy(trails[i].gameObject, trails[i].time);
        }
    }*/

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        //Debug.Log("Луч сделан!");
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

    private void TwoAngleShoot()
    {
        
    }
}