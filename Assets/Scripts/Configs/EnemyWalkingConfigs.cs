using EnterpriceLogic.Constants;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Walking")]
    public class EnemyWalkingConfigs : EnemyConfigs
    {
        public float Speed;
        [HideInInspector] public float MinimalToPlayerDistance;

        private void OnValidate()
        {
            MinimalToPlayerDistance = RadiusDetection - Constants.EPSILON_BETWEEN_RDETECTION_MINDISTANCE;
        }
    }
}