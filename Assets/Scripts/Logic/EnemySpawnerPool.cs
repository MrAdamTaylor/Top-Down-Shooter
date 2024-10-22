using System;
using UnityEngine;

public class EnemySpawnerPool : MonoBehaviour
{
    private GameObjectPool _pool;
    private int _objectCount;

    public void Construct(int enemyCount, Func<GameObject> action)
    {
        _objectCount = enemyCount;
        _pool = new GameObjectPool(action, _objectCount);
    }

    public void Spawn()
    {
        GameObject obj = _pool.Get();
    }

    public void ReturnPool(GameObject obj)
    {
        _pool.Return(obj);
    }

}