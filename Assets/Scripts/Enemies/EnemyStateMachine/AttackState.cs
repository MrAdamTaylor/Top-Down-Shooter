using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class AttackState : ActionMoveState
    {
    
    
        public AttackState(EnemyStateMachine enemyStateMachine) : base("AttackState", enemyStateMachine)
        {
        
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Attack State Active</color>");
        }
    
        public override void UpdateLogic()
        {
        
        }
    }
}