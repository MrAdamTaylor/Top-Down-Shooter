using System;

namespace EnterpriceLogic.Utilities
{
    public static class StringHandler
    {
        public static bool IsEmpty(this string row)
        {
            if (row == "")
                return true;
            else
                return false;
        }
    }
}