using UnityEngine;


public class CoomoonShootSystem : MonoBehaviour, IShootSystem
{
    protected Transform _directionObject;
    protected LayerMask _layerMask;

     public virtual void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
     {
     }

     public virtual void Shoot()
     {
        
     }

     protected Vector3 GetDirection()
    {
        Vector3 direction = _directionObject.transform.forward;
        return direction;
    }

    public virtual void UpdateValues(WeaponCharacteristics characteristics)
    {
        
    }
}
