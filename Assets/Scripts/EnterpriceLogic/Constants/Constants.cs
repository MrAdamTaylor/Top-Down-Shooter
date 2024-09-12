using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    #region Список уровней

    public const string SECOND_LEVEL = "GameScene";
    public const string First_LEVEL = "StartScene";

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
    public static float DEFAULT_BULLET_SPEED;
    public static string INITIAL_POSITION = "InitialPoint";

    #endregion

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

    #endregion
    

    #region Константы путей к префабам

    public const string PLAYER_PATH = "Prefabs/PlayerEmpty";

    #endregion
}
