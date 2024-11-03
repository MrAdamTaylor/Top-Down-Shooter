using Enemies;
using Enemies.EnemyStateMachine;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using Logic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachineTester : MonoBehaviour
{
    public GameObject EnemyObject;

    private Vector3 _position;
    public void Start()
    {
        GameObject gameObject = GameObject.FindWithTag("EnemyTestPosition");
        _position = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateStateMachineEnemy();
        }
    }

    private void CreateStateMachineEnemy()
    {
        GameObject enemyObject = Instantiate(EnemyObject, _position, Quaternion.identity);
        Transform visual = enemyObject.transform.Find(ConstantsSceneObjects.PREFAB_MESH_COMPONENT_NAME);
        Transform physic = enemyObject.transform.Find(ConstantsSceneObjects.PREFAB_PHYSIC_COMPONENT_NAME);
        PlayLoopComponentProvider provider = physic.GetComponent<PlayLoopComponentProvider>();
        Player.Player player = (Player.Player)ServiceLocator.Instance.GetData(typeof(Player.Player));
        
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
        
        EnemyStateMachine stateMachine = enemyObject.AddComponent<EnemyStateMachine>();
        stateMachine.Construct(enemyAnimator, moveToPlayer,enemyRotateSystem, enemyAttack, enemyHealth);
    }
}
