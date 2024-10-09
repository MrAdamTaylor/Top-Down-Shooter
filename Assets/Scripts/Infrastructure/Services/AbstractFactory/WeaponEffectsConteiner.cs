using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using Infrastructure.Services.AssertService.ExtendetAssertService;
using UnityEngine;

public class WeaponEffectsConteiner
{
    private IAsserts _asserts;

    private IAssertByString<LineRenderer> _lineAssert;
    private IAssertByString<ParticleSystem> _particleAssert;
    
    //private AssertServiceString<LineRenderer> _lineAssert;
    //private AssertServiceString<ParticleSystem> _particleAssert;

    private TrailRenderer _trailRenderer;
    private ParticleSystem _particleSystem;
    private LineRenderer _lineRenderer;

    public WeaponEffectsConteiner(IAsserts asserts)
    {
        _asserts = asserts;
        _particleSystem = _asserts.LoadParticle(PrefabPath.IMPACT_PARTICLE_EFFECT);
        //_trailRenderer = _asserts.LoadTrailRenderer(PrefabPath.HOT_TRAIL_PATH);
        //_lineRenderer = _asserts.LoadLineRenderer(PrefabPath.LINE_RENDERER_PATH);
    }

    public WeaponEffectsConteiner(AssertBuilder builder)
    {
        _lineAssert = builder.LoadService<LineRenderer>();
        _particleAssert = builder.BuildAssertServiceByString<ParticleSystem>();
        //_particleSystem = _asserts.LoadParticle(PrefabPath.IMPACT_PARTICLE_EFFECT);
        //_lineRenderer = _asserts.LoadLineRenderer(PrefabPath.LINE_RENDERER_PATH);
        _particleSystem = _particleAssert.Assert(PrefabPath.IMPACT_PARTICLE_EFFECT);
        //_lineRenderer = _lineAssert.Assert(PrefabPath.LINE_RENDERER_PATH);
        _lineRenderer = _lineAssert.Assert(PrefabPath.LINE_RENDERER_PATH);
    }

    public ParticleSystem GetParticleEffect(string path = "", Transform position = null, Transform parent = null)
    {
        if (path.IsEmpty())
            return _particleSystem;
        if (position.IsNullBoolWarning(""))
            return _particleAssert.Assert(path);
        else if(parent.IsNullBoolWarning())
            return _particleAssert.Assert(path, position.position);
        else
            return _particleAssert.Assert(path, position.position, parent);
    }

    public LineRenderer GetLineRenderer()
    {
        return _lineRenderer;
    }
}