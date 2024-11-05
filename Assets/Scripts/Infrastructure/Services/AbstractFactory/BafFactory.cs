using System;
using Configs;
using Infrastructure.Services.AssertService;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BafFactory : IBafFactory
    {
        private readonly IAssertByObj<GameObject> _bafAssert;

        public BafFactory(AssertBuilder assertBuilder)
        {
            _bafAssert = assertBuilder.BuildAssertServiceByObj<GameObject>();
        }

        public void Create(BafConfigs bafConfigs, Vector3 position)
        {
            GameObject baff = _bafAssert.Assert(bafConfigs.Visual, position);
            switch (bafConfigs)
            {
                case AmmoBaffConfigs ammoBaffConfigs:
                {
                    CreateAmmo(baff,ammoBaffConfigs);
                    break;
                }
                case HealthBaffConfigs healthBaffConfigs:
                {
                    CreateHealth(baff,healthBaffConfigs);
                    break;
                }
                case null:
                    throw new ArgumentException("Enemy configs in Factory is null");
                default:
                    throw new Exception("Unknown Enemy configs for Factory");
            }
        }

        private void CreateHealth(GameObject baff, HealthBaffConfigs healthBaffConfigs)
        {
            
        }

        private void CreateAmmo(GameObject baff, AmmoBaffConfigs ammoBaffConfigs)
        {
            
        }
    }
}