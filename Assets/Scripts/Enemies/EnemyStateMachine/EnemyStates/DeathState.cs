using UnityEngine;

namespace Enemies.EnemyStateMachine
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
            Debug.Log($"<color=red>Death State</color>");
            _physic.SetActive(false);
        }
    }
}