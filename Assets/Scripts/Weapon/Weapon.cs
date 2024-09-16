using UnityEngine;

public class Weapon : MonoBehaviour
{
    //TODO - понадобится для будущего рефактора
    public WeaponType TypeWeapon;
    
    public int Damage;
    [SerializeField] public Transform ShootPoint;
    [SerializeField] protected float _speed_fire_range;

    [SerializeField] protected ShootControlSystem _shootSystem;

    public virtual void Construct(ShootControlSystem shootControlSystem)
    {
        _shootSystem = shootControlSystem;
    }

    /*public virtual void Awake()
    {
        ShootData data = new ShootData(Damage, ShootPoint, _speed_fire_range);
        _shootSystem.ConstructShootSystem(data);
    }*/

    public void Fire()
    {
        _shootSystem.Shoot();
    }
}
