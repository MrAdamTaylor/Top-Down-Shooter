using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RVOController : MonoBehaviour
{
    public static RVOController Instance;

    private List<AIExecuter> _dynamicAvoids = new List<AIExecuter>();

    private List<Vector3> _desiredVectors = new List<Vector3>();

    private RVO_Handler _rvoHandler;
    
    void Awake()
    {
        if (Instance == null) { 
            Instance = this; 
        } else if(Instance == this){ 
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        Debug.Log("Agents");
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Debug.Log("Enemy: "+_dynamicAvoids[i].gameObject);
        }
        _rvoHandler = gameObject.AddComponent<RVO_Handler>();
    }

    public void AddAgent(AIExecuter aiExecuter)
    {
        _dynamicAvoids.Add(aiExecuter);
    }

    public void FullCirlcle()
    {
        CalculateStartVectors();
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Vector3 VA = _dynamicAvoids[i].getLastVelocity().ExcludeY();
            Vector3 PA = _dynamicAvoids[i].gameObject.transform.position.ExcludeY();
            _rvoHandler.Full(i, VA, PA, _dynamicAvoids);
            Vector3 velocity = _rvoHandler.GetResultVelocity(_dynamicAvoids[i], _desiredVectors[i]);
            _dynamicAvoids[i].SetVelocity(velocity);
            _dynamicAvoids[i].MakeStep();
        }
    }

    private void CalculateStartVectors()
    {
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Vector3 vec = _dynamicAvoids[i].CalculateVectors();
            _desiredVectors.Add(vec);
        }
    }
}

public class RVO_Handler : MonoBehaviour
{
    public struct RVOData
    {
        public Vector3 Trans;
        public Vector3 LeftBound;
        public Vector3 RightBound;
        public float Dist;
        public float AvoidRadius;
    }

    public List<RVOData> _rvoDatas = new List<RVOData>();
    
    public void Full(int i, Vector3 va, Vector3 pa, List<AIExecuter> dynamicAvoids)
    {
        for (int j = 0; j < dynamicAvoids.Count; j++)
        {
            if (i != j)
            {
                Vector3 VB = dynamicAvoids[j].getLastVelocity().ExcludeY();
                Vector3 PB = dynamicAvoids[j].gameObject.transform.position.ExcludeY();
                RVOData BA = new RVOData();
                BA.AvoidRadius = dynamicAvoids[i].GetRadius();
                BA.Trans.Set(pa.x + 0.5f * (va.x + VB.x), 0, pa.z + 0.5f * (va.z + VB.z));
                Debug.Log(BA.Trans);
                BA.Dist = Vector3.Distance(pa, PB);
                float theta_BA = Mathf.Atan2(PB.z - pa.z, PB.x - pa.x);
                if (dynamicAvoids[j].GetRadius() > BA.Dist)
                {
                    BA.Dist = dynamicAvoids[j].GetRadius();
                }
                float thetaBAOrt = Mathf.Asin(dynamicAvoids[j].GetRadius() / BA.Dist);
                float thetaOrtLeft = theta_BA + thetaBAOrt;
                BA.LeftBound.Set(Mathf.Cos(thetaOrtLeft),0f, Mathf.Sin(thetaOrtLeft));
                Debug.DrawRay(dynamicAvoids[i].gameObject.transform.position, BA.LeftBound, Color.red, 0.2f);
                float thetaOrtRight = theta_BA - thetaBAOrt;
                BA.RightBound.Set(Mathf.Cos(thetaOrtRight),0f, Mathf.Sin(thetaOrtRight));
                Debug.DrawRay(dynamicAvoids[i].gameObject.transform.position, BA.RightBound, Color.red, 0.2f);
                _rvoDatas.Add(BA);
            }
        }
        //_rvoDatas.Clear();
    }


    public Vector3 GetResultVelocity(AIExecuter dynamicAvoid, Vector3 desiredVector)
    {
        Vector3 VAPost = Vector3.zero;
        Vector3 newVel = Vector2.zero;
        bool suit = true;
        float normVelo = desiredVector.magnitude;
        List<Vector3> suitableVelo = new List<Vector3>();
        List<Vector3> unSuitableVelo = new List<Vector3>();
        CheckArround(dynamicAvoid, normVelo, newVel, suitableVelo, unSuitableVelo);
        newVel = desiredVector;
        suit = true;
        CheckForward(dynamicAvoid, normVelo, newVel, suitableVelo, unSuitableVelo, suit);
        if(suitableVelo.Count > 0)
        {
            Debug.Log(dynamicAvoid.name + " has " + suitableVelo.Count + " suitable velo");
            VAPost = SpecificSort.Min(suitableVelo, desiredVector);
            //Debug.Log("min" + VAPost);
        }
        else
        {
            VAPost = CalculateVec(dynamicAvoid, desiredVector, unSuitableVelo);
        }
        _rvoDatas.Clear();
        return VAPost;
    }

    private Vector3 CalculateVec(AIExecuter dynamicAvoid, Vector3 desiredVector, List<Vector3> unSuitableVelo)
    {
        Vector3 VAPost;
        IDictionary<Vector3,float> tc_V = new Dictionary<Vector3, float>();
        foreach(Vector3 unsuitV in unSuitableVelo)
        {
            tc_V[unsuitV] = 0;
            List<float> tc = new List<float>();
               
            foreach (RVOData BA in _rvoDatas)
            {
                Vector3 dif = Vector3.zero;
                float rad = BA.AvoidRadius;
                dif.Set(unsuitV.x + dynamicAvoid.transform.position.x - BA.Trans.x, 0,unsuitV.z + dynamicAvoid.transform.position.z - BA.Trans.z);
                float theta_dif = Mathf.Atan2(dif.z, dif.x);
                float theta_right = Mathf.Atan2(BA.RightBound.z, BA.RightBound.x);
                float theta_left= Mathf.Atan2(BA.LeftBound.y, BA.LeftBound.x);
                if (Triginometry.InBetween(theta_right, theta_dif, theta_left))
                {
                    float small_theta = Mathf.Abs(theta_dif -0.5f*(theta_left+ theta_right));
                    float temp = Mathf.Abs(BA.Dist * Mathf.Sin(small_theta));
                    if (temp >= rad)
                    {
                        rad = temp;
                    }
                    float big_theta = Mathf.Asin(Mathf.Abs(BA.Dist * Mathf.Sin(small_theta)) / rad);
                    float dist_tg = Mathf.Abs(BA.Dist * Mathf.Cos(small_theta)) - Mathf.Abs(rad * Mathf.Cos(big_theta));
                    if(dist_tg < 0)
                    {
                        dist_tg = 0;
                    }
                    float tc_v = dist_tg / dif.magnitude;
                    tc.Add(tc_v);
                }

            }
            tc_V[unsuitV] = SpecificSort.Min(tc) + 0.001f;
        }
        float WT = 0.2f;
        VAPost = unSuitableVelo[0];
        float lastKey = 0f;
        foreach (Vector3 v in unSuitableVelo)
        {
                
            Vector2 temp = v - desiredVector;
            float key = ((WT / tc_V[v])) + temp.magnitude;
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

    private void CheckForward(AIExecuter dynamicAvoid, float normVelo, Vector3 newVel, List<Vector3> suitableVelo, List<Vector3> unSuitableVelo, bool suit)
    {
        for (int k = 0; k < _rvoDatas.Count; k++)
        {
            Vector3 dif=Vector3.zero;
            dif.Set(newVel.x + dynamicAvoid.transform.position.x - _rvoDatas[k].Trans.x, 0,newVel.z + dynamicAvoid.transform.position.z - _rvoDatas[k].Trans.z);
            Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.green, 0.1f);
            float theta_diff = Mathf.Atan2(dif.y, dif.x);
            float theta_right = Mathf.Atan2(_rvoDatas[k].RightBound.y, _rvoDatas[k].RightBound.x);
            float theta_left = Mathf.Atan2(_rvoDatas[k].LeftBound.y, _rvoDatas[k].LeftBound.x);
            if (Triginometry.InBetween(theta_right, theta_diff, theta_left))
            {
                Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.magenta, 0.1f);
                suit = false;
                break;
            }
            else
            {
                Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.cyan, 0.1f);
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
        List<Vector3> unSuitableVelo)
    {
        bool suit;
        float PI2 = Mathf.PI*2;
        for (float theta = 0f; theta < PI2; theta += 0.1f)
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
                    float theta_diff = Mathf.Atan2(dif.z, dif.x);
                    float theta_right = Mathf.Atan2(_rvoDatas[k].RightBound.z, _rvoDatas[k].RightBound.x);
                    float theta_left = Mathf.Atan2(_rvoDatas[k].LeftBound.z, _rvoDatas[k].RightBound.x);
                    if (Triginometry.InBetween(theta_right,theta_diff,theta_left))
                    {
                        Debug.DrawRay(dynamicAvoid.transform.position, newVel, Color.yellow, 0.1f);
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