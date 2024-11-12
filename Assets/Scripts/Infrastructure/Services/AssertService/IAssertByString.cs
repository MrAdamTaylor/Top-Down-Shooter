using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssertService
{
    public interface IAssertByString<out T> : IAssert<T,string> where T : Object
    {
        public new T Assert(string objPath);

        public new T Assert(string objPath, Vector3 pos);
        
        public new T Assert(string objPath, Vector3 pos, Quaternion quaternion);
        
        public new T Assert(string objPath, Vector3 pos,Transform parent);
        
        public new T Assert(string objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }
}