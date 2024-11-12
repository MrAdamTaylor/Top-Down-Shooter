using System;
using UnityEngine;
using Object = UnityEngine.Object;

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
            if (objPath is GameObject obj)
            {
                GameObject gameObject = Object.Instantiate(obj, pos, Quaternion.identity);
                gameObject.transform.SetParent(parent);
                return (T)Convert.ChangeType(gameObject, typeof(T));
            }
            else
            {
                throw new Exception("Error cast in assert object service");
            }
        }

        public T Assert(T objPath, Vector3 pos, Quaternion quaternion, Transform parent)
        {
            throw new System.NotImplementedException();
        }
    }
}