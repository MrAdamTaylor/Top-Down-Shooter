
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService.ExtendetAssertService;
using Scripts.Player.NewWeaponControllSystem;
using UnityEngine;

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
        PlayerConfigs playerConfigs = (PlayerConfigs)ServiceLocator.Instance.GetData(typeof(PlayerConfigs));
        GameObject gameObject = _objAssert.Assert(playerConfigs.PathToPlayer, position);
        Player player = gameObject.AddComponent<Player>();
        Transform physic = player.transform.Find(Constants.PREFAB_PHYSIC_COMPONENT_NAME);
        PlayLoopComponentProvider playLoopComponentProvider = physic.GetComponent<PlayLoopComponentProvider>();
        
        ServiceLocator.Instance.BindData(typeof(Transform), player.transform);
        gameObject.AddComponent<CameraFollower>().Construct(camera, player);
        gameObject.AddComponent<MouseRotateController>().Construct(camera, player);
        gameObject.AddComponent<AxisInputSystem>();
        IInputSystem system = gameObject.GetComponent<AxisInputSystem>();
        gameObject.AddComponent<MoveController>().Construct(player, system);
            gameObject.AddComponent<WeaponSwitcher>();
            gameObject.AddComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            WeaponProvider provider = gameObject.transform.GetComponentInChildren<WeaponProvider>();
            _weaponFactory.CreateAll(provider.ReturnWeapons());
            ServiceLocator.Instance.CleanData(typeof(Transform));
            WeaponSwitcher switcher = gameObject.GetComponent<WeaponSwitcher>();
            Scripts.Player.NewWeaponControllSystem.WeaponController controller =
                gameObject.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            CurrentWeaponConstructor currentWeaponConstructor = new CurrentWeaponConstructor(controller);
            switcher.Construct(provider, currentWeaponConstructor);
            controller.Construct(provider.ReturnWeapons(), switcher);
            PlayableHealth playableHealth = gameObject.AddComponent<PlayableHealth>();
            playLoopComponentProvider.AddToProvideComponent(playableHealth);
        return gameObject;
    }
}