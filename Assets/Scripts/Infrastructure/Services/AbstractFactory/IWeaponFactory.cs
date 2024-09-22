using UnityEngine;

public interface IWeaponFactory : IGameFactory
{
    public void CreateWeapons(Weapon[] weapon);
}