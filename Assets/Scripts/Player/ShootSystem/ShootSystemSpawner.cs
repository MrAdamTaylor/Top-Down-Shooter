using UnityEngine;

public class ShootSystemSpawner : CoomoonShootSystem
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _distance;

    public override void Shoot()
    {
        Debug.Log("Выстрел из базуки");
    }
}