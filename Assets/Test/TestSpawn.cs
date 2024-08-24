using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public GameObject someObject;
    public Vector3 Position;

    void Start()
    {
        // Instantiate an object to the right of the current object
        Vector3 thePosition = transform.TransformPoint(Position);
        Instantiate(someObject, thePosition, someObject.transform.rotation);
    }
}
