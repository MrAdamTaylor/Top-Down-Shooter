using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssertService
{
    public class AssertBuilder
    {
        public IAssertByObj<T> BuildAssertServiceByObj<T>() where T : Object
        {
            return new AssertServiceObj<T>();
        }

        public IAssertByString<T> BuildAssertServiceByString<T>() where T : Object
        {
            return new AssertServiceString<T>();
        }
    
        public AssertLoader<T> LoadService<T>() where T : Object
        {
            return new AssertLoader<T>();
        }

        public AssertServiceAddressableObj<GameObject,T> BuildAssertServiceByAddressable<T>() where T : AssetReferenceGameObject
        {
            return new AssertServiceAddressableObj<GameObject,T>();
        }
    }
}