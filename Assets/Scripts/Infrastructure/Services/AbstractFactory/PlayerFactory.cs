using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Logic;
using Player;
using Player.MouseInput;
using Player.MovementSystem;
using Player.NewWeaponControllSystem;
using Unity.VisualScripting;
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
            GameObject playerObject = _objAssert.Assert(playerConfigs.PathToPlayer, position);
            Player.Player player = playerObject.AddComponent<Player.Player>();
            player.Construct(playerConfigs.Speed);
            
            Transform visual = playerObject.transform.Find(ConstantsSceneObjects.PREFAB_MESH_COMPONENT_NAME);
            PlayerAnimator playerAnimator = visual.AddComponent<PlayerAnimator>();
            playerAnimator.Construct();
        
            Transform physic = player.transform.Find(ConstantsSceneObjects.PREFAB_PHYSIC_COMPONENT_NAME);
            PlayLoopComponentProvider playLoopComponentProvider = physic.GetComponent<PlayLoopComponentProvider>();
        
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(Transform), player.transform);
            playerObject.AddComponent<CameraFollower>().Construct(camera, player);
            MouseRotateController rotateController = playerObject.AddComponent<MouseRotateController>();
            rotateController.Construct(camera, player);
            
            IInputSystem axisInputSystem = playerObject.AddComponent<AxisInputSystem>();
            playerObject.AddComponent<MoveController>().Construct(player, axisInputSystem);
            axisInputSystem.AddSelfBlockList();
            
            WeaponSwitcher switcher = playerObject.AddComponent<WeaponSwitcher>();
            playerObject.AddComponent<WeaponController>();
            WeaponProvider provider = playerObject.transform.GetComponentInChildren<WeaponProvider>();
            _weaponFactory.CreateAll(provider.ReturnWeapons());
            ServiceLocator.ServiceLocator.Instance.CleanData(typeof(Transform));
        
            WeaponController controller = playerObject.GetComponent<WeaponController>();
            CurrentWeaponConstructor currentWeaponConstructor = new CurrentWeaponConstructor(controller);
            switcher.Construct(provider, currentWeaponConstructor);
            controller.Construct(provider.ReturnWeapons(), switcher);
        
            PlayerHealth playerHealth = playerObject.AddComponent<PlayerHealth>();
            playerHealth.Construct(playerConfigs.Health);

            PlayerDeath playerDeath = playerObject.AddComponent<PlayerDeath>();
            playerDeath.Construct(playerHealth, playerAnimator);
            
            playLoopComponentProvider.AddToProvideComponent(playerHealth);
            //player.AddBlockList(axisInputSystem);
            player.AddBlockList(switcher);
            player.AddBlockList(rotateController);
            //player.AddBlockList(controller);
            return playerObject;
        }
    }
}