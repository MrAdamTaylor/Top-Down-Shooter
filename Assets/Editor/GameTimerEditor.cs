using Logic.Timer;
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
            
            if (GUILayout.Button("Stop Timer"))
            {
                helper.StopTimer();
            }
            
            if (GUILayout.Button("Reload Timer"))
            {
                helper.ReloadTimer(0f);
            }
        }
    }
}