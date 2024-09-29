using UnityEngine;

public interface IPlayerFactory : IGameFactory
{
    GameObject Create(Vector3 position, Camera camera);
}