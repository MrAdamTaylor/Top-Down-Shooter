using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerThird : MonoBehaviour
{
    [Range(0.02f,0.2f)]
    [SerializeField] private float _controllerWorkFrequently;
    [SerializeField] private List<AIPart> _aiParts;

    [SerializeField] private AIExecuter _aiExecuter;

    
    
    void Awake()
    {
        _aiExecuter.Construct(_aiParts);

        StartCoroutine(ControllerWork());
    }

    private IEnumerator ControllerWork()
    {
        while (true)
        {
            yield return new WaitForSeconds(_controllerWorkFrequently);
            //Debug.Log("Hello everyone");
            _aiExecuter.DoStep();
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(ControllerWork());
    }
}

public class AIPart : MonoBehaviour, ISubscribe
{
    public virtual void Subscribe(Action missing_name)
    {
        
    }

    public virtual void Unsubscribe(Action missing_name)
    {
        
    }
}