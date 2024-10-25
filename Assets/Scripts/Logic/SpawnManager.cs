using System.Collections.Generic;
using UnityEngine;

public class SpawnManager
{
    private List<string> _accessPools;
    private float _spawnCooldown;
    private int _count;
    
    public bool CanSpawn(string name)
    {
        bool canSpawn = false;
        for (int i = 0; i < _accessPools.Count; i++)
        {
            if (name == _accessPools[i])
            {
                canSpawn = true;
            }
        }
        if (canSpawn == false)
        {
            Debug.Log($"<color=red>Not Access for this Enemie </color> {name}");
        }
        return canSpawn;
    }
    
    public void Configure(float cooldown, List<string> accessPools)
    {
        _spawnCooldown = cooldown;
        _accessPools = accessPools;
    }
}