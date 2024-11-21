using System;
using JetBrains.Annotations;


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

