using UnityEngine;

namespace Enemies.EnemyStateMachine.EnemyStates
{
    public class DeathState : BaseState
    {
        private EnemyAnimator _animator;
        private GameObject _physic;
        
        public DeathState(NPCStateMachine npcStateMachine, EnemyAnimator animator, GameObject physic) : base("DeathState", npcStateMachine)
        {
            _physic = physic;
            _animator = animator;
        }
        
        public override void Enter()
        {
            base.Enter();
            _physic.SetActive(false);
        }
    }
}