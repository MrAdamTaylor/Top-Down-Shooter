using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleDrawer))]
public class CircleDrawerEditor : Editor
{
    private void OnSceneGUI()
    {
        CircleDrawer drawer = (CircleDrawer)target;
        
        Color c = drawer.ColorValue;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidDisc(
            drawer.transform.position,
            drawer.transform.up,
            drawer.Radius);

        Handles.color = c;
        drawer.Radius = Handles.ScaleValueHandle(
            drawer.Radius,
        drawer.transform.position + drawer.transform.forward * drawer.Radius,
            drawer.transform.rotation,
            3,
            Handles.SphereHandleCap,
        1);
    }
}
