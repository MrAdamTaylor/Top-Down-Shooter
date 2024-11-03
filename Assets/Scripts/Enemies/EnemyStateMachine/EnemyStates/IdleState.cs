using Infrastructure.ServiceLocator;
using Player;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class IdleState : BaseState
    {
        private readonly PlayerDeath _playerDeath;
        private readonly EnemyStateMachine _enemyStateMachine;
    
        public IdleState(EnemyStateMachine npcStateMachine) : base("IdleState", npcStateMachine)
        {
            _enemyStateMachine = npcStateMachine;
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Idle State</color>");
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(!_playerDeath.IsDie)
                NpcStateMachine.ChangeState(_enemyStateMachine.DecideState);
        }
    }
}