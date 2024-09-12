using UnityEngine;

public interface IAsserts : IService
{
    public GameObject Instantiate(string path);

    public GameObject Instantiate(string path, Vector3 at);
}