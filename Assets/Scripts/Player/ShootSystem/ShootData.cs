using UnityEngine;

public class ShootData
{
    public int Damage;
    public Transform BulletPoint;
    public float Delay;

    public ShootData(int damage, Transform shootPoint, float speedFireRange)
    {
        Damage = damage;
        BulletPoint = shootPoint;
        Delay = speedFireRange;
    }
}