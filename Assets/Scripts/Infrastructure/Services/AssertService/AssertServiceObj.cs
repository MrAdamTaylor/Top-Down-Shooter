using UnityEngine;

namespace Infrastructure.Services.AssertService
{
    public class AssertServiceObj<T> : IAssertByObj<T> where T : Object
    {
        public T Assert(T objPath)
        {
            return Object.Instantiate(objPath);
        }

        public T Assert(T objPath, Vector3 pos)
        {
            return Object.Instantiate(objPath, pos, Quaternion.identity);
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
}