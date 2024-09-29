using UnityEngine;

public interface ISpecialEffectFactory : IGameFactory
{
    void CreateLaser(MonoBehaviour behaviour, LineRenderer laser, Vector3 bulletPointPosition,
        Vector3 hitInfoPoint, float fadeDuration, Transform shootTrashTransform);

    void CreateBullet(MonoBehaviour behaviour, Vector3 bulletPointPosition, Vector3 startPoint, 
        Vector3 endPoint, float bulletSpeed, bool madeImpact);
}
