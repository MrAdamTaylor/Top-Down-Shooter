using UnityEngine;

namespace EnterpriceLogic.Utilities.Drawers
{
    public class FieldOfViewDrawer : MonoBehaviour
    {
        public float Radius;
        public Color ColorValue;
        [Range(0f, 360f)] public float AngleFov;

        //TODO - commented during compilation WebGL app
        /*private void OnDrawGizmos()
    {
        Handles.color = ColorValue;
        Handles.DrawSolidDisc(transform.position, transform.up, Radius);
    }*/

        public void SetRadius(float radius)
        {
            Radius = radius;
        }
    }
}