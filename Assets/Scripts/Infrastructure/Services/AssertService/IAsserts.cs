using UnityEngine;

public interface IAsserts : IService
{
    public GameObject Instantiate(string path);

    public GameObject Instantiate(string path, Vector3 at);
    ParticleSystem InstantiateParticle(string impactParticleEffect);
    TrailRenderer InstantiateTrailRenderer(string hotTrailPath);
    LineRenderer InstantiateLineRenderer(string lineRendererPath);
}