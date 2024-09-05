using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin_And_Cos_Test : MonoBehaviour
{
    [SerializeField] private int _xMax = 160;
    [SerializeField] private int _yMax = 100;

    private Vector3 _startPointX;
    private Vector3 _endPointX;
    private Vector3 _startPointY;
    private Vector3 _endPointY;

    void Start()
    {
        _startPointX = new Vector3(-_xMax, 0);
        _endPointX = new Vector3(_xMax, 0);
        _startPointY = new Vector3(-_yMax, 0);
        _endPointY = new Vector3(_yMax, 0);

        DrawLine(_startPointX, _endPointX, 0.05f, Color.red);
        DrawLine(_startPointY, _endPointY, 0.05f, Color.red);
        
        DrawLine(Vector3.zero, new Vector3(2,3,0), 0.1f, Color.yellow);
        
        DrawLine(new Vector3(2,3,0), new Vector3(0,3,0), 0.1f, Color.magenta);
        DrawLine(new Vector3(2,3,0), new Vector3(2,0,0), 0.1f, Color.cyan);
        
        
    }


    public void DrawLine(Vector3 origin, Vector3 end, float with, Color color)
    {
        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.startWidth = with;
        lineRenderer.endWidth = with;
        lineRenderer.SetPosition(0, new Vector3(origin.x, origin.y, 0));
        lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
