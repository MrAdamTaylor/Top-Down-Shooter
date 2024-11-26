using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public RectTransform crosshair; // ��������� ���� UI-������ � ��������
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
