using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class GameModule : MonoBehaviour
{
    public virtual IEnumerable<object> GetServices()
    {
        Type type = this.GetType();
        FieldInfo[] fields = type.GetFields(
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.DeclaredOnly
        );
        
        foreach (var field in fields)
        {
            if (field.IsDefined(typeof(Service)))
            {
                yield return field.GetValue(this);
            }
        }
    }
}
