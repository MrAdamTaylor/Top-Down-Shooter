using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponTriger : MonoBehaviour
{
    [SerializeField] private Transform _touching;
    [SerializeField] private float _radius;
    private bool _isInside;


    private void OnDrawGizmos()
    {
        Handles.color = _isInside ? Color.green : Color.red;
        Handles.DrawWireDisc(this.transform.position, Vector3.up, _radius);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = this.transform.position.ExcludeY();
        
        if(_touching == null)
            return;

        Vector3 provoceuterPos = _touching.position.ExcludeY();
        Vector3 delta = center - provoceuterPos;
        
        //_killed.position
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        _isInside = sqrDistance <= _radius * _radius;
        
        if (_isInside)
        {
            this.gameObject.SetActive(false);
        }
    }
}
