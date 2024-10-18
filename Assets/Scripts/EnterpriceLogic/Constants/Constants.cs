using UnityEngine;

namespace EnterpriceLogic.Constants
{
    public static class Constants
    {
        #region Level list

        public const string SECOND_LEVEL = "Level2";
        public const string LEVEL_THREE = "Level3";
        public const string STARTER_SCENE = "StartScene";
        public const string FIRST_LEVEL = "GameScene";

        #endregion


        #region Enemy Characteristics

        public const float NPC_SPEED_MULTIPLYER = 3f;
        public const float DELTA_VALUE_FOR_NPC_ROTATE_SYSTEM = 10;
        public const int MAXIMUM_KAMIKAZE_EXPLOSION_LEVEL = 3;

        public const float MAX_DOT_PRODUCT_VALUE = 1f;
        #endregion


        #region Player Characteristics

        public static Vector3 CAMERA_POSITION = new Vector3(0,12,-5);


        #endregion


        #region Baf Characteristics

        public const float STANDART_VALUE_FOR_SPEED = 0f;

        #endregion

        #region Weapon Constants

        public const float DEFAULT_MAXIMUM_FIRING_RANGE = 40f;
        public const float ROCKET_SPEED = 30f;
        public static float DEFAULT_BULLET_SPEED = 100f;
        public static float DEFAULT_BULLET_DISTANCE = 40f;
        public static string INITIAL_POSITION = "InitialPoint";
        public static LayerMask WEAPON_LAYER_MASK = LayerMask.GetMask("Enemy","Obstacle");
        public const long STANDART_WASTE_WALUE = 1L;
        public const bool SWITCH_WEAPON_MODE = true;
        public const bool ONE_WEAPON_MODE = false;
        public static bool MADE_IMPACT = true;
        public static bool NON_MADE_IMPACT = false;

        #endregion

    


        #region MapBorder

        public const float XBORDER_MAX = 20f;
        public const float ZBORDER_MAX = 15f;
        public const float STANDART_Y_POSITION = 1f;

        #endregion

        #region DebafState

        public const float MAX_DEBUF_STAGE_LEVEL = 100;
        public const float MIN_DEBUF_STAGE_LEVEL = 0f;

        #endregion

        

        #region UI Constants

        public const float SCREEN_OVERLAY_WIDTH = 1920f;
        public const float SCREEN_OVERLAY_HEIGHT = 1080f;
        public const int UI_ELEMENT_VIBRATION = 10;
        public const long STANDART_UI_VALUE = 10;
        #endregion
        
        public static string SCENE_PARENT_NAME = "[GAME_OBJECTS]";
        
        public static Vector3 DEFAULT_VECTOR_FOR_TESTS = new Vector3(0, 1.5f,0);
        public static Vector3 DEFAULT_VECTOR_FOR_TEST2 = new Vector3(-25f, -28f, 25f);
        public static float MOVEMENT_THRESHOLD = 0.8f;
        public static string PREFAB_MESH_COMPONENT_NAME = "[VISUAL]";
        public static int STANDART_CLOCKWISE_VALUE = 1;
        public static float POSIBLE_ROTATION_ANGLE_DEVIANT = 10f;
        public static float ROTATE_SPEED = 10f;
        public const float SPAWN_INTERVAL = 5f;

        public static int MINIMAL_ATTACK_ANIMATION = 1;
        public static int MAXIMUM_ATTACK_ANIMATION = 3;
        public static float DEBUG_TIME_FRAMERATE = 1f;
        public static float EPSILON_BETWEEN_RDETECTION_MINDISTANCE = 0.5f;
        public static float ENEMY_ANIMATION_SPEED = 1.5f;
        public static float PLAYER_HP = 100f;
        public static string PREFAB_PHYSIC_COMPONENT_NAME = "[PHYSICS]";
    }
}