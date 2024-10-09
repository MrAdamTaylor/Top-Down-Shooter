using UnityEngine;

namespace Infrastructure.Services.AssertService.ExtendetAssertService
{
    public class AssertLoader<T> : IAssertByString<T> where T : Object
    {
        public T Assert(string objPath)
        {
            T prefab = Resources.Load<T>(objPath);
            return prefab;
        }

        public T Assert(string objPath, Vector3 pos)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return particleSystem;
        }

        public T Assert(string objPath, Vector3 pos, Quaternion quaternion)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return particleSystem;
        }

        public T Assert(string objPath, Vector3 pos, Transform parent)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return particleSystem;
        }

        public T Assert(string objPath, Vector3 pos, Quaternion quaternion, Transform parent)
        {
            T particleSystem = Resources.Load<T>(objPath);
            return particleSystem;
        }
    }
}