using System.Collections;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Transform _followedTransform;

    private Transform _goal;
    private float _speed;
    private bool _needMove;
    private Vector3 _direction;
    private Vector3 _tempPosition;
    
    public void Construct()
    {
        _tempPosition = _followedTransform.position;
        _needMove = true;
        _direction = _goal.transform.position - _tempPosition;
    }

    public void OnStart(float speed)
    {
        _speed = speed;
        Construct();
    }

    //TODO - Dependency (Level - OnEnable) (class - Player)
    public void OnEnable()
    {
        Player player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        _goal = player.transform;
    }

    public void Move()
    {
        StartCoroutine(MakeStep());
    }

    public void StopMove()
    {
        StopCoroutine(MakeStep());
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
