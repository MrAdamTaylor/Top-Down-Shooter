using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType TypeWeapon;
    
    [SerializeField] public Transform ShootPoint;

    [SerializeField] private Vector3 _vectorShift;
    [SerializeField] private Vector3 _gunDirection;
    private ShootControlSystem _shootSystem;

    [SerializeField] private WeaponCharacteristics Characteristics;
    
    public virtual void Construct(ShootControlSystem shootControlSystem, WeaponStaticData data)
    {
        _shootSystem = shootControlSystem;
        Characteristics.Damage = data.Damage;
        Characteristics.FireSpeedRange = data.SpeedFireRange;
    }

    public void Fire()
    {
        _shootSystem.UpdateValues(Characteristics);
        _shootSystem.Shoot();
    }
    
    public void OnDrawGizmos()
    {
        Vector3 position = transform.TransformPoint(_vectorShift);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(position, _gunDirection);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(position + _gunDirection, 0.05f);
    }

    public Vector3 GetShootPosition()
    {
        Matrix4x4 matrix = transform.localToWorldMatrix;
        Vector3 position = matrix.GetPosition();
        position = position + transform.forward;
        return position;
    }
}

[Serializable]
public struct WeaponCharacteristics
{
    public int Damage;
    public float FireSpeedRange;
}
