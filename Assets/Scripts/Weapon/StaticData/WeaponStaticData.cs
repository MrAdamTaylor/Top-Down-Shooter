using UnityEngine;

[CreateAssetMenu(fileName = "StandartWeaponConfig", menuName = "Configs/Weapon/Bullet")]
public class WeaponStaticData : ScriptableObject
{
    public WeaponType WType;
    public int Damage;
    public float SpeedFireRange;
    public WeaponInputType InpType;

    [Header("Эффект вспышки при стрельбе")]
    public bool IsMuzzle;

    [Header("Ограниченные патроны")] 
    public bool IsAmmo;

    [HideInInspector]public Transform BulletPoint;
}