namespace Enemies.EnemyStateMachine
{
    public interface IEnemyAttack
    {
        public bool IsCanAttack { get; }
        void DisableAttack();
        void EnableAttack();
        void Construct(EnemyAnimator enemyAnimator, float configsMinDamage, float configsMaxDamage);
    }
}