using UnityEditor;
using UnityEngine;

namespace Editor.Message
{
    [CustomEditor(typeof(RefactoringMessage))]
    public class RefactoringCodeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDefaultInspector();

            if (GUILayout.Button("Why this not trash! (Click Me)"))
            {
                (serializedObject.targetObject as RefactoringMessage).DoExplain();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
