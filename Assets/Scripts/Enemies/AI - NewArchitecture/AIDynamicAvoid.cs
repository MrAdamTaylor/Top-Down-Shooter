using UnityEngine;

public class AIDynamicAvoid : AIPart
{
    [SerializeField] private float _avoidRadius = 0.5f;
    [SerializeField] private float _radiusBuffer = 0.2f;


    public float GetRadius()
    {
        return 2 * (_avoidRadius + _radiusBuffer);
    }
}