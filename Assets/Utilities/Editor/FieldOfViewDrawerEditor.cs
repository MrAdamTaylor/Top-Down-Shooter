using EnterpriceLogic.Utilities.Drawers;
using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    [CustomEditor(typeof(FieldOfViewDrawer))]
    public class FieldOfViewDrawerEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            FieldOfViewDrawer drawer = (FieldOfViewDrawer)target;
        
            Color c = drawer.ColorValue;

            Handles.color = new Color(c.r, c.g, c.b, 0.3f);
            Handles.DrawSolidArc(
                drawer.transform.position,
                drawer.transform.up,
                Quaternion.AngleAxis(-drawer.AngleFov / 2f, drawer.transform.up) * drawer.transform.forward,
                drawer.AngleFov,
                drawer.Radius);
            //drawer.Radius);

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
}


