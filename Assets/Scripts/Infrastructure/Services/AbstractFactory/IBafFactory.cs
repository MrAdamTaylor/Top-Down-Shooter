using Configs;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public interface IBafFactory
    {
        void Create(BafConfigs bafConfigs, Vector3 position);
    }
}