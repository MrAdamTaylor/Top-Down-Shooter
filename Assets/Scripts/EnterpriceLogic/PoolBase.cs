using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase<T>
{
    public int PoolCount { get; private set; }
    
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;
    private Queue<T> _pool = new();
    private List<T> _active = new();

    private Transform _parent;

    protected PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;
        PoolCount = 0;
        if (preloadFunc == null)
        {
            Debug.LogError("Preload function is null");
            return;
        }
        
        for(int i = 0; i < preloadCount; i++)
            Return(preloadFunc());
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : throw new Exception("Pool Not Have Objects");
        _getAction(item);
        _active.Add(item);
        PoolCount--;

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _pool.Enqueue(item);
        _active.Remove(item);
        PoolCount++;
    }

    public void ReturnAll()
    {
        foreach (T item in _active.ToArray()) Return(item);
    }
}