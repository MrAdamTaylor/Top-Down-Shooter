using System;

namespace Enemies.EnemyStateMachine
{
    public interface IEnemyAttack
    {
        public Action ActionAttackEnd { get; set; }
        public bool IsCanAttack { get; }
        void DisableAttack();
        void EnableAttack();
        void Construct(EnemyAnimator enemyAnimator, float configsMinDamage, float configsMaxDamage, float hitBox);
        void Attack();
        public bool CooldownIsUp();
    }
}