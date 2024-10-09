
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

    public interface IAssertByString<out T> : IAssert<T,string> where T : Object 
    {
        public new T Assert(string objPath);

        public new T Assert(string objPath, Vector3 pos);
        
        public new T Assert(string objPath, Vector3 pos, Quaternion quaternion);
        
        public new T Assert(string objPath, Vector3 pos,Transform parent);
        
        public new T Assert(string objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }

    public interface IAssertByObj<T> : IAssert<T, T> where T : Object 
    {
        public new T Assert(T objPath);

        public new T Assert(T objPath, Vector3 pos);
        
        public new T Assert(T objPath, Vector3 pos, Quaternion quaternion);
        
        public new T Assert(T objPath, Vector3 pos,Transform parent);
        
        public new T Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent);
    }
    
    public class AssertServiceObj<T> : IAssertByObj<T> where T : Object
    {
        public T Assert(T objPath)
        {
            throw new System.NotImplementedException();
        }

        public T Assert(T objPath, Vector3 pos)
        {
            throw new System.NotImplementedException();
        }

        public T Assert(T objPath, Vector3 pos, Quaternion quaternion)
        {
            throw new System.NotImplementedException();
        }

        public T Assert(T objPath, Vector3 pos, Transform parent)
        {
            throw new System.NotImplementedException();
        }

        public T Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AssertServiceString<T> : IAssertByString<T> where T : Object
    {
        public T Assert(string objPath)
        {
            T prefab = Resources.Load<T>(objPath);
            return Object.Instantiate(prefab);
        }

        public T Assert(string objPath, Vector3 pos)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return Object.Instantiate(particleSystem, pos, Quaternion.identity);
        }

        public T Assert(string objPath, Vector3 pos, Quaternion quaternion)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return Object.Instantiate(particleSystem, pos, quaternion);
        }

        public T Assert(string objPath, Vector3 pos, Transform parent)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return Object.Instantiate(particleSystem, pos, Quaternion.identity, parent);
        }

        public T Assert(string objPath, Vector3 pos, Quaternion quaternion, Transform parent)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return Object.Instantiate(particleSystem, pos, quaternion, parent);
        }
    }
}
