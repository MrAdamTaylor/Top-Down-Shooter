using UI.MVC.Helper;
using UnityEditor;
using UnityEngine;

namespace Utilities.Editor.UI
{
    [CustomEditor(typeof(UIHelper))]
    public class UIHelperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var helper = (UIHelper)target;

            if (GUILayout.Button("Add Money"))
            {
                helper.AddMoney();
            }
            
            if (GUILayout.Button("Spend Money"))
            {
                helper.SpendMoney();
            }
            
            if (GUILayout.Button("Add Scores"))
            {
                helper.AddScores();
            }
            
            if (GUILayout.Button("Spend Scores"))
            {
                helper.SpendScores();
            }
        }
    }
}
