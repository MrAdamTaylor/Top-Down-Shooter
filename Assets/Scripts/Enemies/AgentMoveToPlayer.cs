using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class AgentMoveToPlayer : MonoBehaviour, IEnemyMoveSystem
    {
        public const float MINIMAL_DISTANCE = 1.5f;
        
        private NavMeshAgent _navMeshAgent;
        private Transform _goal;
        private float _speed;
        private Transform _followedTransform;

        private bool _isMoving;

        public void Construct(Transform followedTransform, float speed)
        {
            _navMeshAgent = transform.GetComponent<NavMeshAgent>();
            Player.Player player = (Player.Player)ServiceLocator.Instance.GetData(typeof(Player.Player));
            _goal = player.transform;
            _speed = speed * Constants.NPC_SPEED_MULTIPLYER;
            _followedTransform = followedTransform;
            _navMeshAgent.speed = _speed;
        }

        private void Update()
        {
            if(_isMoving)
                _navMeshAgent.SetDestination(_goal.transform.position);
        }
    

        public Vector3 GoalPos()
        {
            return _goal.transform.position;
        }

        public Vector3 AgentPos()
        {
            return _followedTransform.position;
        }

        public void Move()
        {
            _isMoving = true;
        }

        public void StopMove()
        {
            _navMeshAgent.SetDestination(_followedTransform.transform.position);
            _isMoving = false;
        }

        public bool IsTarget() => _goal != null;

        public bool IsReached()
        {
            float distance = Vector3.Distance(_followedTransform.transform.position, _goal.transform.position);
            if (distance - MINIMAL_DISTANCE >= Mathf.Epsilon)
                return true;
            else
                return false;
        }
    }
}