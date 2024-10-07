using System;
using System.Collections.Generic;
using EnterpriceLogic.Constants;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Kimikaze")]
public class EnemyKamikazeConfigs : EnemyWalkingConfigs
{
    public List<ExplosionRadiusParams> ExplosionRadius;

    public void OnValidate()
    {
        if (ExplosionRadius.Count > Constants.MAXIMUM_KAMIKAZE_EXPLOSION_LEVEL)
        {
            ExplosionRadius.RemoveRange(Constants.MAXIMUM_KAMIKAZE_EXPLOSION_LEVEL-1, 
                ExplosionRadius.Count-Constants.MAXIMUM_KAMIKAZE_EXPLOSION_LEVEL);
        }

        if (ExplosionRadius.Count > 1)
        {
            for (int i = 0; i < ExplosionRadius.Count-1; i++)
            {
                float explosionRadiusParams = ExplosionRadius[i+1].WaweRadius;
                if (explosionRadiusParams < ExplosionRadius[i].WaweRadius)
                {
                    ExplosionRadius[i + 1] = new ExplosionRadiusParams(ExplosionRadius[i].WaweRadius + 1, ExplosionRadius[i+1].ExplositonDamage);
                }
            }
        }
    }
}

[Serializable]
public struct ExplosionRadiusParams
{
    public float WaweRadius;
    public int ExplositonDamage;
    
    public ExplosionRadiusParams(float radius, int damage)
    {
        WaweRadius = radius;
        ExplositonDamage = damage;
    }
}