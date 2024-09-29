using EnterpriceLogic.Constants;
using Scripts.Player.NewWeaponControllSystem;
using UnityEngine;

public class PlayerFactory : IPlayerFactory
{
    private readonly IAsserts _asserts;
    private readonly IWeaponFactory _weaponFactory;
    
    public PlayerFactory(IAsserts assets, IWeaponFactory weaponFactory)
    {
        _asserts = assets;
        _weaponFactory = weaponFactory;
    }

    public GameObject CreatePlayer(Vector3 position, Camera camera)
    {
        GameParams gameParams = (GameParams)ServiceLocator.Instance.GetData(typeof(GameParams));
        string playerName = "";
        if (gameParams.EPlayer == PlayerType.Player1)
        {
            playerName = PrefabPath.PLAYER_PATH_FIRST;
        }
        else
        {
            playerName = PrefabPath.PLAYER_PATH_SECOND;
        }

        GameObject gameObject = _asserts.Instantiate(playerName, position);
        Player player = gameObject.AddComponent<Player>();
        ServiceLocator.Instance.BindData(typeof(Transform), player.transform);
        gameObject.AddComponent<CameraFollower>().Construct(camera, player);
        gameObject.AddComponent<MouseRotateController>().Construct(camera, player);
        gameObject.AddComponent<AxisInputSystem>();
        IInputSystem system = gameObject.GetComponent<AxisInputSystem>();
        gameObject.AddComponent<MoveController>().Construct(player, system);
        bool flag = Constants.SWITCH_WEAPON_MODE;
        if (flag)
        {
            gameObject.AddComponent<WeaponSwitcher>();
            gameObject.AddComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            WeaponProvider provider = gameObject.transform.GetComponentInChildren<WeaponProvider>();
            _weaponFactory.CreateWeapons(provider.ReturnWeapons());
            ServiceLocator.Instance.CleanData(typeof(Transform));
            WeaponSwitcher switcher = gameObject.GetComponent<WeaponSwitcher>();
            Scripts.Player.NewWeaponControllSystem.WeaponController controller =
                gameObject.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            CurrentWeaponConstructor currentWeaponConstructor = new CurrentWeaponConstructor(controller);
            switcher.Construct(provider, currentWeaponConstructor);
            controller.Construct(provider.ReturnWeapons(), switcher);
        }
        else
        {
            gameObject.AddComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            WeaponProvider provider = gameObject.transform.GetComponentInChildren<WeaponProvider>();
            Scripts.Player.NewWeaponControllSystem.WeaponController controller =
                gameObject.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>();
            controller.Construct(provider.GetActiveWeapon());
        }

        return gameObject;
    }
}