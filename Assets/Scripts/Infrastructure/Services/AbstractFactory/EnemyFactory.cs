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
using Infrastructure.StateMachine.States;
using Logic;
using Logic.Spawners;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Infrastructure.Services.AbstractFactory
{
    internal class EnemyFactory : IEnemyFactory, ISubscrible
    {
        private static Vector3 ENEMY_DEFAULT_SPAWN = new Vector3(0,-50,0);
        
        private Action _endAsyncSkinLoad;
        
        private  IAsserByAddressableObj<GameObject, AssetReferenceGameObject> _enemySkinsAssertAddressable;
        private List<List<GameObject>> _allEnemiesSkins = new();
        private List<(GameObject,int,int)> _fullSkins = new();
        private List<(int,int)> _scinsNumber = new();
        private List<int> _counts = new();
        private List<EnemyConfigs> _enemyConfigs = new();
        private Dictionary<EnemyConfigs, List<GameObject>> _skinDictionary = new();
        private LoadAsyncLevelState _loadLevelState;

        private int _configsCounts;

        public EnemyFactory(AssertBuilder builder, EnemyConfigs[] configs)
        {
            _enemySkinsAssertAddressable = builder.BuildAssertServiceByAddressable<AssetReferenceGameObject>();
            for (int i = 0; i < configs.Length; i++)
            {
                _enemyConfigs.Add(configs[i]);
                for (int j = 0; j < configs[i].SkinsReference.Count; j++)
                {
                    _scinsNumber.Add((i,j));
                }

                List<GameObject> skins = new();
                _allEnemiesSkins.Add(skins);
                _counts.Add(configs[i].SkinsReference.Count);
                _skinDictionary.Add(configs[i], skins);
            }
        }

        public void Subscribe(object subscriber)
        {
            if (subscriber is not LoadAsyncLevelState levelState)
            {
                throw new Exception($"Not suitable object for Subscriber in factory");
            }

            _loadLevelState = levelState;
            _endAsyncSkinLoad += levelState.EndAsyncLoad;
            LoadSkins();
        }

        public void Unsubscribe()
        {
            _endAsyncSkinLoad -= _loadLevelState.EndAsyncLoad;
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

        private async void LoadSkins()
        {
            List<Task<Tuple<GameObject, bool>>> tasks = new();
            for (int i = 0; i < _scinsNumber.Count; i++)
            {
                tasks.Add(CreateAsynSkin(_scinsNumber[i].Item1, _scinsNumber[i].Item2));
            }

            int index = 0;
            foreach (var task in await Task.WhenAll(tasks))
            {
                if (task.Item2)
                {
                    _fullSkins.Add((task.Item1, _scinsNumber[index].Item2, _scinsNumber[index].Item1));
                    index++;
                }
            }
            for (int i = 0; i < _fullSkins.Count; i++)
            {
                _allEnemiesSkins[_fullSkins[i].Item3].Add(_fullSkins[i].Item1);
            }

            _endAsyncSkinLoad?.Invoke();
        }

        private async Task<Tuple<GameObject, bool>> CreateAsynSkin(int enemyIndex, int skinIndex)
        {
            GameObject gameObject = await _enemySkinsAssertAddressable.Assert(_enemyConfigs[enemyIndex].SkinsReference[skinIndex], ENEMY_DEFAULT_SPAWN);
            return Tuple.Create(gameObject, true);
        }


        private GameObject ChoseSkin(EnemyConfigs configs, Vector3 position, Transform parent)
        {
            List<GameObject> skins = _skinDictionary[configs];
            GameObject objSkin;
            switch (configs.Skins.Count)
            {
                case 0:
                    throw new Exception("Enemy Count is null");
                case 1:
                    objSkin = UnityEngine.Object.Instantiate(skins[0], position, Quaternion.identity, parent);
                    return objSkin;
                case > 1:
                    objSkin = UnityEngine.Object.Instantiate(skins[Random.Range(0, configs.Skins.Count)], position, Quaternion.identity, parent);
                    return objSkin;
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

            EnemyAnimator enemyAnimator = visual.GetComponent<EnemyAnimator>();
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