using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Logic;
using Player;
using Player.MouseInput;
using Player.MovementSystem;
using Player.NewWeaponControllSystem;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly IAssertByString<GameObject> _objAssert;
    
        public PlayerFactory(AssertBuilder builder, IWeaponFactory weaponFactory)
        {
            _objAssert = new AssertServiceString<GameObject>();
            _weaponFactory = weaponFactory;
        }

        public GameObject Create(Vector3 position, Camera camera)
        {
            PlayerConfigs playerConfigs = (PlayerConfigs)ServiceLocator.ServiceLocator.Instance.GetData(typeof(PlayerConfigs));
            GameObject gameObject = _objAssert.Assert(playerConfigs.PathToPlayer, position);
            Player.Player player = gameObject.AddComponent<Player.Player>();
            player.Construct(playerConfigs.Speed);
        
            Transform physic = player.transform.Find(Constants.PREFAB_PHYSIC_COMPONENT_NAME);
            PlayLoopComponentProvider playLoopComponentProvider = physic.GetComponent<PlayLoopComponentProvider>();
        
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(Transform), player.transform);
            gameObject.AddComponent<CameraFollower>().Construct(camera, player);
            gameObject.AddComponent<MouseRotateController>().Construct(camera, player);
            IInputSystem system = gameObject.AddComponent<AxisInputSystem>();
            gameObject.AddComponent<MoveController>().Construct(player, system);
            
            WeaponSwitcher switcher = gameObject.AddComponent<WeaponSwitcher>();
            gameObject.AddComponent<WeaponController>();
            WeaponProvider provider = gameObject.transform.GetComponentInChildren<WeaponProvider>();
            _weaponFactory.CreateAll(provider.ReturnWeapons());
            ServiceLocator.ServiceLocator.Instance.CleanData(typeof(Transform));
        
            WeaponController controller = gameObject.GetComponent<WeaponController>();
            CurrentWeaponConstructor currentWeaponConstructor = new CurrentWeaponConstructor(controller);
            switcher.Construct(provider, currentWeaponConstructor);
            controller.Construct(provider.ReturnWeapons(), switcher);
        
            PlayerHealth playerHealth = gameObject.AddComponent<PlayerHealth>();
            playerHealth.Construct(playerConfigs.Health);
            playLoopComponentProvider.AddToProvideComponent(playerHealth);
            return gameObject;
        }
    }
}