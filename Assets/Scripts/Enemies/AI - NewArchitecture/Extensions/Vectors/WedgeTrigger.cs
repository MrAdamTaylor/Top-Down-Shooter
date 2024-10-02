using UnityEditor;
using UnityEngine;

public class WedgeTrigger : MonoBehaviour
{
    public Transform Target;
    public float Radius = 1;
    public float Height = 1;

    [Range(0, 1)] 
    public float angThresh = 0.5f;
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Handles.color = Contains(Target.position) ? Color.red : Color.white;
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        Vector3 top = new Vector3(0, this.Height, 0);
        
        Handles.DrawWireDisc(default, Vector3.up, Radius);
        Handles.DrawWireDisc(top, Vector3.up, Radius);
        
        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vRight = new Vector3(-x, 0, p) * Radius;
        Vector3 vLeft = new Vector3(x, 0, p) * Radius;
        
        Gizmos.DrawRay(default, vLeft);
        Gizmos.DrawRay(default, vRight);
        
        Gizmos.DrawRay(top, vLeft);
        Gizmos.DrawRay(top, vRight);
        
        Gizmos.DrawLine(default, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
    }
     

    public bool Contains(Vector3 position)
    {
        Vector3 dirToTargetWorld = (position - transform.position);
        Vector3 vecToTarget = transform.InverseTransformVector(dirToTargetWorld);
        
        if (vecToTarget.y < 0 || vecToTarget.y > Height)
            return false;

        Vector3 flatDirToTarget = vecToTarget;
        flatDirToTarget.y = 0;
        float flatDistance = flatDirToTarget.magnitude;
        flatDirToTarget /= flatDistance;
        if (flatDirToTarget.z < angThresh)
            return false;

        if (flatDistance > Radius)
            return false;
       
        return true;
    }
}