using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;


namespace Infrastructure.Services.AssertService
{
    public class AssertServiceAddressableObj<TGameObject,T> : IAsserByAddressableObj<TGameObject,T> where T : AssetReferenceGameObject
    {
        private IAsserByAddressableObj<TGameObject, T> _asserByAddressableObjImplementation;

        public async Task<TGameObject> Assert(T gameObject)
        {
            if (gameObject is AssetReferenceGameObject addressable)
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressable);
      
                GameObject prefab = await handle.Task;
                
                GameObject monster = Object.Instantiate(prefab);

                return (TGameObject)Convert.ChangeType(monster, typeof(TGameObject));
            }
            else
            {
                throw new Exception("Error cast in AssertServiceAddressableObj");
            }
        }
        
        public Task<TGameObject> Assert(T objPath, Vector3 pos)
        {
            throw new NotImplementedException();
        }
        
        public Task<TGameObject> Assert(T objPath, Vector3 pos, Quaternion quaternion)
        {
            throw new NotImplementedException();
        }
        
        public async Task<TGameObject> Assert(T objPath, Vector3 pos, Transform parent)
        {
            if (objPath is AssetReferenceGameObject addressable)
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressable);
      
                GameObject prefab = await handle.Task;
                
                GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

                return (TGameObject)Convert.ChangeType(monster, typeof(TGameObject));
            }
            else
            {
                throw new Exception("Error cast in AssertServiceAddressableObj");
            }
        }
        
        public Task<TGameObject> Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent)
        {
            throw new NotImplementedException();
        }
    }
}