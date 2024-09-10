using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class AIDynamicAvoidComponent : AIComponent
{
    [SerializeField] private float _avoidRadius = 0.5f;
    [SerializeField] private float _radiusBuffer = 0.2f;
    
    public float GetRadius()
    {
        return 2 * (_avoidRadius + _radiusBuffer);
    }
}