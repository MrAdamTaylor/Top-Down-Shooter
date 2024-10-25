using Logic;

namespace Enemies
{
    public interface IHealth : IPlayableComponent
    {
        public void TakeDamage(float damage);
    }
}