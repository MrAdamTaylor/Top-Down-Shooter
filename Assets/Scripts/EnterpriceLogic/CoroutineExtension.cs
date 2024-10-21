using UnityEngine;

namespace EnterpriceLogic
{
    public static class CoroutineExtension
    {
        public static void IsNull(this Coroutine coroutine, string message = "")
        {
            if (coroutine != null)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    Debug.Log(message);
                }
            }
        }
    }
}