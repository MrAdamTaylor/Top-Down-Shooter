using System.Collections;
using EnterpriceLogic.Constants;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
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
        Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        Debug.Log(player + " is player loaded");
        _goal = player.transform;
        _speed = speed * Constants.NPC_SPEED_MULTIPLYER;
        _followedTransform = followedTransform;
    }

    public void Move()
    {
        _needMove = true;
        _moveRoutine = StartCoroutine(MakeStep());
    }

    public void StopMove()
    {
        StopCoroutine(_moveRoutine);
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
        Debug.Log(velocity);
        _followedTransform.position += velocity * Time.deltaTime;
    }

    public float CalculateDistacne()
    {
        float distance =
            Mathf.Sqrt(Mathf.Pow(_goal.transform.position.x - transform.position.x,2) + 
                       Mathf.Pow(_goal.transform.position.y - transform.position.y,2));
        return distance;
    }
}

