using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonShootSystem : MonoBehaviour
{
    [SerializeField] protected Transform _directionObject;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected LayerMask _layerMask;

    protected ShootData _weaponData;
    
    public virtual void Construct(ShootData data)
    {
        _weaponData = data;
    }

    public virtual void Shoot()
    {
        //Debug.Log("Make Shoot");
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = _directionObject.transform.forward;
        return direction;
    }

    public virtual void PlayShoot()
    {
        throw new System.NotImplementedException();
    }
}