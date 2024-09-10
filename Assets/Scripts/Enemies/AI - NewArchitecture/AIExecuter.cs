using System.Collections.Generic;
using UnityEngine;

//TODO - A component of the test AI that is in the process of being finalized
public class AIExecuter : MonoBehaviour
{
    private AIMover _mover;
    private AIDynamicAvoidComponent _aiDynamicAvoidComponent;
    private AIRotate _aiRotate;

    private bool _isDynamicAvoid;
    private static RVOController _rvoController;
    
    public void Construct(List<AIPart> parts)
    {
        for (int i = 0; i < parts.Count; i++)
        {
            ComponentValidate(parts, i);
        }
    }

    public void DoStep()
    {
        if (_isDynamicAvoid)
        {
            DoDynamic();
        }
        else
        {
            DoSimple();
        }
    }

    public Vector3 CalculateVectors()
    {
        _mover.CalculateDirection();
        return _mover.GetVelocity();
    }

    public Vector3 GetLastVelocity()
    {
        return _mover.GetLastVelocity();
    }

    public float GetRadius()
    {
        return _aiDynamicAvoidComponent.GetRadius();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _mover.SetVelocity(velocity);
    }

    public void MakeStep()
    {
        _mover.DoStep();
    }

    private void ComponentValidate(List<AIPart> parts, int i)
    {
        if (parts[i].GetType() == typeof(AIMover))
        {
            _mover = (AIMover)parts[i];
        }

        if (parts[i].GetType() == typeof(AIDynamicAvoidComponent))
        {
            _aiDynamicAvoidComponent = (AIDynamicAvoidComponent)parts[i];
            _isDynamicAvoid = true;
            _rvoController = RVOCreater.GetInstance().Create();
            _rvoController.AddAgent(this);
        }

        if (parts[i].GetType() == typeof(AIRotate))
        {
            _aiRotate = (AIRotate)parts[i];
        }
    }

    private void DoSimple()
    {
        _mover.CalculateDirection();
        _mover.DoStep();
        if(_aiRotate != null)
            _aiRotate.MakeRotate(true);
    }

    private void DoDynamic()
    {
        if(_mover == null)
            return;
        
        _rvoController.OneFrameOfDunamicAI();
        if(_aiRotate != null)
            _aiRotate.MakeRotate(true);
    }
}