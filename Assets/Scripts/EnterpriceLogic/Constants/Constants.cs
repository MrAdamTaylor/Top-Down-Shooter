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
        public static Vector3 CAMERA_POSITION = new Vector3(0,13,-3);
        #endregion

        #region Weapon Constants
        public const float DEFAULT_BULLET_SPEED = 100f;
        public const float DEFAULT_BULLET_DISTANCE = 40f;
        public static LayerMask WEAPON_LAYER_MASK = LayerMask.GetMask("Enemy","Obstacle");
        
        public const long STANDART_WASTE_WALUE = 1L;
        public const bool MADE_IMPACT = true;
        public const bool NON_MADE_IMPACT = false;
        public const float RAY_SHOOT_DURATION = 5f;
        public const int HALF = 2;
        public const int QUARTER = 4;
        public const int THIRD_FRACTION = 3;
        public const int FIVE_FRACTION = 5;

        #endregion

        #region UI Animation Constants
        public const int UI_ELEMENT_VIBRATION = 10;
        public const long STANDART_UI_VALUE = 10;
        #endregion

        #region TempValue
        public static Vector3 DEFAULT_VECTOR_FOR_TEST2 = new Vector3(-25f, -28f, 25f);
        #endregion


        #region RotateSystem
        public const int STANDART_CLOCKWISE_VALUE = 1;
        public const float POSIBLE_ROTATION_ANGLE_DEVIANT = 10f;
        public const float ROTATE_SPEED = 10f;
        #endregion


        public const float SPAWN_POINT_DEVIATION = 5f;
        public static float DEBUG_RILLRATE_TIME = 1f;
        public static float EPSILON_BETWEEN_RDETECTION_MINDISTANCE = 0.5f;
        public static string TIMER_NAME = "[TIME INVOKER]";


        #region GameObjNames
        public const string PREFABS_SCENE_GAMEOBJECT_PARENT_NAME = "[GAME_OBJECTS]";
        public const string PREFAB_MESH_COMPONENT_NAME = "[VISUAL]";
        public const string PREFAB_PHYSIC_COMPONENT_NAME = "[PHYSICS]";
        public const string PREFAB_SCENE_DEBUG_COMPONENT = "[DEBUG]";
        public const string INITIAL_POSITION = "InitialPoint";
        public const string FRACTION_TRASH = "FractionTrash";
        public const string CANVAS_TAG = "PlayerUI";
        #endregion
        

        #region Animation
        public const int MINIMAL_ATTACK_ANIMATION = 1;
        public const int MAXIMUM_ATTACK_ANIMATION = 3;
        public const float ENEMY_ANIMATION_SPEED = 1.5f;
        #endregion
    }
}