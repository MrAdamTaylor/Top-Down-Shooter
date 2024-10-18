using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentProvider : MonoBehaviour
{
    private readonly Dictionary<Type, object> _playerComponents = new();

    public void AddToProvideComponent<TComponent>(TComponent component) where TComponent : IPlayerComponent
    {
        _playerComponents.Add(component.GetType(), component);
    }

    public object TakeComponent(Type component)
    {
        return _playerComponents[component];
    }
}

public interface IPlayerComponent
{
    
}
