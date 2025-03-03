using System;

namespace Infrastructure.DI.Model
{
    
        public interface IContainerBuilder
        {
            public void Register(ServiceDescriptor descriptor);

            public IContainer Build();
        }
        public interface IContainer
        {
            public IScope CreateScope();

            public void BindData(Type type, Type service, LifeTime lifeTime);
            
            void Construct(object behaviour);
            void CacheData(Type type, object instance);
        }
    
        public interface IScope 
        {
            public object Resolve(Type service);
        }
    
}
