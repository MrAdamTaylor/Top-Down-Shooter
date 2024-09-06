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

    public void AddAgent(AIExecuter aiExecuter)
    {
        _dynamicAvoids.Add(aiExecuter);
    }

    public void FullCirlcle()
    {
        CalculateStartVectors();
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Vector3 VA = _dynamicAvoids[i].GetLastVelocity().ExcludeY();
            Vector3 PA = _dynamicAvoids[i].gameObject.transform.position.ExcludeY();
            _rvoHandler.Full(i, VA, PA, _dynamicAvoids);
            Vector3 velocity = _rvoHandler.GetResultVelocity(_dynamicAvoids[i], _desiredVectors[i]);
            _dynamicAvoids[i].SetVelocity(velocity);
            _dynamicAvoids[i].MakeStep();
        }
        _desiredVectors.Clear();
    }

    void Awake()
    {
        if (Instance == null) { 
            Instance = this; 
        } else if(Instance == this){ 
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        Debug.Log("Agents");
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Debug.Log("Enemy: "+_dynamicAvoids[i].gameObject);
        }
        _rvoHandler = gameObject.AddComponent<RVO_Handler>();
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