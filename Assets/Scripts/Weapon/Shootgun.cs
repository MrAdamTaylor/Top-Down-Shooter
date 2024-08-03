using UnityEngine;

public class Shootgun : Weapon
{
    [Range(3,7)]public int AmountOfFraction;
    [Range(0,360)]public float FovAngle;
    [Range(0,40f)]public float Distance;

    public void OnValidate()
    {
        if (AmountOfFraction % 2 == 0)
        {
            AmountOfFraction = AmountOfFraction - 1;
        }
    }
}