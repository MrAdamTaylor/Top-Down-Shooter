using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private MoveToPlayer _moveToPlayer;
    private EnemyRotateSystem _enemyRotateSystem;

    private bool _isMoving;

    public void Construct(MoveToPlayer moveToPlayer, EnemyAnimator enemyAnimator, EnemyRotateSystem rotateSystem)
    {
        _moveToPlayer = moveToPlayer;
        _enemyAnimator = enemyAnimator;
        _enemyRotateSystem = rotateSystem;

        if (!moveToPlayer.IsNull())
        {
            Debug.Log($"Launch Coroutine for Moving: ");
            _isMoving = true;
            _moveToPlayer.Move();
            _enemyRotateSystem.RotateStart();
            _enemyAnimator.Move(1f);
        }
    }

    private void Update()
    {
        if (_moveToPlayer.CalculateDistacne() < Constants.MOVEMENT_THRESHOLD)
        {
            _isMoving = false;
            _moveToPlayer.StopMove();
            _enemyAnimator.StopMoving();
        }
        else
        {
            if (!_isMoving)
            {
                _isMoving = true;
                _moveToPlayer.Move();
                _enemyAnimator.Move(1f);
            }
        }
    }

    private void OnDestroy()
    {
        _enemyRotateSystem.RotateStop();
    }
}
