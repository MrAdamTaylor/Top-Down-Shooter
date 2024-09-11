using System.Collections.Generic;
using UnityEngine;

public class AmmoProvider : MonoBehaviour
{
     [SerializeField] private List<AmmoController> _ammoControllers;

     public int Count
     {
          get;
          private set;
     }

     private int _count;

     public void Construct()
     {
          
          for (int i = 0; i < _ammoControllers.Count; i++)
          {
               _ammoControllers[i].Construct();
          }
          Count = _ammoControllers.Count;
     }

     public (Weapon, AmmoStorage) GetTypeStorageCortege(int i)
     {
          (Weapon, AmmoStorage) result = (_ammoControllers[i].GetWeaponType(), _ammoControllers[i].GetStorage());
          return result;
     }
}
