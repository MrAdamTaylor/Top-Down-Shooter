using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAsserts _asserts;
    
    public GameFactory(IAsserts assets)
    {
        _asserts = assets;
    }
    
    public static GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public static GameObject Instantiate(string path, Vector3 at)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject CreatePlayer(Vector3 position, Camera camera)
    {
        GameObject gameObject = _asserts.Instantiate(Constants.PLAYER_PATH, position);
        gameObject.AddComponent<Player>();
        Player player = gameObject.GetComponent<Player>();
        gameObject.AddComponent<CameraFollower>().Construct(camera, player);
        gameObject.AddComponent<MouseRotateController>().Construct(camera, player);
        gameObject.AddComponent<AxisInputSystem>();
        IInputSystem system = gameObject.GetComponent<AxisInputSystem>();
        gameObject.AddComponent<MoveController>().Construct(player, system);
        return gameObject;
    }
}