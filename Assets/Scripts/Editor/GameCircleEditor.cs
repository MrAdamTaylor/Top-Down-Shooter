using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameCyrcleManager))]
public class GameCircleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("OnStart"))
        {
            (serializedObject.targetObject as GameCyrcleManager).OnStart();
        }
        
        if (GUILayout.Button("Finish"))
        {
            (serializedObject.targetObject as GameCyrcleManager).OnStart();
        }
        
        if (GUILayout.Button("Pause"))
        {
            (serializedObject.targetObject as GameCyrcleManager).OnStart();
        }
        
        if (GUILayout.Button("Resume "))
        {
            (serializedObject.targetObject as GameCyrcleManager).OnStart();
        }
    }
}
