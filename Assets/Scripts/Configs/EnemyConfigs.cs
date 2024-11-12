using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
    public class EnemyConfigs : ScriptableObject
    {
        public string Name;
        public int Health;
    
        public int MinDamage;
        public int MaxDamage;

        public int Reward;

        public float RadiusDetection;
        [Header("HitBox - Radius")]
        [Range(1.5f, 5f)]
        public float HitBoxRadius;
    
        public List<GameObject> Skins;

        public List<AssetReferenceGameObject> SkinsReference;

        private void OnValidate()
        {
            if (Mathf.Approximately(HitBoxRadius, 0f))
            {
                HitBoxRadius = 1.5f;
            }
        }
    }
}