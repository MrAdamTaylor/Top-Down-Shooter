using UnityEngine;

public class ShootDataShootgun : ShootData
{
    public float FovAngle;
    public float Distance;
    public int AmountFractions;
    
    public ShootDataShootgun(int damage, Transform shootPoint, float speedFireRange, 
        float fovAngle, float distance, int amountFraction) 
        : base(damage, shootPoint, speedFireRange)
    {
        Damage = damage;
        BulletPoint = shootPoint;
        Delay = speedFireRange;
        FovAngle = fovAngle;
        Distance = distance;
        AmountFractions = amountFraction;

    }
}