using System;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService.ExtendetAssertService;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

internal class EnemyFactory : IEnemyFactory 
{
    private IAssertByObj<GameObject> _enemySkinsAssert;
    
    public EnemyFactory(AssertBuilder builder)
    {
        _enemySkinsAssert = builder.BuildAssertServiceByObj<GameObject>();
    }

    public void Create(EnemyConfigs configs, GameObject parent)
    {
        GameObject enemy;
        Vector3 pos = CreateTestPosition();
        if (configs.Skins.Count == 0)
        {
            return;
        }
        else if(configs.Skins.Count == 1)
        {
            enemy = _enemySkinsAssert.Assert(configs.Skins[0], pos);
        }
        else
        {
             enemy = _enemySkinsAssert.Assert(configs.Skins[Random.Range(0, configs.Skins.Count)], pos);
        }
        enemy.transform.parent = parent.transform;
        switch (configs)
        {
            case EnemyKamikazeConfigs:
                CreateKamikaze(enemy,(EnemyKamikazeConfigs)configs);
                break;
            case EnemyWalkingConfigs:
                CreateWalking(enemy,(EnemyWalkingConfigs)configs);
                break;
            case EnemyTurretConfigs:
                CreateTurret(enemy, (EnemyTurretConfigs)configs);
                break;
            case null:
                throw new ArgumentException("Enemy configs in Factory is null");
            default:
                throw new Exception("Unknown Enemy configs for Factory");
        }
        
        Debug.Log("Created Enemy with Random Skin");
    }

    private Vector3 CreateTestPosition()
    {
        Vector3 pos = new Vector3(
            Random.Range(Constants.DEFAULT_VECTOR_FOR_TEST2.x-Constants.SPAWN_INTERVAL,
                Constants.DEFAULT_VECTOR_FOR_TEST2.x+Constants.SPAWN_INTERVAL), 
            Constants.DEFAULT_VECTOR_FOR_TEST2.y, 
            Random.Range(Constants.DEFAULT_VECTOR_FOR_TEST2.z-Constants.SPAWN_INTERVAL,
                Constants.DEFAULT_VECTOR_FOR_TEST2.z+Constants.SPAWN_INTERVAL));
        return pos;
    }

    private void CreateTurret(GameObject enemy, EnemyTurretConfigs configs)
    {
        Debug.Log($"<color=green>  {configs.Name} is Created Registered</color>");
    }

    private void CreateWalking(GameObject enemy, EnemyWalkingConfigs configs)
    {
        Debug.Log($"<color=green>  {configs.Name} is Created Registered</color>");
        Transform visual = enemy.transform.Find(Constants.PREFAB_MESH_COMPONENT_NAME);
        EnemyAnimator enemyAnimator = visual.AddComponent<EnemyAnimator>();
        enemyAnimator.Construct();
        Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        MoveToPlayer moveToPlayer = enemy.AddComponent<MoveToPlayer>();
        moveToPlayer.Construct(enemy.transform, configs.Speed);
        EnemyRotateSystem enemyRotateSystem = enemy.AddComponent<EnemyRotateSystem>();
        enemyRotateSystem.Construct(enemy.transform, player.transform, Constants.ROTATE_SPEED);
        NewEnemyController enemyController = enemy.AddComponent<NewEnemyController>();
        enemyController.Construct(moveToPlayer, enemyAnimator, enemyRotateSystem);
        ReactionTrigger reactionTrigger = enemy.AddComponent<ReactionTrigger>();
        reactionTrigger.Construct(configs.RadiusDetection, player.transform);
        EnemyAttack enemyAttack = visual.AddComponent<EnemyAttack>();
        enemyAttack.Construct(enemyAnimator);
        CheckAttack attackChecker = enemy.AddComponent<CheckAttack>();
        attackChecker.Construct(enemyAttack, reactionTrigger);
    }

    private void CreateKamikaze(GameObject enemy, EnemyKamikazeConfigs configs)
    {
        Debug.Log($"<color=green> {configs.Name} is Created Registered</color>");
    }
}