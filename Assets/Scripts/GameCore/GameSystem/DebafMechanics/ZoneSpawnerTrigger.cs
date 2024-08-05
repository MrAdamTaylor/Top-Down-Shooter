using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class ZoneSpawnerTrigger : MonoBehaviour
{
    [SerializeField] private float _minRadius = 3;
    public float MaxRadius = 9;
    [SerializeField] private GameObject _provocateur;
    private Vector3 _currentPosition;
    private Vector3 _center;

    public bool IsInside { get; set; }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position.ExcludeY();
        
        if(_provocateur == null)
            return;

        Vector3 provoceuterPos = _provocateur.transform.position.ExcludeY();
        Vector3 delta = center - provoceuterPos;
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        bool isInside2 = _minRadius * _minRadius <= sqrDistance;
        bool isInside1 = sqrDistance <= MaxRadius * MaxRadius;

        bool isInside = isInside1 && isInside2;
        
        //Handles.color = IsInside ? Color.blue : Color.red;
        Handles.color = isInside ? Color.magenta : Color.blue;
        Handles.DrawWireDisc(this.transform.position, Vector3.up,_minRadius);
        Handles.DrawWireDisc(this.transform.position, Vector3.up, MaxRadius);
    }

    private void Update()
    {
        GetBool();
    }

    private void GetBool()
    {
        /*if(_provocateur == null)
            return;

        _currentPosition = transform.position;
        _currentPosition = _currentPosition.ExcludeY();
        _center = _provocateur.transform.position;
        _center = _center.ExcludeY();
        Vector3 delta = _currentPosition - _center;

        //float sqrDistance = delta.x * delta.x + delta.y * delta.y;
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        
        bool moreMinimal = sqrDistance >= _minRadius * _minRadius;
        bool lessMaximal = sqrDistance <= _maxRadius * _maxRadius;

        IsInside = moreMinimal && lessMaximal;
        Debug.Log(false);*/
        
    }

    public bool CheckPosition(Vector3 vec)
    {
        Vector3 center = transform.position.ExcludeY();
        Vector3 delta = center - vec;
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        bool isInside2 = _minRadius * _minRadius <= sqrDistance;
        bool isInside1 = sqrDistance <= MaxRadius * MaxRadius;
        bool isInside = isInside1 && isInside2;
        return isInside;
    }
}
