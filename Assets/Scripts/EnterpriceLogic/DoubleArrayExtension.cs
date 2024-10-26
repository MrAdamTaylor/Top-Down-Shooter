using System.Collections.Generic;

namespace EnterpriceLogic
{
    public static class DoubleArrayExtension
    {
        public static List<int> GetArrayByIndex(this List<List<int>> array, int index)
        {
            List<int> resultArray = new List<int>();
            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array[i].Count; j++)
                {
                    if (j == index)
                        resultArray.Add(array[i][j]);
                    else
                        continue;
                }
            }
            return resultArray;
        }
    }
}