using System;

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
    }
}