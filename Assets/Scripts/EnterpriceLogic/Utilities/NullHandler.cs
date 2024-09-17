using System;
using UnityEngine;

namespace EnterpriceLogic.Utilities
{
    public static class NullHandler
    {
        public static void IsNullWithException<T>(this T obj, string warningLabel)
        {
            if (obj == null)
            {
                throw new Exception(warningLabel);
            }
        }

        public static bool IsNullBoolWarning<T>(this T obj, string warningLabel = "")
        {
            if (obj == null)
            {
                Debug.LogWarning(warningLabel);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
    }
}