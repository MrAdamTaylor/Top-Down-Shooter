using UnityEngine;

public interface IAsserts : IService
{
    GameObject Instantiate(string path);

    GameObject Instantiate(string path, Vector3 at);
    ParticleSystem LoadParticle(string impactParticleEffect);
    
    ParticleSystem InstantiateParticle(string impactParticleEffect, Vector3 at);
    
    ParticleSystem InstantiateParticle(string impactParticleEffect, Vector3 at, Quaternion quaternion);

    ParticleSystem InstantiateParticleWithParent(string impactParticleEffect, Vector3 at, Transform parent);
    
    TrailRenderer LoadTrailRenderer(string hotTrailPath);

    TrailRenderer InstantiateTrailRender(string hotTrailPath, Vector3 at);
    LineRenderer LoadLineRenderer(string lineRendererPath);

    LineRenderer InstantiateLineRenderer(LineRenderer lineRenderer);
}