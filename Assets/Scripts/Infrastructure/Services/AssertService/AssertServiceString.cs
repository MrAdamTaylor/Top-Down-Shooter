using UnityEngine;

namespace Infrastructure.Services.AssertService.ExtendetAssertService
{
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