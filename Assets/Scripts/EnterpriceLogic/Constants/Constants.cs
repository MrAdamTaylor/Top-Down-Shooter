using UnityEngine;

public static class Constants
{
    #region Список уровней

    public const string SECOND_LEVEL = "Level2";
    //public static string THIRD_LEVEL = "Level3";
    public const string LEVEL_THREE = "Level3";
    public const string STARTER_SCENE = "StartScene";
    public const string FIRST_LEVEL = "GameScene";

    #endregion


    #region Характеристики игроков

    public const float NPC_SPEED_MULTIPLYER = 3f;

    #endregion


    #region Константы для зон

    public const float STANDART_VALUE_FOR_SPEED = 0f;

    #endregion

    #region Константы для оружия

    public const float DEFAULT_MAXIMUM_FIRING_RANGE = 40f;
    public const float ROCKET_SPEED = 30f;
    public static float DEFAULT_BULLET_SPEED = 100f;
    public static float DEFAULT_BULLET_DISTANCE = 40f;
    public static string INITIAL_POSITION = "InitialPoint";
    public static LayerMask WEAPON_LAYER_MASK = LayerMask.GetMask("Enemy","Obstacle");

    #endregion

    public static long STANDART_UI_VALUE = 10;


    #region Константы для ограничения области карты

    public const float XBORDER_MAX = 20f;
    public const float ZBORDER_MAX = 15f;
    public const float STANDART_Y_POSITION = 1f;

    #endregion

    #region Показатели состояний зон дебафов

    public const float MAX_DEBUF_STAGE_LEVEL = 100;
    public const float MIN_DEBUF_STAGE_LEVEL = 0f;

    #endregion

    #region Weapon Constants

    public const long STANDART_WASTE_WALUE = 1L;
    public const bool SWITCH_WEAPON_MODE = true;
    public const bool ONE_WEAPON_MODE = false;

    #endregion


    #region Константы путей к префабам

    //public const string PLAYER_PATH = "Prefabs/PlayerEmpty";
    public const string PLAYER_PATH_FIRST = "Prefabs/PlayerEmptyFull";
    public const string HOT_TRAIL_PATH = "Prefabs/WeaponsPrefabs/HotTrail";
    public const string IMPACT_PARTICLE_EFFECT = "Prefabs/WeaponsPrefabs/SparksEffect";
    public static string LINE_RENDERER_PATH = "Prefabs/WeaponsPrefabs/LaserShootgun";
    public static string MUZZLE_FLASH_WEAPON_PATH = "Prefabs/WeaponsPrefabs/MuzzleFlash";

    public static string WEAPON_ICO_PATH = "StaticData/UI/UIWeaponIcons";
    public static string DEFAULT_WEAPON_AMMO_TEXT = "Infinity";

    public static string WEAPON_POINTSHOOT_NAME = "Point";
    public static bool MADE_IMPACT = true;
    public static bool NON_MADE_IMPACT = false;
    public static object UI_PLAYER_PATH = "Prefabs/UI/PlayerHudContent";

    #endregion

    public static float SCREEN_OVERLAY_WIDTH = 1920f;
    public static float SCREEN_OVERLAY_HEIGHT = 1080f;
    public static Vector3 CAMERA_POSITION = new Vector3(0,12,-5);
    public static string PLAYER_PATH_SECOND = "Prefabs/Player2";
}
