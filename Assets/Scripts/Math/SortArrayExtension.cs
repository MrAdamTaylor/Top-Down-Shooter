using System.Collections.Generic;
using UnityEngine;

namespace Math
{
    public static class SortArrayExtension
    {
        public static void QuickSort(this List<float> mainSorted, int leftPivot, int rightPivot, List<Vector3> vec,
            List<float> distances)
        {
            if (leftPivot < rightPivot)
            {
                int pivot = Partition(mainSorted, leftPivot, rightPivot, vec, distances);
                QuickSort(mainSorted, leftPivot, pivot - 1, vec, distances);
                QuickSort(mainSorted, pivot + 1, rightPivot, vec, distances);
            }
        }
        
        private static int Partition(List<float> arr, int left, int right, List<Vector3> vec,
            List<float> distances)
        {
            float pivot = arr[right];
            int i = left - 1;
 
            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);

                    (vec[i], vec[j]) = (vec[j], vec[i]);

                    (distances[i], distances[j]) = (distances[j], distances[i]);
                }
            }
 
            (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);

            (vec[i + 1], vec[right]) = (vec[right], vec[i + 1]);

            (distances[i + 1], distances[right]) = (distances[right], distances[i + 1]);

            return i + 1;
        }
    }
}