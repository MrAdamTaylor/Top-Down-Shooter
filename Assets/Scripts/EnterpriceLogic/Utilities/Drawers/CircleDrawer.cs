using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CircleDrawer : MonoBehaviour
{
    public float Radius;
    public Color ColorValue;

    private void OnDrawGizmos()
    {
        Handles.color = ColorValue;
        Handles.DrawSolidDisc(transform.position, transform.up, Radius);
    }

    public void SetRadius(float radius)
    {
        Radius = radius;
    }
}