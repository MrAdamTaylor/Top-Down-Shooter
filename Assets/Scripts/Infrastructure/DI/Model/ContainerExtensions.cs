using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.DI2.Model
{
    public static class ContainerExtensions
    {
        public static void RegisterScoped<TService, TImplementation>(this IContainer container)
            where TImplementation : TService
        {
            container.BindData(typeof(TService), typeof(TImplementation), LifeTime.Scoped);
        }

        public static void RegisterTransient<TService, TImplementation>(this IContainer container)
            where TImplementation : TService
        {
            container.BindData(typeof(TService), typeof(TImplementation), LifeTime.Transient);
        }

        public static void RegisterSingleton<TService, TImplementation>(this IContainer container)
            where TImplementation : TService
        {
            container.BindData(typeof(TService), typeof(TImplementation), LifeTime.Singleton);
        }

        public static void RegisterSingleton<T>(this IContainer container, object instance)
        {
            container.BindData(typeof(T), instance.GetType(), LifeTime.Singleton);
        }
        
        public static void RegisterConfigs<T>(this IContainer container, object instance) 
        {
            //container.BindData(typeof(T), instance.GetType(), LifeTime.Singleton);
            
            container.CacheData(typeof(T), instance);
        }
    }
}