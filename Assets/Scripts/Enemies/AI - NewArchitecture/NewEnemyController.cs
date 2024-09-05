using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    [SerializeField] private List<AIComponent> _components = new List<AIComponent>();
    

    void Start()
    {
        RVO_Controller.AddController(this);
        for (int i = 0; i < _components.Count; i++)
        {
            _components[i].OnStart();
        }
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetDesiredVelocity()
    {
        Vector3 desiredVelocity = new Vector3();
        for (int i = 0; i < _components.Count; i++)
        {
            Mover mover;
            if (_components[i].TryGetComponent<Mover>(out mover))
            {
                desiredVelocity = mover.GetVelocity();
            }
        }
        return desiredVelocity;
    }

    public Vector3 GetVelocity()
    {
        Vector3 velocity = new Vector3();
        for (int i = 0; i < _components.Count; i++)
        {
            SpeedFinder finder;
            if (_components[i].TryGetComponent<SpeedFinder>(out finder))
            {
                velocity = finder.GetVelocity();
            }
        }
        return velocity;
    }

    public void SetVelocity(Vector3 intersect)
    {
        SpeedFinder finder;
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].TryGetComponent<SpeedFinder>(out finder))
            {
                finder.SetVelocity(intersect);
            }
        }
    }
}

public class AIComponent : MonoBehaviour
{

    public virtual void OnStart()
    {
        
    }

}