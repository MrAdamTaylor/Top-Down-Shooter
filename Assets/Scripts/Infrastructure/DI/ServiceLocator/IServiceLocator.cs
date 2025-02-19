using System;

namespace Infrastructure.DI
{
    public interface IServiceLocator
    {
        public void BindData(Type type, object obj);
        public object GetData(Type type);
        public bool IsGetData(Type type);
    }
}