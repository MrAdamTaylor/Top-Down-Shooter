using System;
using System.Collections.Generic;

namespace EnterpriceLogic.Math
{
    public static class PercentageCalculater
    {
        private const int PERCENTAGE_MAXIMUM = 100;
        
        /// <summary>
        /// Check you sum of percentage on 100% percantage
        /// </summary>
        /// <returns></returns>
        public static void PercentageСhecker(List<int> standartPercantage, string message = "")
        {
            for (int i = 0; i < standartPercantage.Count; i++)
            {
                if (standartPercantage[i] != PERCENTAGE_MAXIMUM)
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        throw new Exception($"<color=cyan>Incorrectly distributed percentages </color>");
                    }
                    else
                    {
                        throw new Exception($"<color=cyan>Incorrectly distributed percentages in the line for {message}</color>");
                    }
                }
            }
        }

        public static int CalculateValueInPercantage(int percantage, int counts, int maxPercantage = PERCENTAGE_MAXIMUM)
        {
            float coefficient = (float)percantage / maxPercantage;
            float value = counts * coefficient;
            return (int)value;
        }
        
        
    }
}
