using UnityEngine;

public interface IGameFactory : IService
{
    public GameObject CreatePlayer(Vector3 position, Camera camera);
}