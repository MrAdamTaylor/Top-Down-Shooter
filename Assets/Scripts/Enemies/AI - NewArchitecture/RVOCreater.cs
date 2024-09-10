using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class RVOCreater
{
    private static RVOCreater _instance;

    private bool _isCreated;
    private RVOController _rvoController;
    
    public static RVOCreater GetInstance()
    {
        return _instance ??= new RVOCreater();
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