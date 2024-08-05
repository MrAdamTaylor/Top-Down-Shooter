using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BafSpawnPositions : MonoBehaviour
{
    public float MinimalRadiusDiaposone = 4;
    public float MaximumRadiusDiaposone = 7;

    public Transform Provocateur;
    public float PorogDistance = 2f;
    private Vector3 _center;
    private Vector3 _currentPosition;

    public bool IsInside;
    
    private void OnDrawGizmos()
    {
        _center = transform.position.ExcludeY();
        //Handles.color = IsInside ? Color.blue : Color.red;
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(_center, Vector3.up, MinimalRadiusDiaposone);
        Handles.DrawWireDisc(_center, Vector3.up, MaximumRadiusDiaposone);
    }
    
    void Update()
    {
        /*if(Provocateur == null)
            return;
        _currentPosition = transform.position;
        _currentPosition = _currentPosition.ExcludeY();
        _center = Provocateur.position;
        _center = _center.ExcludeY();
        Vector3 delta = _currentPosition - _center;
        
        float sqrDistance = delta.x * delta.x + delta.z * delta.z;
        
        bool moreMinimal = sqrDistance >= MinimalRadiusDiaposone * MinimalRadiusDiaposone;
        bool lessMaximal = sqrDistance <= MaximumRadiusDiaposone * MaximumRadiusDiaposone;

        IsInside = moreMinimal && lessMaximal;*/
    }
}
