using System;
using JetBrains.Annotations;

namespace Infrastructure.DI.Injector
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        
    }
    
}