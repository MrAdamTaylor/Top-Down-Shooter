using System;
using System.Collections.Generic;

namespace Infrastructure.DIConteiner
{
    public interface IServiceProvider
    {
        IEnumerable<(Type, object)> ProvideServices();
    }
}