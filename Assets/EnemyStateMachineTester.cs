using Enemies;
using Enemies.EnemyStateMachine;
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
        
        EnemyStateMachine stateMachine = enemyObject.AddComponent<EnemyStateMachine>();

        stateMachine.Construct(enemyAnimator);
    }
}
