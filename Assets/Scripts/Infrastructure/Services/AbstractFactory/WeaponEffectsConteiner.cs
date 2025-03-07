using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using Infrastructure.Services.AssertService;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public class WeaponEffectsConteiner
    {
        private IAssertByString<LineRenderer> _lineAssert;
        private IAssertByString<ParticleSystem> _particleAssert;

        private TrailRenderer _trailRenderer;
        private ParticleSystem _particleSystem;
        private LineRenderer _lineRenderer;


        public WeaponEffectsConteiner(AssertBuilder builder)
        {
            _lineAssert = builder.LoadService<LineRenderer>();
            _particleAssert = builder.BuildAssertServiceByString<ParticleSystem>();
            _particleSystem = _particleAssert.Assert(PrefabPath.IMPACT_PARTICLE_EFFECT);
            _lineRenderer = _lineAssert.Assert(PrefabPath.LINE_RENDERER_PATH);
        }

        public ParticleSystem GetParticleEffect(string path = "", Transform position = null, Transform parent = null)
        {
            if (string.IsNullOrEmpty(path))
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
}