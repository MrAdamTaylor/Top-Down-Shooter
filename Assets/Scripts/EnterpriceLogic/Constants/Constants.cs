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

        #endregion

        #region UI Animation Constants
        public const int UI_ELEMENT_VIBRATION = 10;
        public const long STANDART_UI_VALUE = 10;
        #endregion
        
        public static float DEBUG_RILLRATE_TIME = 1f;
        public static float EPSILON_BETWEEN_RDETECTION_MINDISTANCE = 0.5f;
        public const string INTERMEDIATE_SCENE = "Intermediate_Scene";
        public const string MAIN_MENU_SCENE = "StartScene";


        #region Animation
        public const int MINIMAL_ATTACK_ANIMATION = 1;
        public const int MAXIMUM_ATTACK_ANIMATION = 3;
        public const float ENEMY_ANIMATION_SPEED = 1.5f;
       
        public const string POOL_PREFIX = " Pool";

        #endregion
    }
}