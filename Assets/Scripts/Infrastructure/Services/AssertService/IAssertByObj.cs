using UnityEngine;

namespace Infrastructure.Services.AssertService
{
    public interface IAssertByObj<T> : IAssert<T, T> where T : Object 
    {
        public new T Assert(T objPath);

        public new T Assert(T objPath, Vector3 pos);
        
        public new T Assert(T objPath, Vector3 pos, Quaternion quaternion);
        
        public new T Assert(T objPath, Vector3 pos,Transform parent);
        
        public new T Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }
}