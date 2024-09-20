using UnityEngine;

public interface IWeaponFactory : IGameFactory
{
    public void CreateWeapons(Weapon[] weapon, Transform playerTransform);
    void Construct();
}