using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class PlayerConfigs : ScriptableObject
{
    public string PathToPlayer;

    public float Speed;

    public int Health;
}