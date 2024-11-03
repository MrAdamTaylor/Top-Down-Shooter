namespace Enemies.EnemyStateMachine
{
    public class BaseState
    {
        public string Name;
        protected NPCStateMachine NpcStateMachineMachine;

        public BaseState(string name, NPCStateMachine npcStateMachineMachine)
        {
            Name = name;
            NpcStateMachineMachine = npcStateMachineMachine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class DeathState : BaseState
    {
        public DeathState(NPCStateMachine npcStateMachineMachine) : base("DeathState", npcStateMachineMachine)
        {
        
        }
    }
}