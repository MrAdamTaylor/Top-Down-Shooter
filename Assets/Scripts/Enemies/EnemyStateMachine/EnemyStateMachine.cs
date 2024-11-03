namespace Enemies.EnemyStateMachine
{
    public class EnemyStateMachine : NPCStateMachine
    {
        public EnemyAnimator Animator;
        public IEnemyMoveSystem MoveSystem;
        public EnemyRotateSystem RotateSystem;
        public IEnemyAttack EnemyAttack;

        public BaseState IdleState;
        public BaseState MoveState;
        public BaseState AttackState;
        public BaseState DecideState;
        public BaseState DeathState;

        public void Construct(EnemyAnimator enemyAnimator, IEnemyMoveSystem enemyMoveSystem, 
            EnemyRotateSystem enemyRotateSystem, IEnemyAttack enemyAttack, EnemyHealth enemyHealth)
        {
            
            Animator = enemyAnimator;
            IdleState = new IdleState(this);
            MoveState = new FollowPlayerState(this, enemyMoveSystem, enemyHealth, enemyRotateSystem);
            DecideState = new DecideState(this, enemyAttack, enemyMoveSystem, enemyHealth, enemyRotateSystem);
            AttackState = new AttackState(this, enemyAttack, enemyHealth, enemyRotateSystem);
            DeathState = new DeathState(this);
            _currentState = GetDefaultState();
            if(_currentState != null)
                _currentState.Enter();
        }

        protected override BaseState GetDefaultState()
        {
            return IdleState;
        }
        
    }
}