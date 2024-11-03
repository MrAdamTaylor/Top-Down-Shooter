using System;
using System.Collections.Generic;
using Configs;
using Enemies;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Logic;
using Logic.Spawners;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Services.AbstractFactory
{
    internal class EnemyFactory : IEnemyFactory 
    {
        private IAssertByObj<GameObject> _enemySkinsAssert;
    
        public EnemyFactory(AssertBuilder builder)
        {
            _enemySkinsAssert = builder.BuildAssertServiceByObj<GameObject>();
        }

        public GameObject Create(EnemyConfigs configs, EnemySpawnPoint[] positions, GameObject parent)
        {
            GameObject enemy;

            int index = Random.Range(0, positions.Length);
            Transform randomTransform = positions[index].transform;
        
            Vector3 position = randomTransform.position;
        
            switch (configs.Skins.Count)
            {
                case 0:
                    throw new Exception("Enemy Count is null");
                    break;
                case 1:
                    enemy = _enemySkinsAssert.Assert(configs.Skins[0], position);
                    break;
                case > 1:
                    enemy = _enemySkinsAssert.Assert(configs.Skins[Random.Range(0, configs.Skins.Count)], position);
                    break;
                default:
                    throw new Exception("Cannot add skin in EnemyFactory");
            }
            enemy.transform.parent = parent.transform;
            switch (configs)
            {
                case EnemyKamikazeConfigs kamikazeConfigs:
                {
                    CreateKamikaze(enemy,kamikazeConfigs);
                    break;
                }
                case EnemyWalkingConfigs walkingConfigs:
                {
                    CreateWalking(enemy,walkingConfigs);
                    break;
                }
                case EnemyTurretConfigs turretConfigs:
                {

                    CreateTurret(enemy, turretConfigs);
                    break;
                }
                case null:
                    throw new ArgumentException("Enemy configs in Factory is null");
                default:
                    throw new Exception("Unknown Enemy configs for Factory");
            }
        
            return enemy;
        }

        private void CreateTurret(GameObject enemy, EnemyTurretConfigs configs)
        {
            Debug.Log($"<color=green>  {configs.Name} is Created Registered</color>");
        }

        private void CreateWalking(GameObject enemy, EnemyWalkingConfigs configs)
        {
            Transform visual = enemy.transform.Find(ConstantsSceneObjects.PREFAB_MESH_COMPONENT_NAME);
            Transform physic = enemy.transform.Find(ConstantsSceneObjects.PREFAB_PHYSIC_COMPONENT_NAME);
            PlayLoopComponentProvider provider = physic.GetComponent<PlayLoopComponentProvider>();
            Player.Player player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));

            EnemyAnimator enemyAnimator = visual.AddComponent<EnemyAnimator>();
            enemyAnimator.Construct();
            //EnemyAnimationEvent enemyAnimationEvent = visual.AddComponent<EnemyAnimationEvent>();
        
            //MoveToPlayer moveToPlayer = enemy.AddComponent<MoveToPlayer>();
            IEnemyMoveSystem moveToPlayer = enemy.AddComponent<AgentMoveToPlayer>();
            moveToPlayer.Construct(enemy.transform, configs.Speed);
            EnemyRotateSystem enemyRotateSystem = enemy.AddComponent<EnemyRotateSystem>();
            enemyRotateSystem.Construct(enemy.transform, player.transform);
            ReactionTrigger reactionTrigger = enemy.AddComponent<ReactionTrigger>();
            reactionTrigger.Construct(configs.RadiusDetection, player.transform);
        
            EnemyAttack enemyAttack = visual.AddComponent<EnemyAttack>();
            enemyAttack.Construct(enemyAnimator, configs.MinDamage, configs.MaxDamage);
            CheckAttack attackChecker = enemy.AddComponent<CheckAttack>();
            attackChecker.Construct(enemyAttack, reactionTrigger);
            EnemyHealth enemyHealth = enemy.AddComponent<EnemyHealth>();
            enemyHealth.Construct(configs.Health, enemyAnimator);
            EnemyDeath enemyDeath = enemy.AddComponent<EnemyDeath>();
            enemyDeath.Construct(enemyHealth, enemyAnimator, configs.Reward);
            provider.AddToProvideComponent(enemyHealth);
        
            EnemyController enemyController = enemy.AddComponent<EnemyController>();
            enemyController.Construct(moveToPlayer, enemyAnimator, enemyRotateSystem, enemyAttack, 
                configs.MinimalToPlayerDistance, enemyDeath, physic.gameObject, enemyHealth);

            List<EnemyController> enemies =
                (List<EnemyController>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyController>));
            enemies.Add(enemyController);
        }

        private void CreateKamikaze(GameObject enemy, EnemyKamikazeConfigs configs)
        {
            Debug.Log($"<color=green> {configs.Name} is Created Registered</color>");
        }
    }
}