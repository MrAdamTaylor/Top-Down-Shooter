using System;
using JetBrains.Annotations;

namespace Infrastructure.DIConteiner
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
    }

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class Service : Attribute
    {
    
    }
}