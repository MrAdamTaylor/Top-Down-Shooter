using Mechanics.Spawners;
using UnityEngine;

public class GameObjectPool : PoolBase<GameObject>
{
    public static Transform _parent;
    
    public GameObjectPool(GameObject prefab, int preloadCount)
        :base(()=> Preload(prefab),GetAction, ReturnAction, preloadCount)
    {
    }

    public static GameObject Preload(GameObject prefab)
    {
        var gameObject = Object.Instantiate(prefab, _parent, true);
        return gameObject;
    }


    public static void GetAction(GameObject gObject) => gObject.SetActive(true);
    
    public static void ReturnAction(GameObject gObject) => gObject.SetActive(false);
}