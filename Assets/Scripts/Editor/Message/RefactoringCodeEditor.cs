using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RefactoringMessage))]
public class RefactoringCodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Why this not trash! (Click Me)"))
        {
            (serializedObject.targetObject as RefactoringMessage).DoExplain();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
