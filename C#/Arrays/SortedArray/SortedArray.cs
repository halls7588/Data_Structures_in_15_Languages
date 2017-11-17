/*******************************************************
 *  SortedArray.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in C#
 ********************************************************/
 using System;

namespace DataStructures.Arrays.SortedArray
{
    /// <summary>
    /// Sorted Array Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class SortedArray<T> where T : IComparable
    {
        /// <summary>
        /// Private members
        /// </summary>
        private T[] _array;
        private int _count;
        private int _size;

        /// <summary>
        /// SortedArray default Constructor
        /// </summary>
        public SortedArray()
        {
            _count = 0;
            _array = new T[_size = 4];
        }

        /// <summary>
        /// SortedArray constructor initialized to a specific size
        /// </summary>
        /// <param name="size">Size to initialize the array to</param>
        public SortedArray(int size)
        {
            _count = 0;
            _size = (size > 0) ? size : 4;
            _array = new T[_size];
        }

        /// <summary>
        ///  Doubles the size of the internal array
        /// </summary>
        private void Resize()
        {
            _size *= 2;
            T[] tmp = new T[_size];
            Array.Copy(_array, tmp, _count);
            _array = tmp;
        }

        /// <summary>
        /// Adds new item into the array
        /// </summary>
        /// <param name="data">Data to add into the array</param>
        /// <returns>Data added into the array</returns>
        public T Add(T data)
        {
            if (data == null)
                return default(T);

            if (_count == _size)
                Resize();

            _array[_count] = data;
            _count++;

            return data;
        }

        /// <summary>
        /// Appends the contents of an array to the SortedArray
        /// </summary>
        /// <param name="data"> array to append</param>
        /// <returns>success|fail</returns>
        public bool Append(T[] data)
        {
            if (data == null)
                return false;
            foreach (T aData in data)
            {
                if (aData != null)
                    Add(aData);
            }
            return true;
        }

        /// <summary>
        /// Sets the data at the given index
        /// </summary>
        /// <param name="index">index to set</param>
        /// <param name="data">data to set index to</param>
        /// <returns>success|fail</returns>
        public bool Set(int index, T data)
        {
            if (index >= 0 && index < _size)
            {
                _array[index] = data;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the data at the arrays given index
        /// </summary>
        /// <param name="index">Index to get data at</param>
        /// <returns>Data at the given index or default value of T if index does not exist</returns>
        public T Get(int index) => index >= 0 && index < _size ? _array[index] : default(T);

        /// <summary>
        /// Removes the data at arrays given index
        /// </summary>
        /// <param name="index">Index to remove</param>
        /// <returns>Data removed from the array or default T value if index does not exist</returns>
        public T Remove(int index)
        {
            if (index < 0 || index > _count)
                return default(T);

            T tmp = _array[index];
            _array[index] = default(T);
            _count--;
            return tmp;
        }
        
        /// <summary>
        /// Resets the internal array to default size with no data
        /// </summary>
        public void Reset()
        {
            _count = 0;
            _size = 4;
            _array = new T[_size];
        }
        
        /// <summary>
        /// Clears all data in the array leaving size intact
        /// </summary>
        public void Clear()
        {
            _array = new T[_size];   
            _count = 0;
        }
        
        /// <summary>
        /// Gets the current count of the array
        /// </summary>
        /// <returns>Number of items in the array</returns>
        public int Count() => _count;

        /// <summary>
        /// Private helper method for Merger Sort. Merges two sub-arrays of arr[].
        /// </summary>
        /// <param name="arr">array to be sorted</param>
        /// <param name="l">index of first sub array</param>
        /// <param name="m">merge point</param>
        /// <param name="r">index of second sub array</param>
        private void Merge(T[] arr, int l, int m, int r)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            // create temp arrays
            T[] L = new T[n1];
            T[] R = new T[n2];

            // Copy data to temp arrays L[] and R[]
            for (i = 0; i < n1; i++)
                L[i] = arr[l + i];
            for (j = 0; j < n2; j++)
                R[j] = arr[m + 1 + j];

            // Merge the temp arrays back into arr[l..r]
            i = 0; // Initial index of first sub-array
            j = 0; // Initial index of second sub-array
            k = l; // Initial index of merged sub-array
            while (i < n1 && j < n2)
            {
                if (LessThan(L[i], R[j]) || EqualTo(L[i], R[j]))
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            // Copy the remaining elements of L[], if there are any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy the remaining elements of R[], if there are any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
       
        /// <summary>
        /// Recursive helper method for merge sort
        /// </summary>
        /// <param name="arr">sub array to be sorted</param>
        /// <param name="l">left index</param>
        /// <param name="r">right index</param>
        private void MergeSortHelper(T[] arr, int l, int r)
        {
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for
                // large l and h
                int m = l + (r - l) / 2;

                // Sort first and second halves
                MergeSortHelper(arr, l, m);
                MergeSortHelper(arr, (m + 1), r);

                Merge(arr, l, m, r);
            }
        }

        /// <summary>
        /// Performs Merge Sort on internal array
        /// </summary>
        /// <returns>sorted copy of the internal array</returns>
        public T[] MergeSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);
            MergeSortHelper(tmp, 0, (_count - 1));
            return tmp;
        }

        /// <summary>
        /// Performs Bubble sort on internal array
        /// </summary>
        /// <returns>sorted copy of the internal array</returns>
        public T[] BubbleSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);

            for (int i = 0; i < _count - 1; i++)
                for (int j = 0; j < _count - i - 1; j++)
                {
                    if (GreaterThan(tmp[j], tmp[j + 1]))
                    {
                        // swap temp and arr[i]
                        T temp = tmp[j];
                        tmp[j] = tmp[j + 1];
                        tmp[j + 1] = temp;
                    }
                }
            return tmp;
        }

        /// <summary>
        /// Helper method for Quick Sort. Swaps data in the partition
        /// </summary>
        /// <param name="arr">array to be sorted</param>
        /// <param name="low">low index</param>
        /// <param name="high">high index</param>
        /// <returns>pivot index</returns>
        private int Partition(T[] arr, int low, int high)
        {
            T pivot = arr[high];
            T temp;
            int i = (low - 1); // index of smaller element
            for (int j = low; j < high; j++)
            {
                // If current element is smaller than or
                // equal to pivot
                if (LessThan(arr[j], pivot) || EqualTo(arr[j], pivot))
                {
                    i++;
                    // swap arr[i] and arr[j]
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot)
            temp = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp;

            return i + 1;
        }

        /// <summary>
        /// Helper method for recursive Quick Sort
        /// </summary>
        /// <param name="arr">array to be sorted</param>
        /// <param name="low">low index</param>
        /// <param name="high">high index</param>
        private void QuickSortHelper(T[] arr, int low, int high)
        {
            if (low < high)
            {
                // pivot is partitioning index, arr[pivot] is now at right place
                int pivot = Partition(arr, low, high);

                // Recursively sort elements before and after pivot
                QuickSortHelper(arr, low, (pivot - 1));
                QuickSortHelper(arr, (pivot + 1), high);
            }
        }

        /// <summary>
        /// Performs Quick Sort on the internal array
        /// </summary>
        /// <returns>sorted copy of the internal array</returns>
        public T[] QuickSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);
            QuickSortHelper(tmp, 0, (_count - 1));

            return tmp;
        }

        /// <summary>
        /// Performs Insertion sort on the internal array
        /// </summary>
        /// <returns>sorted copy of the internal array</returns>
        public T[] InsertionSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, 0, tmp, 0, _count);

            for (int i = 1; i < _count; ++i)
            {
                T key = tmp[i];
                int j = i - 1;

                // Move elements of arr[0..i-1], that are greater than key, to one position ahead
                while (j >= 0 && LessThan(tmp[j], key))
                {
                    tmp[j + 1] = tmp[j];
                    j = j - 1;
                }
                tmp[j + 1] = key;
            }
            return tmp;
        }

        /// <summary>
        ///  Performs Selection Sort on internal array
        /// </summary>
        /// <returns>Sorted copy of the internal data</returns>
        public T[] SelectionSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);
            // One by one move boundary of unsorted sub-array
            for (int i = 0; i < _count - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min = i;
                for (int j = i + 1; j < _count; j++)
                    if (LessThan(tmp[j], tmp[min]))
                        min = j;

                // Swap the found minimum element with the first
                // element
                T temp = tmp[min];
                tmp[min] = tmp[i];
                tmp[i] = temp;
            }
            return tmp;
        }

        /// <summary>
        /// Determines if a is less than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private bool LessThan(T a, T b) => a.CompareTo(b) < 0;

        /// <summary>
        /// Determines if a is equal to b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private bool EqualTo(T a, T b) => a.CompareTo(b) == 0;

        /// <summary>
        /// Determines if a is greater than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private bool GreaterThan(T a, T b) => a.CompareTo(b) > 0;
    }

}

