using System.Collections;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public class SpecialEffectFactory : ISpecialEffectFactory
    {
        private IAssertByObj<LineRenderer> _lineAssertObj;
        private IAssertByString<TrailRenderer> _bulletAssert;
        private IAssertByString<LineRenderer> _lineAssert;
        private IAssertByString<ParticleSystem> _particleAssert;

        public SpecialEffectFactory(AssertBuilder builder)
        {
            _particleAssert = builder.BuildAssertServiceByString<ParticleSystem>();
            _bulletAssert = builder.BuildAssertServiceByString<TrailRenderer>();
            _lineAssert = builder.BuildAssertServiceByString<LineRenderer>();
            _lineAssertObj = builder.BuildAssertServiceByObj<LineRenderer>();
        }

        public void CreateLaser(MonoBehaviour behaviour, LineRenderer lineRenderer, Vector3 start, Vector3 end, float fadeDuration, Transform parent)
        {
            LineRenderer lr = _lineAssertObj.Assert(lineRenderer);
            lr.transform.parent = parent.transform;
            lr.SetPositions(new Vector3[2] {start, end});
            behaviour.StartCoroutine(FadeLaser(lr, lineRenderer, fadeDuration));
        }

        public void CreateBullet(MonoBehaviour behaviour, Vector3 bulletPointPosition, Vector3 startPoint, 
            Vector3 endPoint, float bulletSpeed, bool madeImpact)
        {
            TrailRenderer trail = _bulletAssert.Assert(PrefabPath.HOT_TRAIL_PATH, bulletPointPosition);
            behaviour.StartCoroutine(SpawnTrail(trail, startPoint, endPoint, bulletSpeed, madeImpact));
        }

        private IEnumerator FadeLaser(LineRenderer lr, LineRenderer laser, float fadeDuration)
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

        private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, float bulletSpeed, bool madeImpact)
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
                _particleAssert.Assert(PrefabPath.IMPACT_PARTICLE_EFFECT, hitPoint, Quaternion.LookRotation(hitNormal));
            }
            Object.Destroy(trail.gameObject, trail.time);
        }
    }
}