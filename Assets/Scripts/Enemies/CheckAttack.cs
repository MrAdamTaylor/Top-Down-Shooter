using Enemies.EnemyStateMachine;
using EnterpriceLogic;
using UnityEngine;

namespace Enemies
{
    public class CheckAttack : MonoBehaviour
    {
        private IEnemyAttack _attack;
        private ReactionTrigger _reactionTrigger;

        public void Construct(IEnemyAttack enemyAttack, ReactionTrigger reactionTrigger)
        {
            _attack = enemyAttack;
            _reactionTrigger = reactionTrigger;
            _reactionTrigger.TriggerAction += TriggerAttack;
            _reactionTrigger.TriggerEndAction += TriggerEndAttack;
            _attack.DisableAttack();
        }

        private void OnDestroy()
        {
            _reactionTrigger.TriggerAction -= TriggerAttack;
            _reactionTrigger.TriggerEndAction -= TriggerEndAttack;
        }

        private void TriggerAttack()
        {
            _attack.EnableAttack();
        }

        private void TriggerEndAttack()
        {
            _attack.DisableAttack();
        }
    }
}