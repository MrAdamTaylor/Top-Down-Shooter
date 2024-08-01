using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] public Transform Goal;
    
    [SerializeField] public Transform FollowedTransform;

    private float _speed;
    public bool NeedMove { get; set; }
    private Vector3 _direction;
    private IEnumerator _makeStepRoutine;
    private Vector3 _tempPosition;
    private float _length;
    
    public void OnStart(float speed)
    {
        _speed = speed;
        Construct();
    }
    
    public void Construct()
    {
        _tempPosition = FollowedTransform.position;
        _makeStepRoutine = MakeStep();
        NeedMove = true;
        _direction = Goal.transform.position - _tempPosition;
    }
    
    public void Move()
    {
        StartCoroutine(_makeStepRoutine);
    }

    public void StopMove()
    {
        StopCoroutine(_makeStepRoutine);
    }
    
    private IEnumerator MakeStep()
    {
        while (NeedMove)
        {
            yield return null;
            yield return null;
            yield return null;
            Step();
        }
    }
    
    private void Step()
    {
        _length = Vector3.Distance(Goal.transform.position, FollowedTransform.position);
        _tempPosition = FollowedTransform.position;
        _direction = Goal.transform.position - _tempPosition;
        Vector3 velocity = _direction.normalized * _speed;
        FollowedTransform.position += velocity * Time.deltaTime;

        #region Корректировка по константе

        _tempPosition = FollowedTransform.position;
        //_tempPosition.y = WorldConstants.NPCDownPointShift;
        FollowedTransform.position = _tempPosition;

        #endregion
    }

}
