using System;
using UnityEngine;

namespace EnterpriceLogic.Utilities
{
    public static class NullHandler
    {
        public static bool IsNull<T>(this T obj)
        {
            if (obj == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public static bool IsNull(this long value)
        {
            
            if (value == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}