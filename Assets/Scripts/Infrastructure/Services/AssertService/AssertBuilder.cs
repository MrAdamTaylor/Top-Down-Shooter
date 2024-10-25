using UnityEngine;

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
    }
}