using System;
using Configs;
using EnterpriceLogic;
using Infrastructure.Services.AssertService;
using Logic.Bafs;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public class BafFactory : IBafFactory
    {
        private const float DETECT_PLAYER_RADIUS = 1f;
        
        private IAssertByObj<GameObject> _bafAssert;

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
            Player.Player player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
            
            ReactionTrigger reactionTrigger = baff.AddComponent<ReactionTrigger>();
            reactionTrigger.Construct(DETECT_PLAYER_RADIUS, player.transform);

            IBaffComponent baffComponent = baff.AddComponent<HealthBaffComponent>();
            baffComponent.Construct(player, healthBaffConfigs);
            
            CheckPlayerOnBaff checkPlayerOnBaff = baff.AddComponent<CheckPlayerOnBaff>();
            checkPlayerOnBaff.Construct(baffComponent, reactionTrigger);

        }

        private void CreateAmmo(GameObject baff, AmmoBaffConfigs ammoBaffConfigs)
        {
            Player.Player player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
            
            ReactionTrigger reactionTrigger = baff.AddComponent<ReactionTrigger>();
            reactionTrigger.Construct(DETECT_PLAYER_RADIUS, player.transform);

            IBaffComponent baffComponent = baff.AddComponent<AmmoBaffComponent>();
            baffComponent.Construct(player, ammoBaffConfigs);
            
            CheckPlayerOnBaff checkPlayerOnBaff = baff.AddComponent<CheckPlayerOnBaff>();
            checkPlayerOnBaff.Construct(baffComponent, reactionTrigger);
        }
    }
}