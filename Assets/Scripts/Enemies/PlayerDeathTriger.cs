using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDeathTriger : MonoBehaviour
{
    [SerializeField] private float _radius;

    public Transform Killed;

    private bool isInside;

    private Death _death;

    public void OnDrawGizmos()
    {
        Vector3 center = transform.position.ExcludeY();
        
        if(Killed == null)
            return;

        Vector3 provoceuterPos = Killed.position.ExcludeY();
        Vector3 delta = center - provoceuterPos;
        
        //_killed.position
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        isInside = sqrDistance <= _radius * _radius;
        
        Handles.color = isInside ? Color.green : Color.red;
        Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
    }

    public void Update()
    {
        Vector3 center = this.transform.position.ExcludeY();
        
        if(Killed == null)
            return;

        Vector3 provoceuterPos = Killed.position.ExcludeY();
        Vector3 delta = center - provoceuterPos;
        
        //_killed.position
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        isInside = sqrDistance <= _radius * _radius;
        
        if (isInside)
        {
            _death = Killed.gameObject.GetComponent<Death>();
            _death.MakeDeath();
            _death = null;
        }
    }
}
