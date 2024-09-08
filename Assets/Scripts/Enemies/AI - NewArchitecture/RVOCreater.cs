using System;
using UnityEngine;

public class RVOCreater
{
    public static RVOCreater Instance;

    private bool _isCreated;
    private RVOController _rvoController;
    
    public static RVOCreater GetInstance()
    {
        return Instance ??= new RVOCreater();
    }

    public RVOController Create()
    {
        Debug.Log("Creater!");
        if (!_isCreated)
        {
            GameObject
                instancedObj =
                    new GameObject(
                        "RVO_Controller");
            instancedObj.AddComponent<RVOController>();
            _rvoController = instancedObj.GetComponent<RVOController>();
            _isCreated = true;
            return _rvoController;
        }
        else
        {
            return _rvoController;
        }
    }
}