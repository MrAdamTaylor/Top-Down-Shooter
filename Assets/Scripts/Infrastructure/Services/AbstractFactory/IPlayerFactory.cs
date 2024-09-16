using UnityEngine;

public interface IPlayerFactory : IGameFactory
{
    public GameObject CreatePlayer(Vector3 position, Camera camera);
}