using System;
using System.Collections.Generic;
using EnterpriceLogic.Utilities;
using JetBrains.Annotations;

namespace Infrastructure.ServiceLocator
{
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

        public bool IsGetData(Type type)
        {
            if (_servicesDataBase.ContainsKey(type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object GetData(Type type)
        {
            return _servicesDataBase[type];
        }

        public object GetCloneData(Type type)
        {
            var obj = _servicesDataBase[type];
            return obj.PrototypeDeepClone();
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

        public void CleanData(Type type)
        {
            _servicesDataBase.Remove(type);
        }

        public void CleanAllData()
        {
            _servicesDataBase.Clear();
        }
    }
}