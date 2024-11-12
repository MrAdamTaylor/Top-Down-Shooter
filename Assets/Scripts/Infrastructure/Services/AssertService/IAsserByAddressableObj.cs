using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssertService
{
    public interface IAsserByAddressableObj<TGameObject,T> : IAsyncAssert<TGameObject, T> where T : AssetReferenceGameObject
    {
        public new Task<TGameObject> Assert(T objPath);
        
        public new Task<TGameObject> Assert(T objPath, Vector3 pos);
        
        public new Task<TGameObject> Assert(T objPath, Vector3 pos, Quaternion quaternion);
        
        public new Task<TGameObject> Assert(T objPath, Vector3 pos,Transform parent);
        
        public new Task<TGameObject> Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }
}