using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RVO_Controller : MonoBehaviour
{
    [SerializeField] private float _roboRadius = 0.2f;
    [SerializeField] private float _radiusBuffer = 0.01f;
    
    private static RVO_Controller _instance;
    private float _twiceRadius;

    private static List<NewEnemyController> _controllers = new List<NewEnemyController>();

   void Awake()
    {
        if (_instance == null) { 
            _instance = this; 
        } else if(_instance == this){ 
            Destroy(gameObject); 
        }
    }
    
    void Start () {
        _twiceRadius = 2 * (_roboRadius + _radiusBuffer);
    }

    public static void AddController(NewEnemyController newEnemyController)
    {
        _controllers.Add(newEnemyController);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                Debug.Log($"I have "+_controllers[i]);
            }
        }

        for (int i = 0; i < _controllers.Count; i++)
        {
            Vector3 VA = _controllers[i].GetVelocity();
            Vector3 PA = _controllers[i].gameObject.transform.position;

            ArrayList RVO_ALL = new ArrayList();
            for (int j = 0; j < _controllers.Count; j++)
            {
                if (i != j)
                {
                    Vector3 VB = _controllers[j].GetVelocity();
                    Vector3 PB = _controllers[j].gameObject.transform.position;
                    RVO_BA BA = new RVO_BA();
                    BA.AvoidRadius = _twiceRadius;
                    if (_controllers[j].isActiveAndEnabled)
                    {
                        BA.Trans.Set(PA.x + 0.5f * (VA.x + VB.x), 0f,PA.z + 0.5f * (VA.z + VB.z));
                    }
                    else
                    {
                        BA.Trans.Set(PA.x + 0.5f * (VA.x + VB.x), 0f, PA.z + 0.5f * (VA.z + VB.z));
                    }
                    BA.Dist = Vector3.Distance(PA, PB);
                    float theta_BA = Mathf.Atan2(PB.z - PA.z, PB.x - PA.x);
                    if (_twiceRadius > BA.Dist)
                    {
                        BA.Dist = _twiceRadius;
                    }
                    float thetaBAOrt = Mathf.Asin(_twiceRadius / BA.Dist);
                    float thetaOrtLeft = theta_BA + thetaBAOrt;
                    BA.LeftBound.Set(Mathf.Cos(thetaOrtLeft),0f, Mathf.Sin(thetaOrtLeft));
                    Debug.DrawRay(_controllers[i].transform.position, BA.LeftBound, Color.red, 0.2f);
                    float thetaOrtRight = theta_BA - thetaBAOrt;
                    BA.RightBound.Set(Mathf.Cos(thetaOrtRight),0f, Mathf.Sin(thetaOrtRight));
                    Debug.DrawRay(_controllers[i].transform.position, BA.RightBound, Color.red, 0.2f);
                    RVO_ALL.Add(BA);
                }
            }

            _controllers[i].SetVelocity(Intersect(i,RVO_ALL));
            //Intersect(i, RVO_ALL);
        }
    }

    private Vector3 Intersect(int i, ArrayList rvoAll)
    {
        NewEnemyController controller = _controllers[i];
        Vector3 VAPost = Vector3.zero;
        Vector3 newVel = Vector2.zero;
        bool suit = true;
        float normVelo = controller.GetDesiredVelocity().magnitude;
        Debug.Log("Magnitude to Player "+normVelo);
        ArrayList suitableVelo = new ArrayList();
        ArrayList unSuitableVelo = new ArrayList();
        float pi2 = Mathf.PI*2;
        for(float theta = 0f; theta < pi2; theta += 0.1f)
        {
            float velStep = normVelo / 10.0f;
            for(float rad = 0.02f;rad< normVelo + 0.02f; rad += velStep)
            {
                newVel.Set(rad*Mathf.Cos(theta),0,rad * Mathf.Sin(theta));
                Debug.DrawRay(controller.transform.position, newVel, Color.blue, 0.1f);
                suit = true;
                foreach(RVO_BA BA in rvoAll)
                {
                    Vector3 dif=Vector3.zero;
                    dif.Set(newVel.x + controller.transform.position.x -BA.Trans.x, 0f, newVel.z + controller.transform.position.z - BA.Trans.z);
                    Debug.DrawRay(controller.transform.position, dif, Color.green, 0.1f);
                    float theta_diff = Mathf.Atan2(dif.y, dif.x);
                    float theta_right = Mathf.Atan2(BA.RightBound.z, BA.RightBound.x);
                    float theta_left = Mathf.Atan2(BA.LeftBound.z, BA.LeftBound.x);
                    if (inBetween(theta_right,theta_diff,theta_left))
                    {
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
        newVel = controller.GetDesiredVelocity();
        suit = true;
        foreach (RVO_BA BA in rvoAll)
        {
            Vector3 dif = Vector3.zero;
            dif.Set(newVel.x + controller.transform.position.x - BA.Trans.x, 0f,newVel.y + controller.transform.position.y - BA.Trans.y);
            Debug.DrawRay(controller.transform.position, newVel, Color.green, 0.1f);
            float theta_diff = Mathf.Atan2(dif.z, dif.x);
            float theta_right = Mathf.Atan2(BA.RightBound.z, BA.RightBound.x);
            float theta_left = Mathf.Atan2(BA.LeftBound.z, BA.LeftBound.x);
            if (inBetween(theta_right, theta_diff, theta_left))
            {
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
        if(suitableVelo.Count > 0)
        {
            Debug.Log(controller.name + " has " + suitableVelo.Count + " suitable velo");
            VAPost = min(suitableVelo, controller.GetDesiredVelocity());
            //Debug.Log("min" + VAPost);
        }
        else
        {
            IDictionary<Vector3,float> tc_V = new Dictionary<Vector3, float>();
            foreach(Vector3 unsuitV in unSuitableVelo)
            {
                tc_V[unsuitV] = 0;
                ArrayList tc = new ArrayList();
               
                foreach (RVO_BA BA in rvoAll)
                {
                    Vector3 dif = Vector3.zero;
                    float rad = BA.AvoidRadius;
                    dif.Set(unsuitV.x + controller.transform.position.x - BA.Trans.x, 0f,unsuitV.z + controller.transform.position.z - BA.Trans.z);
                    float theta_dif = Mathf.Atan2(dif.y, dif.x);
                    float theta_right = Mathf.Atan2(BA.RightBound.y, BA.RightBound.x);
                    float theta_left= Mathf.Atan2(BA.LeftBound.y, BA.LeftBound.x);
                    if (inBetween(theta_right, theta_dif, theta_left))
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
                tc_V[unsuitV] = min(tc) + 0.001f;
            }
            float WT = 0.2f;
            VAPost = (Vector3)unSuitableVelo[0];
            float lastKey = 0f;
            foreach (Vector3 v in unSuitableVelo)
            {
                
                Vector3 temp = v - controller.GetDesiredVelocity();
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
        }
        return VAPost;
        //throw new Exception();
    }
    
    private bool inBetween(float thetaRight, float thetaDif,float thetaLeft)
    {
        if(Mathf.Abs(thetaRight- thetaLeft)<= Mathf.PI)
        {
            if(thetaRight<=thetaDif && thetaDif<= thetaLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(thetaLeft <0 && thetaRight> 0)
            {
                thetaLeft += 2 * Mathf.PI;
                if (thetaDif < 0)
                {
                    thetaDif += 2 * Mathf.PI;
                }
                if (thetaRight <= thetaDif && thetaDif <= thetaLeft)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (thetaLeft > 0 && thetaRight< 0)
            {
                thetaRight += 2 * Mathf.PI;
                if (thetaDif < 0)
                {
                    thetaDif += 2 * Mathf.PI;
                }
                if (thetaLeft <= thetaDif && thetaDif <= thetaRight )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    private Vector3 min(ArrayList mylist, Vector3 desVel)
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
    private float min(ArrayList mylist)
    {
        
        float diff = (float)mylist[0];
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


struct RVO_BA
{
    public Vector3 Trans;
    public Vector3 LeftBound;
    public Vector3 RightBound;
    public float Dist;
    public float AvoidRadius;
}