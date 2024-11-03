using Infrastructure.ServiceLocator;
using Player;

namespace Enemies.EnemyStateMachine
{
    public class ActionMoveState : BaseState
    {
        private PlayerDeath _playerDeath;

        protected EnemyStateMachine EnemyStateMachine;
    
        public ActionMoveState(string name, NPCStateMachine npcStateMachine) : base(name, npcStateMachine)
        {
            _playerDeath = (PlayerDeath)ServiceLocator.Instance.GetData(typeof(PlayerDeath));
            EnemyStateMachine = (EnemyStateMachine)NpcStateMachineMachine;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if(_playerDeath.IsDie)
                NpcStateMachineMachine.ChangeState(EnemyStateMachine.IdleState);
            
        }
    }
}