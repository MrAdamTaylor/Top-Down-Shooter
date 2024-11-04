using System;
using System.Collections.Generic;
using Configs;
using Enemies;
using Enemies.EnemyStateMachine;
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

        private void CreateWalking(GameObject enemyObject, EnemyWalkingConfigs configs)
        {
            Transform visual = enemyObject.transform.Find(ConstantsSceneObjects.PREFAB_MESH_COMPONENT_NAME);
            Transform physic = enemyObject.transform.Find(ConstantsSceneObjects.PREFAB_PHYSIC_COMPONENT_NAME);
            PlayLoopComponentProvider provider = physic.GetComponent<PlayLoopComponentProvider>();
            Player.Player player = (Player.Player)ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
        
            EnemyAnimator enemyAnimator = visual.AddComponent<EnemyAnimator>();
            enemyAnimator.Construct();
        
            IEnemyMoveSystem moveToPlayer = enemyObject.AddComponent<AgentMoveToPlayer>();
            moveToPlayer.Construct(enemyObject.transform, 4f);

            IEnemyAttack enemyAttack = visual.AddComponent<EnemySimpleAttack>();
            enemyAttack.Construct(enemyAnimator, 10,20);
        
            EnemyRotateSystem enemyRotateSystem = enemyObject.AddComponent<EnemyRotateSystem>();
            enemyRotateSystem.Construct(enemyObject.transform, player.transform);
        
            ReactionTrigger reactionTrigger = enemyObject.AddComponent<ReactionTrigger>();
            reactionTrigger.Construct(1.5f, player.transform);
        
            CheckAttack attackChecker = enemyObject.AddComponent<CheckAttack>();
            attackChecker.Construct(enemyAttack, reactionTrigger);

            EnemyHealth enemyHealth = enemyObject.AddComponent<EnemyHealth>();
            enemyHealth.Construct(20, enemyAnimator);
        
            EnemyDeath enemyDeath = enemyObject.AddComponent<EnemyDeath>();
            enemyDeath.Construct(enemyHealth, enemyAnimator, 1);
            provider.AddToProvideComponent(enemyHealth);
        
            EnemyStateMachine stateMachine = enemyObject.AddComponent<EnemyStateMachine>();
            stateMachine.Construct(enemyAnimator, moveToPlayer,enemyRotateSystem, enemyAttack, enemyHealth, physic.gameObject);
            //return enemyObject;
            List<EnemyStateMachine> enemies =
                (List<EnemyStateMachine>)ServiceLocator.ServiceLocator.Instance.GetData(typeof(List<EnemyStateMachine>));
            enemies.Add(stateMachine);
            
        }

        private void CreateKamikaze(GameObject enemy, EnemyKamikazeConfigs configs)
        {
            Debug.Log($"<color=green> {configs.Name} is Created Registered</color>");
        }
    }
}