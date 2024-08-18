using System.Collections;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform Goal;
    
    [SerializeField] private Transform _followedTransform;

    private float _speed;
    private bool _needMove;
    private Vector3 _direction;
    private Vector3 _tempPosition;
    private float _length;
    
    public void OnStart(float speed)
    {
        _speed = speed;
        Construct();
    }
    
    public void Construct()
    {
        _tempPosition = _followedTransform.position;
        _needMove = true;
        _direction = Goal.transform.position - _tempPosition;
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
        _length = Vector3.Distance(Goal.transform.position, _followedTransform.position);
        _tempPosition = _followedTransform.position;
        _direction = Goal.transform.position - _tempPosition;
        Vector3 velocity = _direction.normalized * _speed;
        _followedTransform.position += velocity * Time.deltaTime;
    }

}
