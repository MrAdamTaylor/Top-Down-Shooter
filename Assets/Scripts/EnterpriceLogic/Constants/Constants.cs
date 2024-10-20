using UnityEngine;

namespace EnterpriceLogic.Constants
{
    public static class Constants
    {
        #region Enemy Characteristics
        public const float NPC_SPEED_MULTIPLYER = 3f;
        public const int MAXIMUM_KAMIKAZE_EXPLOSION_LEVEL = 3;
        public const float MAX_DOT_PRODUCT_VALUE = 1f;
        public static string ENEMY_LAYER = "Enemy";
        #endregion


        #region Player Characteristics
        public static Vector3 CAMERA_POSITION = new Vector3(0,12,-5);
        //public static float PLAYER_HP = 100f;
        #endregion

        #region Weapon Constants
        public static float DEFAULT_BULLET_SPEED = 100f;
        public static float DEFAULT_BULLET_DISTANCE = 40f;
        public static LayerMask WEAPON_LAYER_MASK = LayerMask.GetMask("Enemy","Obstacle");
        public static string WEAPON_DATA_PATH = "StaticData/WeaponData";
        public const long STANDART_WASTE_WALUE = 1L;
        public static bool MADE_IMPACT = true;
        public static bool NON_MADE_IMPACT = false;
        public static float RAY_SHOOT_DURATION = 5f;
        public static int HALF = 2;
        public static int QUARTER = 4;
        public const int THIRD_ANGLE = 3;
        public const int FIVE_ANGLE = 5;

        #endregion

        #region UI Animation Constants
        public const int UI_ELEMENT_VIBRATION = 10;
        public const long STANDART_UI_VALUE = 10;
        #endregion


        #region TempValue
        public static Vector3 DEFAULT_VECTOR_FOR_TEST2 = new Vector3(-25f, -28f, 25f);
        #endregion


        #region RotateSystem
        public static int STANDART_CLOCKWISE_VALUE = 1;
        public static float POSIBLE_ROTATION_ANGLE_DEVIANT = 10f;
        public static float ROTATE_SPEED = 10f;
        #endregion


        public const float SPAWN_POINT_DEVIATION = 5f;
        public static float DEBUG_RILLRATE_TIME = 1f;
        public static float EPSILON_BETWEEN_RDETECTION_MINDISTANCE = 0.5f;


        #region GameObjNames
        public static string SCENE_PARENT_NAME = "[GAME_OBJECTS]";
        public static string PREFAB_MESH_COMPONENT_NAME = "[VISUAL]";
        public static string PREFAB_PHYSIC_COMPONENT_NAME = "[PHYSICS]";
        public static string INITIAL_POSITION = "InitialPoint";
        #endregion
        

        #region Animation
        public static int MINIMAL_ATTACK_ANIMATION = 1;
        public static int MAXIMUM_ATTACK_ANIMATION = 3;
        public static float ENEMY_ANIMATION_SPEED = 1.5f;
        #endregion

        #region HitBox
        public static float ATTACK_COOLDOWN = 1f;
        public static float SHIFTED_DISTANCE = 0.5f;
        public static float CLEAVE_RADIUS = 0.8f;
        public static string FRACTION_TRASH = "FractionTrash";
        public static string CANVAS_TAG = "PlayerUI";

        #endregion
        
        
    }
}