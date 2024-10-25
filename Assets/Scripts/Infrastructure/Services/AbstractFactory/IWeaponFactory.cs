
namespace Infrastructure.Services.AbstractFactory
{
    public interface IWeaponFactory : IGameFactory
    {
        public void CreateAll(Weapon.Weapon[] weapon);
    }
}