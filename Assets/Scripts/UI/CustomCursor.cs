using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public RectTransform crosshair; // Привяжите сюда UI-объект с прицелом
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        crosshair.position = mousePosition;
    }
}
