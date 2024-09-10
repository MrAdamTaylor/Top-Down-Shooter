using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class EnemyBehaviour : MonoBehaviour
{
    [Range(0.02f,0.2f)]
    [SerializeField] private float _controllerWorkFrequently;
    [SerializeField] private List<AIComponent> _aiParts;

   [SerializeField] private AIExecuter _aiExecuter;
    
    void Awake()
    {
        _aiExecuter.Construct(_aiParts);

        StartCoroutine(ControllerWork());
    }

    private void OnDestroy()
    {
        StopCoroutine(ControllerWork());
    }

    private IEnumerator ControllerWork()
    {
        while (true)
        {
            yield return new WaitForSeconds(_controllerWorkFrequently);
            _aiExecuter.DoStep();
        }
    }
}