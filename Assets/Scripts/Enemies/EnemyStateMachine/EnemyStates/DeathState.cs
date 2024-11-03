using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class DeathState : BaseState
    {
        public DeathState(NPCStateMachine npcStateMachine) : base("DeathState", npcStateMachine)
        {
        
        }
        
        public override void Enter()
        {
            base.Enter();
            Debug.Log("<color=cyan>Death State</color>");
        }
    }
}