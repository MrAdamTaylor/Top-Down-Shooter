using System;
using JetBrains.Annotations;

namespace Infrastructure.DI
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
        
    }
    
}