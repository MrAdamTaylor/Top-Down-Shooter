using System;
using Enemies.EnemyStateMachine.Components;

namespace Enemies.EnemyStateMachine.EnemyStates
{
    public class AttackState : ActionMoveState, IDisposable
    {

        private IEnemyAttack _enemyAttack;
        
        public AttackState(EnemyStateMachine enemyStateMachine, IEnemyAttack enemyAttack, EnemyHealth enemyHealth, 
            EnemyRotateSystem enemyRotateSystem) 
            : base("AttackState", enemyStateMachine, enemyHealth, enemyRotateSystem)
        {
            _enemyAttack = enemyAttack;
            _enemyAttack.ActionAttackEnd += AttackEnd;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (_enemyAttack.IsCanAttack)
                _enemyAttack.Attack();
            else
            {
                if (!_enemyAttack.CooldownIsUp())
                {
                    NpcStateMachine.ChangeState(EnemyStateMachine.DecideState);
                }
            }
        }

        private void AttackEnd()
        {
            NpcStateMachine.ChangeState(EnemyStateMachine.DecideState);
        }
        

        public void Dispose()
        {
            _enemyAttack.ActionAttackEnd -= AttackEnd;
        }
    }
}