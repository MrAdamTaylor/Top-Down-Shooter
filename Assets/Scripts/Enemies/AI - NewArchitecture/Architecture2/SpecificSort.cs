using System.Collections.Generic;
using UnityEngine;

public static class SpecificSort
{
    public static Vector2 Min(List<Vector3> mylist, Vector3 desVel)
    {
        Vector3 result = -desVel;
        float diff =2*desVel.magnitude;
        foreach(Vector3 vel in mylist)
        {
            Vector3 diffVec = vel - desVel;
            if (diffVec.magnitude < diff)
            {
                result = vel;
                diff = diffVec.magnitude;
            }
        }
        return result;
    }
    

    public static float Min(List<float> mylist)
    {
        float diff = mylist[0];
        foreach (float vel in mylist)
        {
            
            if (vel < diff)
            {
                diff = vel;
            
            }
        }
        return diff;
    }
}