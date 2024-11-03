namespace Enemies.EnemyStateMachine
{
    public class BaseState
    {
        public string Name;
        protected NPCStateMachine NpcStateMachine;

        public BaseState(string name, NPCStateMachine npcStateMachine)
        {
            Name = name;
            NpcStateMachine = npcStateMachine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }
}