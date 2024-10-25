using System.Collections;
using UnityEngine;

namespace EnterpriceLogic
{
    public static class CollectionsOutputExtension
    {
        private static IEnumerator _enumerator = null;
        
        public static void OutputCollection(this IEnumerable enumerable, string message = "")
        {
            _enumerator = enumerable.GetEnumerator();

            if (!string.IsNullOrEmpty(message))
                OutputWithMessage(message);
            else
                OutputWithoutMessage();

            _enumerator = null;
        }

        private static void OutputWithMessage(string message)
        {
            string values = "";
            values = CreateEnumerableRow(values);
            Debug.Log(message + values);
        }

        private static string CreateEnumerableRow(string values)
        {
            while (_enumerator.MoveNext())
            {
                if (_enumerator.Current != null) 
                    values += '[' + _enumerator.Current.ToString() + ']';
            }
            return values;
        }

        private static void OutputWithoutMessage()
        {
            string values = "";
            values = CreateEnumerableRow(values);
            Debug.Log(values);
        }
    }
}