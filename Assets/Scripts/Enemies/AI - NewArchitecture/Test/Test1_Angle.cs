using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1_Angle : MonoBehaviour
{
    public Transform Target;

    public List<Transform> Agengts;

    public void Start()
    {
        for (int i = 0; i < Agengts.Count; i++)
        {
            Vector3 relative = Agengts[i].InverseTransformPoint(Target.position);
            Debug.Log("Cordinate from local: "+relative);
            float angle2 = Mathf.Atan2(relative.x, relative.z);
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            Debug.Log($"Angle from {Agengts[i].name} - {angle}  {angle2}");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < Agengts.Count; i++)
            {
                Vector3 relative = Agengts[i].InverseTransformPoint(Target.position);
                float angle2 = Mathf.Atan2(relative.x, relative.z);
                float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                Debug.Log($"Angle from {Agengts[i].name} - {angle}  {angle2}");
            }
        }
    }
}
