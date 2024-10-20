using System;
using UnityEngine;

public class CheckAttack : MonoBehaviour
{
    private EnemyAttack _attack;
    private ReactionTrigger _reactionTrigger;

    public void Construct(EnemyAttack enemyAttack, ReactionTrigger reactionTrigger)
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
        _reactionTrigger.TriggerAction -= TriggerEndAttack;
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