using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private const float MOVE_PAUSE_COOLDOWN = 1.2f;
        private const float TOLERANCE = 0.02f;
    
        [SerializeField] private float _minimalDistance;
        private EnemyAnimator _enemyAnimator;

        private IEnemyMoveSystem _enemyMoveSystem;
        //private MoveToPlayer _moveToPlayer;
        private EnemyRotateSystem _enemyRotateSystem;
        private EnemyAttack _enemyAttack;
        private CheckAttack _checkAttack;
        private EnemyDeath _enemyDeath;
        private EnemyHealth _enemyHealth;


        private bool _isConstructed;
        private GameObject _physic;
        private bool _isBusy;
        private float _moveCooldown;
        private bool _isMoving;
        private bool _isDeath;
        private bool _isSetCoolDown;
        private bool _isAlive;

        public void Construct(IEnemyMoveSystem moveSystem, EnemyAnimator enemyAnimator, EnemyRotateSystem rotateSystem,
            EnemyAttack enemyAttack,float minimalDistance, EnemyDeath death, GameObject physic, EnemyHealth enemyHealth)
        {
            //_moveToPlayer = moveToPlayer;
            _enemyMoveSystem = moveSystem;
            _enemyAnimator = enemyAnimator;
            _enemyRotateSystem = rotateSystem;
            _minimalDistance = minimalDistance;
            _enemyAttack = enemyAttack;
            _enemyAttack.ReadyForAction += ChangeActionState;
            _enemyDeath = death;
            _enemyDeath.DeathAction += StopAllComponents;
            _physic = physic;
            _isConstructed = true;
            _enemyHealth = enemyHealth;
            _isAlive = true;
        }

        private void OnEnable()
        {
            if (_isDeath)
            {
                _enemyAttack.Reset();
                //_enemyAttack.DisableAttack();
                _enemyHealth.ReloadHealth();
                _enemyAnimator.PlayIdle();
                _physic.SetActive(true);
                _isDeath = false;
                _isBusy = false;
            }

            if (_isConstructed)
            {
                _enemyMoveSystem.Move();
                //_moveToPlayer.Move();
                _enemyRotateSystem.RotateStart();
                _enemyAnimator.Move(1f);
            }
        }

        private void Update()
        {
            UpdateCoolDown();

            if (CanMove())
            {
                _enemyMoveSystem.Move();
                //_moveToPlayer.Move();
                _enemyAnimator.Move(1f);
            }


            if(!SubjectNotReached())
            {
                _enemyMoveSystem.StopMove();
                _enemyAnimator.StopMoving();
                //_moveToPlayer.StopMove();
            }
        }

        private void OnDestroy()
        {
            _isDeath = true;
            _enemyRotateSystem.RotateStop();
            _enemyDeath.DeathAction -= StopAllComponents;
        }

        private void ChangeActionState(bool canMove)
        {
            if (canMove)
            {
                _moveCooldown = MOVE_PAUSE_COOLDOWN;
                _isBusy = false;
            }
            else
            {
                _isBusy = true;
            }
        }

        private bool CheckSubjectOnNull()
        {
            return _enemyMoveSystem.IsTarget() && _isAlive;
            //return _moveToPlayer.IsTarget();
        }


        private bool SubjectNotReached()
        {
            //float distance = Vector3.Distance(_moveToPlayer.AgentPos(), _moveToPlayer.GoalPos());
            float distance = Vector3.Distance(_enemyMoveSystem.AgentPos(), _enemyMoveSystem.GoalPos());
            if (distance - _minimalDistance >= TOLERANCE)
                return true;
            else
                return false;

        }

        private bool CanMove() =>
            !_isBusy && CooldownIsUp() && SubjectNotReached() && CheckSubjectOnNull();

        private void UpdateCoolDown()
        {
            if (!CooldownIsUp())
                _moveCooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp()
        {
            _isSetCoolDown = false;
            return _moveCooldown <= 0;
        }


        private void StopAllComponents()
        {
            _physic.SetActive(false);
            _enemyMoveSystem.StopMove();
            //_moveToPlayer.StopMove();
            _enemyRotateSystem.RotateStop();
            _isBusy = true;
            _isDeath = true;
        }

        public void GoalIsDefeated()
        {
            _isAlive = false;
            _enemyAnimator.PlayIdle();
            _enemyAttack.ActualAttacking(false);
        }
    }

    public interface IEnemyMoveSystem
    {
        public void Construct(Transform followedTransform, float speed);
        
        public Vector3 AgentPos();

        public Vector3 GoalPos();

        public void Move();

        public void StopMove();

        public bool IsTarget();
    }
}
