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

    public ParticleSystem InstantiateParticle(string impactParticleEffect)
    {
        ParticleSystem particleSystem = Resources.Load<ParticleSystem>(impactParticleEffect);
        return particleSystem;
    }

    public TrailRenderer InstantiateTrailRenderer(string hotTrailPath)
    {
        TrailRenderer trailRenderer = Resources.Load<TrailRenderer>(hotTrailPath);
        return trailRenderer;
    }

    public LineRenderer InstantiateLineRenderer(string lineRendererPath)
    {
        LineRenderer lineRenderer = Resources.Load<LineRenderer>(lineRendererPath);
        return lineRenderer;
    }
}