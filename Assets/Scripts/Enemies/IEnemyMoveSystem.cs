using UnityEngine;

namespace Enemies
{
    public interface IEnemyMoveSystem
    {
        public void Construct(Transform followedTransform, float speed);
        
        public Vector3 AgentPos();

        public Vector3 GoalPos();

        public void Move();

        public void StopMove();

        public bool IsTarget();

        public bool IsReached();
    }
}