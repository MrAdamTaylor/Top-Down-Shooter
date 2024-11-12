using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configs;
using Enemies;
using Enemies.EnemyStateMachine;
using Enemies.EnemyStateMachine.Components;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Logic;
using Logic.Spawners;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Infrastructure.Services.AbstractFactory
{
    internal class EnemyFactory : IEnemyFactory 
    {
        private IAssertByObj<GameObject> _enemySkinsAssert;
        private  IAsserByAddressableObj<GameObject, AssetReferenceGameObject> _enemySkinsAssertAddressable;
        private List<List<GameObject>> _allEnemiesSkins = new();
        private List<int> _counts = new();
        private EnemyConfigs _enemyConfigs;
        private Dictionary<EnemyConfigs, List<GameObject>> _skinDictionary = new();

        public EnemyFactory(AssertBuilder builder, EnemyConfigs[] configs)
        {
            _enemySkinsAssert = builder.BuildAssertServiceByObj<GameObject>();
            _enemySkinsAssertAddressable = builder.BuildAssertServiceByAddressable<AssetReferenceGameObject>();
            for (int i = 0; i < configs.Length; i++)
            {
                List<GameObject> skins = new();
                _allEnemiesSkins.Add(skins);
                _counts.Add(configs[i].SkinsReference.Count);
                _skinDictionary.Add(configs[i], skins);
            }

            //for (int i = 0; i < _allEnemiesSkins.Count; i++)
            //{
                //_enemyConfigs = configs[i];
                LoadSkins(_allEnemiesSkins[0], _counts[0]);
            //}
            //Debug.Log($"Finish Factory: ");
        }
        
        /*public async Task<bool> Init()
        {
            var series = Enumerable.Range(1, 5).ToList();
            var tasks = new List<Task<Tuple<int, bool>>>();
            foreach (var i in series)
            {
                Console.WriteLine("Starting Process {0}", i);
                tasks.Add(DoWorkAsync(i));
            }
            foreach (var task in await Task.WhenAll(tasks))
            {
                if (task.Item2)
                {
                    Console.WriteLine("Ending Process {0}", task.Item1);
                }
            }
            return true;
        }

        public async Task<Tuple<int, bool>> DoWorkAsync(int i)
        {
            Console.WriteLine("working..{0}", i);
            await Task.Delay(1000);
            return Tuple.Create(i, true);
        }*/

        private async void LoadSkins(List<GameObject> gameObjects, int counts)
        {
            List<Task<Tuple<GameObject, bool>>> tasks = new();
            for (int i = 0; i < counts; i++)
            {
                Debug.Log($"Starting Process LoadSkins {i}");
                tasks.Add(CreateAsynSkin(i));
            }
            foreach (var task in await Task.WhenAll(tasks))
            {
                if (task.Item2)
                {
                    gameObjects.Add(task.Item1);
                    Debug.Log($"Ending Process LoadSking{task.Item1}");
                }
            }
        }

        private async Task<Tuple<GameObject, bool>> CreateAsynSkin(int i)
        {
            GameObject gameObject = await _enemySkinsAssertAddressable.Assert(_enemyConfigs.SkinsReference[i]);
            return Tuple.Create(gameObject, true);
        }


        public GameObject Create(EnemyConfigs configs, EnemySpawnPoint[] positions, GameObject parent)
        {
            GameObject enemy;

            int index = Random.Range(0, positions.Length);
            Transform randomTransform = positions[index].transform;
        
            Vector3 position = randomTransform.position;

            enemy = ChoseSkin(configs, position, parent.transform);
            
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
        

        private GameObject ChoseSkin(EnemyConfigs configs, Vector3 position, Transform parent)
        {
            GameObject objSkin;
            switch (configs.Skins.Count)
            {
                case 0:
                    throw new Exception("Enemy Count is null");
                case 1:
                    objSkin = _enemySkinsAssert.Assert(configs.Skins[0], position, parent.transform);
                    return objSkin;
                case > 1:
                    objSkin = _enemySkinsAssert.Assert(configs.Skins[Random.Range(0, configs.Skins.Count)], position, parent.transform);
                    return objSkin;
                default:
                    throw new Exception("Cannot add skin in EnemyFactory");
            }
        }

        private async Task<GameObject> ChoseAsyncSkin(EnemyConfigs configs, Vector3 position, Transform parent)
        {
            switch (configs.Skins.Count)
            {
                case 0:
                    throw new Exception("Enemy Count is null");
                case 1:
                    GameObject skin = await _enemySkinsAssertAddressable.Assert(configs.SkinsReference[0], position, parent.transform);
                    return skin;
                case > 1:
                    GameObject skinMore = await _enemySkinsAssertAddressable.Assert(configs.SkinsReference[Random.Range(0, configs.Skins.Count)], position, parent.transform);
                    return skinMore;
                default:
                    throw new Exception("Cannot add skin in EnemyFactory");
            }
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
            moveToPlayer.Construct(enemyObject.transform, configs.Speed);

            IEnemyAttack enemyAttack = visual.AddComponent<EnemySimpleAttack>();
            enemyAttack.Construct(enemyAnimator, configs.MinDamage,configs.MaxDamage, configs.HitBoxRadius);
        
            EnemyRotateSystem enemyRotateSystem = enemyObject.AddComponent<EnemyRotateSystem>();
            enemyRotateSystem.Construct(enemyObject.transform, player.transform);
        
            ReactionTrigger reactionTrigger = enemyObject.AddComponent<ReactionTrigger>();
            reactionTrigger.Construct(configs.MinimalToPlayerDistance, player.transform);
        
            CheckAttack attackChecker = enemyObject.AddComponent<CheckAttack>();
            attackChecker.Construct(enemyAttack, reactionTrigger);

            EnemyHealth enemyHealth = enemyObject.AddComponent<EnemyHealth>();
            enemyHealth.Construct(configs.Health, enemyAnimator);
        
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