using UnityEngine;

namespace Weapon.StaticData
{
    [CreateAssetMenu(fileName = "StandartWeaponConfig", menuName = "Configs/Weapon/Bullet")]
    public class WeaponStaticData : ScriptableObject
    {
        public WeaponType WType;
        public int Damage;
        public float SpeedFireRange;
        public WeaponInputType InpType;
        public AudioClip [] ShootSound;

        [Header("Add Flash Effect")]
        public bool IsMuzzle;

        [Header("Add Ammo Component")] 
        public bool IsAmmo;

        [Header("Working if Ammo true")] 
        public AmmoCharacteristics AmmoValues;

        [HideInInspector]public Transform BulletPoint;
        [HideInInspector] public Vector3 ShootPosition;
    }


    [System.Serializable]
    public struct AmmoCharacteristics
    {
        public bool IsInfinity;

        [Header("Number of cartiges at game start")]
        public long StarterAmmo;
    
        [Header("Сartridge consumption per shot")]
        public long WastedAmmo;
    
    }
}