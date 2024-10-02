using UnityEngine;


public class WorkingTurret : MonoBehaviour
{
    public Transform Target;
    public WedgeTrigger Trigger;
    public Transform GunTf;
    public float SmoothingFactor = 10;

    private Quaternion _targetRotation;
    
    public void Update()
    {
        if (Trigger.Contains(Target.position))
        {
            Vector3 vecToTarget = Target.position - GunTf.position;
            _targetRotation = Quaternion.LookRotation(vecToTarget, transform.up);
        }

        GunTf.rotation = Quaternion.Slerp(GunTf.rotation, _targetRotation, SmoothingFactor * Time.deltaTime);
    }
}