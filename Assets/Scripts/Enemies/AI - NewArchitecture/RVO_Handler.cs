using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class RVO_Handler : MonoBehaviour
{
    private struct RVOData
    {
        public Vector3 Trans;
        public Vector3 LeftBound;
        public Vector3 RightBound;
        public float Dist;
        public float AvoidRadius;
    }

    private List<RVOData> _rvoDatas = new List<RVOData>();
    
    //NOTE - I don't delete comments here because in some places the code is complicated and I don't want to remember in the future where I need to add ray drawing
    public void FullRvoData(int i, Vector3 va, Vector3 pa, List<AIExecuter> dynamicAvoids)
    {
        for (int j = 0; j < dynamicAvoids.Count; j++)
        {
            if (i != j)
            {
                Vector3 neighbourVelocity = dynamicAvoids[j].GetLastVelocity().ExcludeY();
                Vector3 neighbourPosition = dynamicAvoids[j].gameObject.transform.position.ExcludeY();
                RVOData rvoData = new RVOData();
                rvoData.AvoidRadius = dynamicAvoids[i].GetRadius();
                rvoData.Trans.Set(pa.x + 0.5f * (va.x + neighbourVelocity.x), 0, pa.z + 0.5f * (va.z + neighbourVelocity.z));
                //Debug.Log(BA.Trans);
                rvoData.Dist = Vector3.Distance(pa, neighbourPosition);
                float thetaBa = Mathf.Atan2(neighbourPosition.z - pa.z, neighbourPosition.x - pa.x);
                if (dynamicAvoids[j].GetRadius() > rvoData.Dist)
                {
                    rvoData.Dist = dynamicAvoids[j].GetRadius();
                }
                float thetaBaOrt = Mathf.Asin(dynamicAvoids[j].GetRadius() / rvoData.Dist);
                float thetaOrtLeft = thetaBa + thetaBaOrt;
                rvoData.LeftBound.Set(Mathf.Cos(thetaOrtLeft),0f, Mathf.Sin(thetaOrtLeft));
                //Debug.DrawRay(dynamicAvoids[i].gameObject.transform.position, rvoData.LeftBound, Color.red, 0.1f);
                float thetaOrtRight = thetaBa - thetaBaOrt;
                rvoData.RightBound.Set(Mathf.Cos(thetaOrtRight),0f, Mathf.Sin(thetaOrtRight));
                //Debug.DrawRay(dynamicAvoids[i].gameObject.transform.position, rvoData.RightBound, Color.red, 0.1f);
                _rvoDatas.Add(rvoData);
            }
        }
    }

    public Vector3 GetResultVelocity(AIExecuter dynamicAvoid, Vector3 desiredVector)
    {
        Vector3 resultVelocity = Vector3.zero;
        Vector3 newVel = Vector3.zero;
        bool suit = true;
        float normVelo = desiredVector.magnitude;
        List<Vector3> suitableVelo = new List<Vector3>();
        List<Vector3> unSuitableVelo = new List<Vector3>();
        CheckArround(dynamicAvoid, normVelo, newVel, suitableVelo, unSuitableVelo, ref suit);
        newVel = desiredVector;
        suit = true;
        CheckForward(dynamicAvoid,newVel, suitableVelo, unSuitableVelo,ref suit);
        if(suitableVelo.Count > 0)
        {
            //Debug.Log(dynamicAvoid.name + " has " + suitableVelo.Count + " suitable velo");
            resultVelocity = SpecificSort.GetMin(suitableVelo, desiredVector);
        }
        else
        {
            resultVelocity = CalculateVec(dynamicAvoid, desiredVector, unSuitableVelo,ref resultVelocity);
        }
        _rvoDatas.Clear();
        return resultVelocity;
    }

    private Vector3 CalculateVec(AIExecuter dynamicAvoid, Vector3 desiredVector, List<Vector3> unSuitableVelo, ref Vector3 VAPost)
    {
        IDictionary<Vector3,float> dictionary = new Dictionary<Vector3, float>();
        foreach(Vector3 unsuitV in unSuitableVelo)
        {
            dictionary[unsuitV] = 0;
            List<float> tc = new List<float>();
               
            foreach (RVOData BA in _rvoDatas)
            {
                Vector3 dif = Vector3.zero;
                float rad = BA.AvoidRadius;
                dif.Set(unsuitV.x + dynamicAvoid.transform.position.x - BA.Trans.x, 0,unsuitV.z + dynamicAvoid.transform.position.z - BA.Trans.z);
                float thetaDif = Mathf.Atan2(dif.z, dif.x);
                float thetaRight = Mathf.Atan2(BA.RightBound.z, BA.RightBound.x);
                float thetaLeft= Mathf.Atan2(BA.LeftBound.z, BA.LeftBound.x);
                if (Triginometry.InBetween(thetaRight, thetaDif, thetaLeft))
                {
                    //Debug.DrawRay(dynamicAvoid.transform.position, BA.LeftBound, Color.white, 0.1f);
                    float smallTheta = Mathf.Abs(thetaDif -0.5f*(thetaLeft+ thetaRight));
                    float temp = Mathf.Abs(BA.Dist * Mathf.Sin(smallTheta));
                    if (temp >= rad)
                    {
                        rad = temp;
                    }
                    float bigTheta = Mathf.Asin(Mathf.Abs(BA.Dist * Mathf.Sin(smallTheta)) / rad);
                    float distTg = Mathf.Abs(BA.Dist * Mathf.Cos(smallTheta)) - Mathf.Abs(rad * Mathf.Cos(bigTheta));
                    if(distTg < 0)
                    {
                        distTg = 0;
                    }
                    float tcV = distTg / dif.magnitude;
                    tc.Add(tcV);
                }

            }
            //dictionary[unsuitV] = SpecificSort.Min(tc) + 0.001f;
            dictionary[unsuitV] = tc.Min() + 0.001f;
        }
        float wt = 0.2f;
        VAPost = unSuitableVelo[0];
        float lastKey = 0f;
        foreach (Vector3 v in unSuitableVelo)
        {
                
            Vector3 temp = v - desiredVector;
            float key = ((wt / dictionary[v])) + temp.magnitude;
            if (!VAPost.Equals(v))
            {
                if (key< lastKey)
                {
                    lastKey = key;
                    VAPost = v;
                }
            }else
            {
                lastKey = key;
                VAPost = v;
            }
        }

        return VAPost;
    }

    private void CheckForward(AIExecuter dynamicAvoid, Vector3 newVel, List<Vector3> suitableVelo, List<Vector3> unSuitableVelo,ref bool suit)
    {
        for (int k = 0; k < _rvoDatas.Count; k++)
        {
            Vector3 dif=Vector3.zero;
            dif.Set(newVel.x + dynamicAvoid.transform.position.x - _rvoDatas[k].Trans.x, 0,newVel.z + dynamicAvoid.transform.position.z - _rvoDatas[k].Trans.z);
            //Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.green, 0.1f);
            float thetaDiff = Mathf.Atan2(dif.z, dif.x);
            float thetaRight = Mathf.Atan2(_rvoDatas[k].RightBound.z, _rvoDatas[k].RightBound.x);
            float thetaLeft = Mathf.Atan2(_rvoDatas[k].LeftBound.z, _rvoDatas[k].LeftBound.x);
            if (Triginometry.InBetween(thetaRight, thetaDiff, thetaLeft))
            {
                //Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.magenta, 0.1f);
                suit = false;
                break;
            }
        }
        if (suit)
        {
            
            suitableVelo.Add(newVel);
        }
        else
        {
            unSuitableVelo.Add(newVel);
        }

    }

    private void CheckArround(AIExecuter dynamicAvoid, float normVelo, Vector3 newVel, List<Vector3> suitableVelo,
        List<Vector3> unSuitableVelo, ref bool suit)
    {
        
        float pi2 = Mathf.PI*2;
        for (float theta = 0f; theta < pi2; theta += 0.1f)
        {
            float velStep = normVelo / 10.0f;
            for (float rad = 0.02f; rad < normVelo + 0.02f; rad += velStep)
            {
                newVel.Set(rad*Mathf.Cos(theta),0,rad * Mathf.Sin(theta));
                //Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.blue, 0.1f);
                suit = true;
                for(int k = 0; k < _rvoDatas.Count; k++)
                {
                    Vector3 dif=Vector3.zero;
                    dif.Set(newVel.x + dynamicAvoid.transform.position.x - _rvoDatas[k].Trans.x, 0,newVel.z + dynamicAvoid.transform.position.z -_rvoDatas[k].Trans.z);
                    float thetaDiff = Mathf.Atan2(dif.z, dif.x);
                    float thetaRight = Mathf.Atan2(_rvoDatas[k].RightBound.z, _rvoDatas[k].RightBound.x);
                    float thetaLeft = Mathf.Atan2(_rvoDatas[k].LeftBound.z, _rvoDatas[k].LeftBound.x);
                    if (Triginometry.InBetween(thetaRight,thetaDiff,thetaLeft))
                    {
                        //Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.yellow, 0.1f);
                        suit = false;
                        break;
                    }
                }
                if (suit)
                {
                    suitableVelo.Add(newVel);
                }
                else
                {
                    unSuitableVelo.Add(newVel);
                }
            }
        }
        
    }
}