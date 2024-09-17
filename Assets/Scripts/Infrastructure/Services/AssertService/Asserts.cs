using UnityEngine;

public class Asserts : IAsserts
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
        var prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab, at, Quaternion.identity);
    }

    public ParticleSystem LoadParticle(string impactParticleEffect)
    {
        ParticleSystem particleSystem = Resources.Load<ParticleSystem>(impactParticleEffect);
        return particleSystem;
    }

    public ParticleSystem InstantiateParticle(string impactParticleEffect, Vector3 at)
    {
        ParticleSystem particleSystem = Resources.Load<ParticleSystem>(impactParticleEffect);
        return ParticleSystem.Instantiate(particleSystem, at, Quaternion.identity);
    }

    public ParticleSystem InstantiateParticle(string impactParticleEffect, Vector3 at, Quaternion quaternion)
    {
        ParticleSystem particleSystem = Resources.Load<ParticleSystem>(impactParticleEffect);
        return ParticleSystem.Instantiate(particleSystem, at, quaternion);
    }

    public ParticleSystem InstantiateParticleWithParent(string impactParticleEffect, Vector3 at, Transform parent)
    {
        ParticleSystem prefabParticle = Resources.Load<ParticleSystem>(impactParticleEffect);
        return ParticleSystem.Instantiate(prefabParticle, at, Quaternion.identity, parent);
    }

    public TrailRenderer LoadTrailRenderer(string hotTrailPath)
    {
        TrailRenderer trailRenderer = Resources.Load<TrailRenderer>(hotTrailPath);
        return trailRenderer;
    }

    public TrailRenderer InstantiateTrailRender(string hotTrailPath, Vector3 at)
    {
        TrailRenderer trailRenderer = Resources.Load<TrailRenderer>(hotTrailPath);
        return Object.Instantiate(trailRenderer, at, Quaternion.identity);
    }

    public LineRenderer InstantiateLineRenderer(LineRenderer lineRenderer)
    {
        return Object.Instantiate(lineRenderer).GetComponent<LineRenderer>();
    }

    public LineRenderer LoadLineRenderer(string lineRendererPath)
    {
        LineRenderer lineRenderer = Resources.Load<LineRenderer>(lineRendererPath);
        return lineRenderer;
    }
}