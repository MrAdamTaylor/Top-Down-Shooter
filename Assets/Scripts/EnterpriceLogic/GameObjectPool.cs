using System;
using System.Threading.Tasks;
using UnityEngine;

namespace EnterpriceLogic
{
    public class GameObjectPool : PoolBase<GameObject>
    {
    
        public GameObjectPool(Func<GameObject> func, int preloadCount)
            :base(()=> NonCreatePreload(func),GetAction, ReturnAction, preloadCount) {}
        
        /*public GameObjectPool(Func<Task<GameObject>> func, int preloadCount)
            :base(()=> NonCreatePreload(func),GetAction, ReturnAction, preloadCount) {}*/

        /*private GameObject NonCreatePreload(Func<Task<GameObject>> callbackOnCreated)
        {
            Task<GameObject> prefab = callbackOnCreated.Invoke();

            
            return obj;
        }*/

        private static GameObject NonCreatePreload(Func<GameObject> func)
        {
            GameObject prefab = func.Invoke();
            return prefab;
        }
    

        private static void GetAction(GameObject gObject) => gObject.SetActive(true);
    
        private static void ReturnAction(GameObject gObject) => gObject.SetActive(false);
    }
}