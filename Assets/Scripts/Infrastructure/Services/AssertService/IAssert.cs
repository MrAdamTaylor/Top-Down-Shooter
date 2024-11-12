using System.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.AssertService
{
    public interface IAssert<out T,TU> : IService
    {
        public T Assert(TU objPath);
        public T Assert(TU objPath, Vector3 pos);
        
        public T Assert(TU objPath, Vector3 pos, Quaternion quaternion);
        
        public T Assert(TU objPath, Vector3 pos,Transform parent);
        
        public T Assert(TU objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }

    public interface IAsyncAssert<T, TU> : IService
    {
        public Task<T> Assert(TU objPath);
        
        public Task<T> Assert(TU objPath, Vector3 pos);
        
        public Task<T> Assert(TU objPath, Vector3 pos, Quaternion quaternion);
        
        public Task<T> Assert(TU objPath, Vector3 pos,Transform parent);
        
        public Task<T> Assert(TU objPath, Vector3 pos, Quaternion quaternion, Transform parent);
        
    }
}
