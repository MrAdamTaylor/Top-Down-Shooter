using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BafSpawnPositions : MonoBehaviour
{
    [SerializeField] private float _minimalRadiusDiaposone = 4;
    [SerializeField] private float _maximumRadiusDiaposone = 7;

    private Vector3 _center;
    private Vector3 _currentPosition;
    
    private void OnDrawGizmos()
    {
        _center = transform.position.ExcludeY();
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(_center, Vector3.up, _minimalRadiusDiaposone);
        Handles.DrawWireDisc(_center, Vector3.up, _maximumRadiusDiaposone);
    }

}
