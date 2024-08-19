
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ServiceLocator 
{
    [CanBeNull] private static ServiceLocator _instance;

    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceLocator();
            }
            return _instance;
        }
    }

    private readonly Dictionary<Type, object> _servicesDataBase = new();

    public object GetData(Type type)
    {
        return _servicesDataBase[type];
    }

    public void BindData(Type type, object data)
    {
        if (_servicesDataBase.ContainsKey(type))
        {
            _servicesDataBase[type] = data;
        }
        else
        {
            _servicesDataBase.Add(type, data);
        }
    }

}
