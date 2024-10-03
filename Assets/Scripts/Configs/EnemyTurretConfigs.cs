using System;
using UnityEngine;

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
    [Range(-1, 1)] public float TurningRange;
    public float DetectionHeight;
}

public enum TurretAmmoType
{
    Rocket,
    Fraction,
    Bullet
}