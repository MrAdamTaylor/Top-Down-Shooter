using System;
using System.Collections.Generic;

public interface IServiceProvider
{
    IEnumerable<(Type, object)> ProvideServices();
}