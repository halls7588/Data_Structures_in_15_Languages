/*******************************************************
 *  HeapSort.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  HeapSort implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Heaps.HeapSort
{
    /**
 * HeapSort Class
 * @param <T> Generic Type
 */
    public static class HeapSort<T> where T : IComparable
    {
       
        /// <summary>
        /// orts the given array
        /// </summary>
        /// <param name="heap">array to sort</param>
        public static void Sort(T[] heap)
        {
            for (int i = heap.Length / 2 - 1; i >= 0; i--)
                Heapify(heap, heap.Length, i);
            for (int i = heap.Length - 1; i >= 0; i--)
            {
                T temp = heap[0];
                heap[0] = heap[i];
                heap[i] = temp;
                Heapify(heap, i, 0);
            }
        }

        /// <summary>
        /// Builds a heap out of the array
        /// </summary>
        /// <param name="arr">array to heapify</param>
        /// <param name="length">length allowed</param>
        /// <param name="index">starting index </param>
        private static void Heapify(T[] arr, int length, int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;

            if (left < length && arr[left].CompareTo(arr[largest]) > 0)
                largest = left;

            if (right < length && arr[right].CompareTo(arr[largest]) > 0)
                largest = right;

            if (largest != index)
            {
                T swap = arr[index];
                arr[index] = arr[largest];
                arr[largest] = swap;
                Heapify(arr, length, largest);
            }
        }
    }

}
