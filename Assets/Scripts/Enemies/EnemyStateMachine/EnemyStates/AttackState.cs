using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class AttackState : ActionMoveState
    {

        private IEnemyAttack _enemyAttack;
        
        /*public AttackState(EnemyStateMachine enemyStateMachine) : base("AttackState", enemyStateMachine)
        {
        
        }*/
        
        public AttackState(EnemyStateMachine enemyStateMachine, IEnemyAttack enemyAttack, EnemyHealth enemyHealth, 
            EnemyRotateSystem enemyRotateSystem) 
            : base("AttackState", enemyStateMachine, enemyHealth, enemyRotateSystem)
        {
            _enemyAttack = enemyAttack;
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