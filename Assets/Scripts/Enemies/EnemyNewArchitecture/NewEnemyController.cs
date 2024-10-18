using EnterpriceLogic.Utilities;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    [SerializeField] private float _minimalDistance;
    private EnemyAnimator _enemyAnimator;
    private MoveToPlayer _moveToPlayer;
    private EnemyRotateSystem _enemyRotateSystem;
    private EnemyAttack _enemyAttack;
    private CheckAttack _checkAttack;
    private bool _isMoving;

    public void Construct(MoveToPlayer moveToPlayer, EnemyAnimator enemyAnimator, EnemyRotateSystem rotateSystem,EnemyAttack enemyAttack,float minimalDistance)
    {
        _moveToPlayer = moveToPlayer;
        _enemyAnimator = enemyAnimator;
        _enemyRotateSystem = rotateSystem;
        _minimalDistance = minimalDistance;
        _enemyAttack = enemyAttack;
        _enemyAttack.AfterAttackAction += UpdateState;

        if (!moveToPlayer.IsNull())
        {
            _isMoving = true;
            _moveToPlayer.Move();
            _enemyRotateSystem.RotateStart();
            _enemyAnimator.Move(1f);
        }
    }

    private void UpdateState()
    {
        if (_moveToPlayer.CalculateDistacne() > _minimalDistance)
        {
            _enemyAnimator.Move(1f);
            _moveToPlayer.Move();
        }
    }

    private void Update()
    {
        if (_moveToPlayer.CalculateDistacne() <= _minimalDistance)
        {
            _enemyAnimator.StopMoving();
            _moveToPlayer.StopMove();
        }
    }

    private void OnDestroy()
    {
        _enemyAttack.AfterAttackAction -= UpdateState;
        _enemyRotateSystem.RotateStop();
    }
}
