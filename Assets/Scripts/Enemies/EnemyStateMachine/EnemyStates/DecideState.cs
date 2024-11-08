using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class DecideState : ActionMoveState
    {

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
        }
        
        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(!_enemyMoveSystem.IsReached())
                NpcStateMachine.ChangeState(EnemyStateMachine.AttackState);
            else
            {
                NpcStateMachine.ChangeState(EnemyStateMachine.MoveState);
            }
        }
    }
}