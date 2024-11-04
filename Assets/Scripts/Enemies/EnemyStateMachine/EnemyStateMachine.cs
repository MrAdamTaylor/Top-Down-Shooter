using System;
using Logic;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemyStateMachine : NPCStateMachine
    {
        public EnemyAnimator Animator;

        public BaseState IdleState;
        public BaseState MoveState;
        public BaseState AttackState;
        public BaseState DecideState;
        public BaseState DeathState;

        private bool _isConstructed;

        public void Construct(EnemyAnimator enemyAnimator, IEnemyMoveSystem enemyMoveSystem, 
            EnemyRotateSystem enemyRotateSystem, IEnemyAttack enemyAttack, EnemyHealth enemyHealth, 
            GameObject physic) 
        {
            Animator = enemyAnimator;
            IdleState = new IdleState(this, enemyAnimator, physic, enemyHealth);
            MoveState = new FollowPlayerState(this, enemyMoveSystem, enemyHealth, enemyRotateSystem);
            DecideState = new DecideState(this, enemyAttack, enemyMoveSystem, enemyHealth, enemyRotateSystem);
            AttackState = new AttackState(this, enemyAttack, enemyHealth, enemyRotateSystem);
            DeathState = new DeathState(this, enemyAnimator, physic);
            _currentState = GetDefaultState();
            if(_currentState != null)
                _currentState.Enter();
            _isConstructed = true;
        }

        private void OnEnable()
        {
            if(_isConstructed)
                ChangeState(IdleState);
        }

        public void GoalIsDefeated()
        {
            ChangeState(IdleState);
        }

        protected override BaseState GetDefaultState()
        {
            return IdleState;
        }
    }
}