using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class LevelConfigs : ScriptableObject
{
    public string LevelName;

    public List<SpawnerConfigs> SpawnerConfigsList;

    public PlayerConfigs PlayerConfigs;
    
    [Space]
    [Header("Level Tasks")] 
    [Space] 
    
    public bool WalkToEnd;

    public bool KillAllMobs;
    public int MobsCount;

    public bool IsTime;
    public int PerSeconds;

    public bool DestroyTower;
    public int TowerHealth;
        
}