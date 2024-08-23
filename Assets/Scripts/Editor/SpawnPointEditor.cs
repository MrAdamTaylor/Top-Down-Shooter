using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnPoint spawnPoint, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPoint.transform.position, 0.5f);
    }
}
