using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.DI.ServiceLocator
{
    public class DictionaryServiceLocator : IServiceLocator
    {
        private static readonly Dictionary<Type, object> _servicesDataBase = new();
    
        public void BindData(Type type, object obj)
        {
            if (_servicesDataBase.ContainsKey(type))
            {
                _servicesDataBase[type] = obj;
            }
            else
            {
                _servicesDataBase.Add(type, obj);
            }
        }

        public object GetData(Type type)
        {
            return _servicesDataBase[type];
        }

        public bool IsGetData(Type type)
        {
            return _servicesDataBase.ContainsKey(type);
        }

        public static void PrintAllServices()
        {
            foreach (var kvp in _servicesDataBase)
            {
                Debug.Log($"<color=magenta>Ключ: {kvp.Key}, Значение: {kvp.Value}</color>");
            }
        }
    }
}