using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
    public interface IPlayerFactory : IGameFactory
    {
        GameObject Create(Vector3 position, Camera camera);
    }
}