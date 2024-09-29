using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class WeaponEffectsConteiner
{
    private IAsserts _asserts;

    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;
    private LineRenderer _lineRenderer;

    public WeaponEffectsConteiner(IAsserts asserts)
    {
        _asserts = asserts;
        _particleSystem = _asserts.LoadParticle(PrefabPath.IMPACT_PARTICLE_EFFECT);
        _trailRenderer = _asserts.LoadTrailRenderer(PrefabPath.HOT_TRAIL_PATH);
        _lineRenderer = _asserts.LoadLineRenderer(PrefabPath.LINE_RENDERER_PATH);
    }


    public ParticleSystem GetParticleEffect(string path = "", Transform position = null, Transform parent = null)
    {
        if (path.IsEmpty())
            return _particleSystem;
        if (position.IsNullBoolWarning(""))
            return _asserts.LoadParticle(path);
        else if(parent.IsNullBoolWarning())
            return _asserts.InstantiateParticle(path, position.position);
        else
            return _asserts.InstantiateParticleWithParent(path, position.position, parent);
    }

    public TrailRenderer GetTrailRenderer()
    {
        return _trailRenderer;
    }

    public LineRenderer GetLineRenderer()
    {
        return _lineRenderer;
    }


}