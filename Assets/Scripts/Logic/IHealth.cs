namespace Logic
{
    public interface IHealth : IPlayableComponent
    {
        public void TakeDamage(float damage);
    }
}