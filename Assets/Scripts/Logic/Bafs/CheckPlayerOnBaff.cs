using EnterpriceLogic;
using UnityEngine;

namespace Logic.Bafs
{
    public class CheckPlayerOnBaff : MonoBehaviour
    {
        private ReactionTrigger _reactionTrigger;
        private IBaffComponent _baffComponent;

        public void Construct(IBaffComponent baffComponent, ReactionTrigger reactionTrigger)
        {
            _baffComponent = baffComponent;
            _reactionTrigger = reactionTrigger;
            _reactionTrigger.TriggerAction += Triggerred;
        }

        private void Triggerred()
        {
            _baffComponent.AddBaff();
        }

        private void OnDestroy()
        {
            _reactionTrigger.TriggerAction -= Triggerred;
        }
    }
}