using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class FollowPlayerState : ActionMoveState
    {
        public FollowPlayerState(EnemyStateMachine enemyStateMachine) : base("Follow Player State",enemyStateMachine)
        {
       
        }
    
        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Follow Player State</color>");
        }
    }
}