using System.Collections.Generic;
using UnityEngine;

public class EnemyConfigs : ScriptableObject
{
    public string Name;
    public int Health;
    
    public int MinDamage;
    public int MaxDamage;

    public int Reward;

    public float RadiusDetection;
    
    public List<GameObject> Skins;
}