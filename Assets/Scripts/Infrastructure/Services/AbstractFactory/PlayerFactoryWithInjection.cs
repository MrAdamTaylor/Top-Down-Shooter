using Configs;
using Infrastructure.Services.AssertService;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public class PlayerFactoryWithInjection : IPlayerFactory
    {
        private readonly IAssertByString<GameObject> _objAssert;
        private readonly IWeaponFactory _weaponFactory;
        private PlayerConfigs _playerConfigs;

        public PlayerFactoryWithInjection(AssertBuilder builder, IWeaponFactory weaponFactory, PlayerConfigs playerConfigs)
        {
            _objAssert = builder.BuildAssertServiceByString<GameObject>();
            _weaponFactory = weaponFactory;
            _playerConfigs = playerConfigs;
        }
        
        public GameObject Create(Vector3 position, Camera camera)
        {
            GameObject playerObject = _objAssert.Assert(_playerConfigs.PathToPlayer, position);
            return playerObject;
        }

        /*public GameObject Create(ScriptableObject scriptableObject, Vector3 position, GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }*/
    }

    public interface IFactory : IService
    {
        public GameObject Create(Vector3 position, GameObject gameObject);
    }
}