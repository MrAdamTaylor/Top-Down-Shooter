
using UnityEngine;

namespace Infrastructure.Services.AssertService.ExtendetAssertService
{
    public interface IAssert<out T,TU> : IService
    {
        public T Assert(TU objPath);
        public T Assert(TU objPath, Vector3 pos);
        
        public T Assert(TU objPath, Vector3 pos, Quaternion quaternion);
        
        public T Assert(TU objPath, Vector3 pos,Transform parent);
        
        public T Assert(TU objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }
}
