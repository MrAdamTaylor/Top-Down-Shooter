using Configs;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IBafFactory
    {
        void Create(BafConfigs bafConfigs, Vector3 position);
    }
}