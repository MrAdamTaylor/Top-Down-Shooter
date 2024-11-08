using Infrastructure.ServiceLocator;
using Player;

namespace Enemies.EnemyStateMachine.EnemyStates
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
            _health.NoHealthAction += ChangeStateToDie;
            _rotateSystem = rotateSystem;
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            EnemyStateMachine = (EnemyStateMachine)NpcStateMachine;
        }

        private void ChangeStateToDie()
        {
            NpcStateMachine.ChangeState(EnemyStateMachine.DeathState);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(_playerDeath.IsDie)
                NpcStateMachine.ChangeState(EnemyStateMachine.IdleState);
        }
        

        public override void Enter()
        {
            base.Enter();
            _rotateSystem.RotateStart();
        }

        public override void Exit()
        {
            _rotateSystem.RotateStop();
            base.Exit();
        }
    }
}