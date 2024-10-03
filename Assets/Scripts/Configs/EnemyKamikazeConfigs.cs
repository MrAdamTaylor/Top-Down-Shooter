using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Kimikaze")]
public class EnemyKamikazeConfigs : EnemyWalkingConfigs
{
    public List<ExplosionRadiusParams> ExplosionRadius;

    public void OnValidate()
    {
        if (ExplosionRadius.Count > 3)
        {
            ExplosionRadius.RemoveRange(3-1, ExplosionRadius.Count-3);
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