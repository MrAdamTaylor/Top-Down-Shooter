using System.Collections.Generic;
using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class RVOController : MonoBehaviour
{
    private static RVOController _instance;

    private List<AIExecuter> _dynamicAvoids = new List<AIExecuter>();

    private List<Vector3> _desiredVectors = new List<Vector3>();

    private RVO_Handler _rvoHandler;

    private void Awake()
    {
        if (_instance == null) { 
            _instance = this; 
        } else if(_instance == this){ 
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

    public void OneFrameOfDunamicAI()
    {
        CalculateStartVectors();
        for (int i = 0; i < _dynamicAvoids.Count; i++)
        {
            Vector3 VA = _dynamicAvoids[i].GetLastVelocity().ExcludeY();
            Vector3 PA = _dynamicAvoids[i].gameObject.transform.position.ExcludeY();
            _rvoHandler.FullRvoData(i, VA, PA, _dynamicAvoids);
            Vector3 velocity = _rvoHandler.GetResultVelocity(_dynamicAvoids[i], _desiredVectors[i]);
            _dynamicAvoids[i].SetVelocity(velocity);
            _dynamicAvoids[i].MakeStep();
        }
        _desiredVectors.Clear();
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