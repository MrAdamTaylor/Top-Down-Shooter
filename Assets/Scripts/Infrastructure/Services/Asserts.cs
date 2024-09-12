using UnityEngine;

public class Asserts : IAsserts
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at)
    {
        var prefab = Resources.Load<GameObject>(path);
        return GameObject.Instantiate(prefab, at, Quaternion.identity);
    }
}