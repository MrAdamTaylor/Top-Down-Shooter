namespace Enemies.EnemyStateMachine.EnemyStates
{
    public class FollowPlayerState : ActionMoveState
    {
        private IEnemyMoveSystem _moveSystem;
        
        public FollowPlayerState(EnemyStateMachine enemyStateMachine, IEnemyMoveSystem moveSystem, EnemyHealth health,
            EnemyRotateSystem rotateSystem) : base("Follow Player State",enemyStateMachine, health,rotateSystem)
        {
            _moveSystem = moveSystem;
        }
    
        public override void Enter()
        {
            base.Enter();
            _moveSystem.Move();
            EnemyStateMachine.Animator.Move(1f);
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(!_moveSystem.IsReached())
                NpcStateMachine.ChangeState(EnemyStateMachine.DecideState);

        }

        public override void Exit()
        {
            base.Exit();
            EnemyStateMachine.Animator.StopMoving();
            _moveSystem.StopMove();
        }
    }
}