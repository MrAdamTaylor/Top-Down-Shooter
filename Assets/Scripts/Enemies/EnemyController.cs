using EnterpriceLogic.Utilities;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _minimalDistance;
    private EnemyAnimator _enemyAnimator;
    private MoveToPlayer _moveToPlayer;
    private EnemyRotateSystem _enemyRotateSystem;
    private EnemyAttack _enemyAttack;
    private CheckAttack _checkAttack;
    private EnemyDeath _enemyDeath;

    private GameObject _physic;
    private bool _isMoving;
    private bool _isDeath;

    public void Construct(MoveToPlayer moveToPlayer, EnemyAnimator enemyAnimator, EnemyRotateSystem rotateSystem,
        EnemyAttack enemyAttack,float minimalDistance, EnemyDeath death, GameObject physic)
    {
        _moveToPlayer = moveToPlayer;
        _enemyAnimator = enemyAnimator;
        _enemyRotateSystem = rotateSystem;
        _minimalDistance = minimalDistance;
        _enemyAttack = enemyAttack;
        _enemyAttack.AfterAttackAction += UpdateState;
        _enemyDeath = death;
        _enemyDeath.DeathAction += StopAllComponents;
        _physic = physic;

        if (!moveToPlayer.IsNull())
        {
            _isMoving = true;
            _moveToPlayer.Move();
            _enemyRotateSystem.RotateStart();
            _enemyAnimator.Move(1f);
        }
    }

    void Update()
    {
        if (!_isDeath)
        {
            if (_moveToPlayer.CalculateDistacne() <= _minimalDistance)
            {
                _enemyAnimator.StopMoving();
                _moveToPlayer.StopMove();
            }
        }
    }

    void OnDestroy()
    {
        _enemyAttack.AfterAttackAction -= UpdateState;
        _enemyRotateSystem.RotateStop();
        _enemyDeath.DeathAction -= StopAllComponents;
    }

    private void StopAllComponents()
    {
        _physic.SetActive(false);
        _moveToPlayer.StopMove();
        _enemyRotateSystem.RotateStop();
        _isDeath = true;
    }

    private void UpdateState()
    {
        if (_moveToPlayer.CalculateDistacne() > _minimalDistance)
        {
            _enemyAnimator.Move(1f);
            _moveToPlayer.Move();
        }
    }
}
