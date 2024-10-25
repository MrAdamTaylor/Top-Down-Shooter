using System.Collections;
using System.Text;
using UnityEngine;

namespace EnterpriceLogic
{
    public static class CollectionsOutputExtension
    {
        private static IEnumerator _enumerator;
        private static StringBuilder _stringBuilder = new ();
        
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
            CreateEnumerableRow();
            Debug.Log($"{_stringBuilder} + <color=green>{message}</color>");
            _stringBuilder.Clear();
        }

        private static void CreateEnumerableRow()
        {
            while (_enumerator.MoveNext())
            {
                if (_enumerator.Current != null)
                {
                    _stringBuilder.Append('-',3);
                    _stringBuilder.Append('[');
                    _stringBuilder.Append(_enumerator.Current);
                    _stringBuilder.Append(']');
                    _stringBuilder.Append('-', 3);
                }
                _stringBuilder.AppendLine();
            }
        }

        private static void OutputWithoutMessage()
        {
            CreateEnumerableRow();
            Debug.Log(_stringBuilder);
            _stringBuilder.Clear();
        }
    }
}