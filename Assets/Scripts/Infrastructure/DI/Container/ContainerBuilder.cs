using System.Collections.Generic;
using Infrastructure.DI2.Model;

namespace Infrastructure.DI2.Container
{
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<Model.ServiceDescriptor> _descriptors = new();
        
        public void Register(Model.ServiceDescriptor descriptor)
        {
            _descriptors.Add(descriptor);
        }

        public IContainer Build()
        {
            return new Container(_descriptors);
        }
    }
}