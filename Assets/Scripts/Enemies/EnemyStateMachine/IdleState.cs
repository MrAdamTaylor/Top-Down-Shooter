using Infrastructure.ServiceLocator;
using Player;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class IdleState : BaseState
    {
        private PlayerDeath _playerDeath;
        private EnemyStateMachine _enemyStateMachine;
    
        public IdleState(EnemyStateMachine npcStateMachineMachine) : base("IdleState", npcStateMachineMachine)
        {
            _enemyStateMachine = npcStateMachineMachine;
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
                NpcStateMachineMachine.ChangeState(_enemyStateMachine.MoveState);
        }
    }
}