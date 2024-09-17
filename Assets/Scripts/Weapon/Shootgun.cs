using UnityEngine;

public class Shootgun : Weapon
{
    [Range(3,5)]public int AmountOfFraction;
    [Range(0,360)]public float FovAngle;
    [Range(0,40f)]public float Distance;

    public override void Construct(ShootControlSystem shootControlSystem,WeaponStaticData data)
    {
        base.Construct(shootControlSystem, data);
    }
    
    /*public override void Awake()
    {
        ShootDataShootgun data =
            new ShootDataShootgun(Damage, ShootPoint, _speed_fire_range, FovAngle, Distance, AmountOfFraction);
        _shootSystem.ConstructShootSystem(data);
    }*/

    public void OnValidate()
    {
        if (AmountOfFraction % 2 == 0)
        {
            AmountOfFraction = AmountOfFraction - 1;
        }
    }
}