using Enemies;
using EnterpriceLogic.Constants;
using Mechanics;
using Mechanics.Spawners.NewArchitecture;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private RotateSystem _rotateSystem;
    [SerializeField] private MoveTo _moveSystem;
    [SerializeField] private EnemyDeath _death;
    
    private EnemySpawner _spawner;

    //TODO - Dependency (Level - SelfConstruct) (class - EnemySpawner)
    public void Construct(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    private void Start()
    {
        if (_rotateSystem != null)
        {
            _rotateSystem.OnStart();
        }

        if (_moveSystem != null)
        {
            _moveSystem.OnStart(_enemy.ReturnSpeed()*Constants.NPC_SPEED_MULTIPLYER);
            _moveSystem.Move();
        }
    }

    private void OnEnable()
    {
        _moveSystem.Move();
        _rotateSystem.OnStart();
    }

    private void OnDisable()
    {
        _moveSystem.StopMove();
        _rotateSystem.Stop();
    }

    private void OnDestroy()
    {
        _death.OnDeath -= ReturnPool;
    }

    public void SubscribeDeath()
    {
        _death.OnDeath += ReturnPool;
    }

    private void ReturnPool()
    {
        _spawner.ReturnPool(this.gameObject);
    }
}
