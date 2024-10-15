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
        Transform visual = enemy.transform.Find("[VISUAL]");
        EnemyAnimator enemyAnimator = visual.AddComponent<EnemyAnimator>();
        enemyAnimator.Construct();
        MoveToPlayer moveToPlayer = enemy.AddComponent<MoveToPlayer>();
        moveToPlayer.Construct(enemy.transform, configs.Speed);
        NewEnemyController enemyController = enemy.AddComponent<NewEnemyController>();
        enemyController.Construct(moveToPlayer, enemyAnimator);
    }

    private void CreateKamikaze(GameObject enemy, EnemyKamikazeConfigs configs)
    {
        Debug.Log($"<color=green> {configs.Name} is Created Registered</color>");
    }
}