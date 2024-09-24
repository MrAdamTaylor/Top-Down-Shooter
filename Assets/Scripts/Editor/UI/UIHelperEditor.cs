using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIHelper))]
public class UIHelperEditor : Editor
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

        /*if (GUILayout.Button("Add Ammo"))
        {
            helper.AddAmmo();
        }

        if (GUILayout.Button("Spend Ammo"))
        {
            helper.SpendAmmo();
        }*/
    }
    
}
