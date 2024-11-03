using Infrastructure.ServiceLocator;
using Player;

namespace Enemies.EnemyStateMachine
{
    public class ActionMoveState : BaseState
    {
        private PlayerDeath _playerDeath;

        protected EnemyStateMachine EnemyStateMachine;

        protected EnemyHealth _health;
        protected EnemyRotateSystem _rotateSystem;

        protected ActionMoveState(string name, NPCStateMachine npcStateMachine) : base(name, npcStateMachine)
        {
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            EnemyStateMachine = (EnemyStateMachine)NpcStateMachine;
        }

        protected ActionMoveState(string name, NPCStateMachine npcStateMachine, EnemyHealth health, EnemyRotateSystem rotateSystem) : base(name, npcStateMachine)
        {
            _health = health;
            _rotateSystem = rotateSystem;
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            EnemyStateMachine = (EnemyStateMachine)NpcStateMachine;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(_playerDeath.IsDie)
                NpcStateMachine.ChangeState(EnemyStateMachine.IdleState);
        }
    }
}