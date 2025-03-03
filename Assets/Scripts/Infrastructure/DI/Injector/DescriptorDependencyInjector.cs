using System;
using System.Collections.Generic;
using System.Reflection;
using Infrastructure.DI.ServiceLocator;
using UnityEngine;

namespace Infrastructure.DI.Injector
{
    public class DescriptorDependencyInjector : IInjector
    {
        private ServiceDescriptor _serviceDescriptor;
        private IServiceLocator _serviceLocator;
        private Container.Container _container;

        public DescriptorDependencyInjector(ServiceDescriptor service, IServiceLocator serviceLocator, Container.Container container)
        {
            _serviceLocator = serviceLocator;
            _serviceDescriptor = service;
            _container = container;
        }
        
        public void Inject(object target)
        {
            Type type = target.GetType();
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            foreach (MethodInfo method in methods)
            {
                if (method.IsDefined(typeof(InjectAttribute)))
                {
                    InvokeConstruct(method, target);
                }
            }
        }

        private void InvokeConstruct(MethodInfo method, object target)
        {
            ParameterInfo[] parametrs = method.GetParameters();
            int count = parametrs.Length;
            object[] args = new object[count];

            for (int i = 0; i < count; i++)
            {
                ParameterInfo parametr = parametrs[i];
                Type type = parametr.ParameterType;
                
                
                object service = null;
                
                if (typeof(ScriptableObject).IsAssignableFrom(type))
                {
                    if (_serviceLocator.IsGetData(type))
                    {
                        service = _serviceLocator.GetData(type);
                    }
                }
                else if (type.Namespace == "System.Collections.Generic" || 
                         (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
                {
                    if (_serviceLocator.IsGetData(type))
                    {
                        service = _serviceLocator.GetData(type);
                    }
                }
                
                if (service == null)
                {
                    Model.ServiceDescriptor descriptor = _serviceDescriptor.GetDescriptor(type);
                    service = _container.ReturnInjectArgument(descriptor, type);
                }
                

                args[i] = service;
            }

            method.Invoke(target, args);
        }
    }
}