using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.DI.Injector;
using Infrastructure.DI.Model;
using Infrastructure.DI.ServiceLocator;
using UnityEngine;
using ServiceDescriptor = Infrastructure.DI.ServiceLocator.ServiceDescriptor;

namespace Infrastructure.DI.Container
{
    public class Container : IContainer
    {
        private class Scope : IScope
        {
            private readonly Container _container;
            private readonly ConcurrentDictionary<Type, object> _scopedInstance = new();

            public Scope(Container container)
            {
                _container = container;
            }

            public object Resolve(Type service)
            {
                var descriptor = _container.FindDescriptors(service);
                if (descriptor.LifeTime == LifeTime.Transient)
                    return _container.CreateInstance(service, this);
                else if (descriptor.LifeTime == LifeTime.Scoped || _container._rootScope == this)
                {
                    return _scopedInstance.GetOrAdd(service, s => _container.CreateInstance(s, this));
                }
                else
                {
                    return _container._rootScope.Resolve(service);
                }
            }
        }

        private readonly ServiceDescriptor _serviceDescriptor;
        private readonly IServiceLocator _serviceLocator;
        private readonly DescriptorDependencyInjector _dependencyInjector;
        private readonly ConcurrentDictionary<Type,Func<IScope, object>> _cachedBuildActivators = new();
        private readonly Scope _rootScope;

        public Container(IEnumerable<Model.ServiceDescriptor> descriptors, IServiceLocator serviceLocator = null)
        {
            Dictionary<Type, Model.ServiceDescriptor> dictionaryDescriptors = descriptors.ToDictionary(x => x.ServiceType);
            _serviceDescriptor = new ServiceDescriptor(dictionaryDescriptors);
            if (serviceLocator == null)
            {
                _serviceLocator = new DictionaryServiceLocator();
            }
            _dependencyInjector = new DescriptorDependencyInjector(_serviceDescriptor, _serviceLocator, this);
            _rootScope = new Scope(this);
        }
        
        public Container(IServiceLocator serviceLocator = null)
        {
            _serviceDescriptor = new ServiceDescriptor();
            if (serviceLocator == null)
            {
                _serviceLocator = new DictionaryServiceLocator();
            }
            _dependencyInjector = new DescriptorDependencyInjector(_serviceDescriptor, _serviceLocator, this);
            _rootScope = new Scope(this);
        }

        public static void PrintServiceLocator()
        {
            DictionaryServiceLocator.PrintAllServices();
        }


        public object ReturnInjectArgument(Model.ServiceDescriptor descriptor, Type type)
        {
            object service = descriptor.LifeTime == LifeTime.Transient ? CreateInstance(type, _rootScope) : _rootScope.Resolve(type);
            return service;
        }

        private Model.ServiceDescriptor FindDescriptors(Type service)
        {
           return _serviceDescriptor.TryGetType(service);
        }

        private object CreateInstance(Type service, IScope scope)
        {
            return _cachedBuildActivators.GetOrAdd(service, BuildActivation)(scope);
        }

        public IScope CreateScope()
        {
            return new Scope(this);
        }

        public void BindData(Type type, Type service, LifeTime lifetime)
        {
            
            if (type.IsSubclassOf(typeof(MonoBehaviour)))
            {
                if (service == null)
                {
                    Debug.Log("Класс MonoBehaviour не может быть равен null");
                }
            }

            TypeBasedServiceDescriptor serviceDescriptor = new TypeBasedServiceDescriptor() 
                {ImplementationType = service, ServiceType = type, LifeTime = lifetime };
            _serviceDescriptor.BindData(type,serviceDescriptor);
        }

        public void Construct(object target)
        {
            _dependencyInjector.Inject(target);
        }

        public void CacheData(Type type, object instance)
        {
            
            if (_serviceLocator.IsGetData(type))
            {

                object existingInstance = _serviceLocator.GetData(type);
                
                if (existingInstance is IEnumerable existingEnumerable && instance is IEnumerable newEnumerable)
                {
                    object mergedCollection = EnumeratorExtension.MergeCollections(existingEnumerable, newEnumerable);

                    if (mergedCollection != null)
                    {
                        _serviceLocator.BindData(type, mergedCollection);
                        return;
                    }
                }
            }

            _serviceLocator.BindData(type, instance);
        }


        private Func<IScope, object> BuildActivation(Type service)
        {
            var descriptor = _serviceDescriptor.TryGetType(service);
            
            if(descriptor is null)
                throw new InvalidOperationException($"Service {service} is not registered.");

            if (descriptor is InstanceBasedServiceDescriptor ib)
                return _ => ib.Instance;

            if (descriptor is FactoryBasedServiceDescriptor fb)
                return _ => fb.Factory;

            var tb = (TypeBasedServiceDescriptor)descriptor;
            
            var ctor = tb.ImplementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var args = ctor.GetParameters();

            return s =>
            {
                var argsForCtor = new object[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    argsForCtor[i] = CreateInstance(args[i].ParameterType, s);
                }

                return ctor.Invoke(argsForCtor);
            };
        }
    }
}