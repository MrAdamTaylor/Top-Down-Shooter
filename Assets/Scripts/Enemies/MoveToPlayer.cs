using System.Collections;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using UnityEngine;

namespace Enemies
{
    public class MoveToPlayer : MonoBehaviour, IEnemyMoveSystem
    {
        private Transform _goal;
        private bool _needMove;
        private Coroutine _moveRoutine;
        private Transform _followedTransform;

        private Vector3 _tempPosition;
        private Vector3 _direction;
        private float _speed;
    

        public void Construct(Transform followedTransform, float speed)
        {
            Player.Player player = (Player.Player)ServiceLocator.Instance.GetData(typeof(Player.Player));
            _goal = player.transform;
            _speed = speed * Constants.NPC_SPEED_MULTIPLYER;
            _followedTransform = followedTransform;
            _needMove = true;
        }

        public void Move()
        {
            _moveRoutine ??= StartCoroutine(MakeStep());
        }

        public void StopMove()
        {
            if (_moveRoutine == null) 
                return;
            StopCoroutine(_moveRoutine);
            _moveRoutine = null;
        }

        public Vector3 GoalPos()
        {
            return _goal.transform.position;
        }

        public Vector3 AgentPos()
        {
            return _followedTransform.position;
        }

        public bool IsTarget() => _goal != null;
        public bool IsReached()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator MakeStep()
        {
            while (_needMove)
            {
                yield return null;
                yield return null;
                yield return null;
                Step();
            }
        }

        private void Step()
        {
            _tempPosition = _followedTransform.position;
            _direction = _goal.transform.position - _tempPosition;
            Vector3 velocity = _direction.normalized * _speed;
            _followedTransform.position += velocity * Time.deltaTime;
        }
    }
}