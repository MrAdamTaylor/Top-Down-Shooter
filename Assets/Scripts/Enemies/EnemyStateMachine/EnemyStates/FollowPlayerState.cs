using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class FollowPlayerState : ActionMoveState
    {
        private IEnemyMoveSystem _moveSystem;
        
        /*public FollowPlayerState(EnemyStateMachine enemyStateMachine) : base("Follow Player State",enemyStateMachine)
        {
       
        }*/
        
        public FollowPlayerState(EnemyStateMachine enemyStateMachine, IEnemyMoveSystem moveSystem, EnemyHealth health,
            EnemyRotateSystem rotateSystem) : base("Follow Player State",enemyStateMachine, health,rotateSystem)
        {
            _moveSystem = moveSystem;
        }
    
        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Follow Player State</color>");
        }
    }
}