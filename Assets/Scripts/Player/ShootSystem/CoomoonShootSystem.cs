using UnityEngine;


public class CoomoonShootSystem : MonoBehaviour, IShootSystem
{
    //[SerializeField] 
    protected Transform _directionObject;
    //[SerializeField] 
    protected LayerMask _layerMask;

     //protected ShootData _weaponData;


     public virtual void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
     {
         //Debug.Log();
     }


     /*public virtual void Construct(ShootData data)
     {
         _weaponData = data;
     }*/

     public virtual void Shoot()
    {
        
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = _directionObject.transform.forward;
        return direction;
    }


    public virtual void UpdateValues(WeaponCharacteristics characteristics)
    {
        
    }
}
