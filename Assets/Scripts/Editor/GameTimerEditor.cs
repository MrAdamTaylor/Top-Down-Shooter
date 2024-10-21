using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameTimer))]
    public class GameTimerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var helper = (GameTimer)target;

            if (GUILayout.Button("Start Timer"))
            {
                helper.StartTimer();
            }
            
            if (GUILayout.Button("Pause/Resume Timer"))
            {
                helper.PauseResume();
            }
            
            if (GUILayout.Button("Stop"))
            {
                helper.Stop();
            }
        }
    }
}