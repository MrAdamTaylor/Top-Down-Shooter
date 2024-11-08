using Infrastructure.ServiceLocator;
using Logic;
using Player;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class IdleState : BaseState
    {
        readonly PlayerDeath _playerDeath;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyAnimator _animator;
        private readonly GameObject _physic;
        private readonly EnemyHealth _health;
    
        public IdleState(EnemyStateMachine npcStateMachine, EnemyAnimator enemyAnimator, 
            GameObject physic, EnemyHealth health) : base("IdleState", npcStateMachine)
        {
            _physic = physic;
            _health = health;
            _animator = enemyAnimator;
            _enemyStateMachine = npcStateMachine;
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log($"<color=green>Idle State</color>");
            _physic.SetActive(true);
            _health.ReloadHealth();
            _animator.PlayIdle();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(!_playerDeath.IsDie)
                NpcStateMachine.ChangeState(_enemyStateMachine.DecideState);
        }
    }
}