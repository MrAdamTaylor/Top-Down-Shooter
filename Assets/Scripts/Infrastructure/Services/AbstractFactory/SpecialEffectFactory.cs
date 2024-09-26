using System.Collections;
using UnityEngine;

public class SpecialEffectFactory : ISpecialEffectFactory
{
    private IAsserts _asserts;

    public SpecialEffectFactory(IAsserts asserts)
    {
        _asserts = asserts;
    }

    public void CreateLaser(MonoBehaviour behaviour, LineRenderer lineRenderer, Vector3 start, Vector3 end, float fadeDuration, Transform parent)
    {
        LineRenderer lr = _asserts.InstantiateLineRenderer(lineRenderer);
        lr.transform.parent = parent.transform;
        lr.SetPositions(new Vector3[2] {start, end});
        behaviour.StartCoroutine(FadeLaser(lr, lineRenderer, fadeDuration));
    }

    public void CreateBullet(MonoBehaviour behaviour, Vector3 bulletPointPosition, Vector3 startPoint, 
        Vector3 endPoint, float bulletSpeed, bool madeImpact)
    {
        TrailRenderer trail = _asserts.InstantiateTrailRender(Constants.HOT_TRAIL_PATH, bulletPointPosition);
        behaviour.StartCoroutine(SpawnTrail(trail, startPoint, endPoint, bulletSpeed, madeImpact));
    }
    

    IEnumerator FadeLaser(LineRenderer lr, LineRenderer laser, float fadeDuration)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            lr.startColor = new Color(laser.startColor.r,laser.startColor.g,laser.startColor.b, alpha);
            lr.endColor = new Color(laser.endColor.r,laser.endColor.g,laser.endColor.b, alpha);
            yield return null;
        }
    }
    
    IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, float bulletSpeed, bool madeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        trail.transform.position = hitPoint;
        if (madeImpact)
        {
            _asserts.InstantiateParticle(Constants.IMPACT_PARTICLE_EFFECT, hitPoint, Quaternion.LookRotation(hitNormal));
        }
        Object.Destroy(trail.gameObject, trail.time);
    }
    

    
}