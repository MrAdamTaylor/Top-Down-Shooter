using Configs;

namespace Enemies
{
    public interface IBaffComponent
    {
        public void Construct(Player.Player player, BafConfigs bafConfigs);
        
        public void AddBaff();
    }
}