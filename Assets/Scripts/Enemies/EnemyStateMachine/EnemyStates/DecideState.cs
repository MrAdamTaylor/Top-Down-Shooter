using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class DecideState : ActionMoveState
    {
        
        /*public DecideState(EnemyStateMachine enemyStateMachine) : base("DecideState", enemyStateMachine)
        {
        
        }*/

        private IEnemyAttack _enemyAttack;
        private IEnemyMoveSystem _enemyMoveSystem;
        
        public DecideState(EnemyStateMachine enemyStateMachine, IEnemyAttack enemyAttack, IEnemyMoveSystem moveSystem, 
            EnemyHealth health, EnemyRotateSystem rotateSystem) 
            : base("DecideState", enemyStateMachine, health, rotateSystem)
        {
            _enemyAttack = enemyAttack;
            _enemyMoveSystem = moveSystem;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Decide State</color>");
             /*if(EnemyStateMachine.EnemyAttack.IsCanAttack)
                EnemyStateMachine.ChangeState();*/
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(_enemyAttack.IsCanAttack)
                NpcStateMachine.ChangeState(EnemyStateMachine.AttackState);
            
            //if(_enemyMoveSystem.)
            
        }
    }
}