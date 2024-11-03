namespace Enemies.EnemyStateMachine
{
    public class EnemyStateMachine : NPCStateMachine
    {
        public EnemyAnimator Animator;
        public IEnemyMoveSystem MoveSystem;
        public EnemyRotateSystem RotateSystem;

        public BaseState IdleState;
        public BaseState MoveState;
        public BaseState AttackState;
        public BaseState DecideState;

        public void Construct(EnemyAnimator enemyAnimator)
        {
            Animator = enemyAnimator;
            IdleState = new IdleState(this);
            MoveState = new FollowPlayerState(this);
            DecideState = new DecideState(this);
            AttackState = new AttackState(this);
        }

        protected override BaseState GetDefaultState()
        {
            return IdleState;
        }
    

    }

    public class DecideState : BaseState
    {
    
    
        public DecideState(EnemyStateMachine enemyStateMachine) : base("DecideState", enemyStateMachine)
        {
        
        }
    }
}