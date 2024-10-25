using System;
using EnterpriceLogic.Constants;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Turret")]
    public class EnemyTurretConfigs : EnemyConfigs
    {
        public TurretAmmoType TurretAmmo;
        public float RillRate;
        public TurretSensorCharacteristics SensorCharacteristics;
    }

    [Serializable]
    public struct TurretSensorCharacteristics
    {
        public float TurretRotateSpeed;
        [Range(-Constants.MAX_DOT_PRODUCT_VALUE, Constants.MAX_DOT_PRODUCT_VALUE)] public float TurningRange;
        public float DetectionHeight;
    }

    public enum TurretAmmoType
    {
        Rocket,
        Fraction,
        Bullet
    }
}