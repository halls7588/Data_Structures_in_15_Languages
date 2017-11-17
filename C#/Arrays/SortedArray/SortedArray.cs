/*******************************************************
 *  SortedArray.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in C#
 ********************************************************/
 using System;

namespace DataStructures.Arrays.SortedArray
{
    public class SortedArray<T> where T : IComparable{
        /**
         * Private Members
         */
        private T[] _array;

        private int _count;
        private int _size;

        /**
         * SortedArray default constructor
         */
        public SortedArray()
        {
            _count = 0;
            _array = new T[_size = 4];
        }

        /**
         * SortedArray constructor initialized to a specific size
         * @param size Size to initialize the array to
         */

        public SortedArray(int size)
        {
            _count = 0;
            _size = (size > 0) ? size : 4;
            _array = new T[_size];
        }

        /**
         * Doubles the size of the internal array
         */

        private void Resize()
        {
            _size *= 2;
            T[] tmp = new T[_size];
            Array.Copy(_array, tmp, _count);
            _array = tmp;
        }

        /**
         * Adds new item into the array
         * @param data Data to add into the array
         * @return Data added into the array
         */
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

        /**
         * Appends the contents of an array to the SortedArray
         * @param data: array to append
         * @return boolean: success|fail
         */
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

        /**
         * Sets the data at the given index
         * @param index: index to set
         * @param data: data to set index to
         * @return boolean: success|fail
         */
        public bool Set(int index, T data)
        {
            if (index >= 0 && index < _size)
            {
                _array[index] = data;
                return true;
            }
            return false;
        }

        /**
         * Gets the data at the arrays given index
         * @param index Index to get data at
         * @return Data at the given index or default value of T if index does not exist
         */
        public T Get(int index)
        {
            return index >= 0 && index < _size ? _array[index] : default(T);
        }

        /**
         * Removes the data at arrays given index
         * @param index Index to remove
         * @return Data removed from the array or default T value if index does not exist
         */
        public T Remove(int index)
        {
            if (index < 0 || index > _count)
                return default(T);

            T tmp = _array[index];
            _array[index] = default(T);
            _count--;
            return tmp;
        }

        /**
         * Resets the internal array to default size with no data
         */

        public void Reset()
        {
            _count = 0;
            _size = 4;
            _array = new T[_size];
        }

        /**
         * Clears all data in the array leaving size intact
         */
        public void Clear()
        {
            _array = new T[_size];   
            _count = 0;
        }


        /**
         * Gets the current count of the array
         * @return Number of items in the array
         */
        public int Count() => _count;

        /**
         * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
         * @param arr: array to be sorted
         * @param l: index of first sub array
         * @param m: merge point
         * @param r: index of second sub array
         */

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

        /**
         * Recursive helper method for merge sort
         * @param arr: sub array to be sorted
         * @param l: left index
         * @param r: right index
         */
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

        /**
         * Performs Merge Sort on internal array
         * @return T[]: sorted copy of the internal array
         */

        public T[] MergeSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);
            MergeSortHelper(tmp, 0, (_count - 1));
            return tmp;
        }

        /**
         * Performs Bubble sort on internal array
         * @return T[]: sorted copy of the internal array
         */

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

        /**
         * Helper method for Quick Sort. Swaps data in the partition
         * @param arr: array to be sorted
         * @param low: low index
         * @param high: high index
         * @return int: pivot index
         */
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

        /**
         * Helper method for recursive Quick Sort
         * @param arr: array to be sorted
         * @param low: low index
         * @param high: high index
         */
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

        /**
         * Performs Quick Sort on the internal array
         * @return T[]: sorted copy of the internal array
         */

        public T[] QuickSort()
        {
            T[] tmp = new T[_count];
            Array.Copy(_array, tmp, _count);
            QuickSortHelper(tmp, 0, (_count - 1));

            return tmp;
        }

        /**
         * Performs Insertion sort on the internal array
         * @return T[]: sorted copy of the internal array
         */

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

        /**
         * Performs Selection Sort on internal array
         * @return T[]: Sorted copy of the internal data
         */

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

        /**
         * Determines if a is less than b
         * @param a: generic type to test
         * @param b: generic type to test
         * @return boolean: true|false
         */
        private bool LessThan(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        /**
         * Determines if a is equal to b
         * @param a: generic type to test
         * @param b: generic type to test
         * @return boolean: true|false
         */
        private bool EqualTo(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        /**
         * Determines if a is greater than b
         * @param a: generic type to test
         * @param b: generic type to test
         * @return boolean: true|false
         */
        private bool GreaterThan(T a, T b)
        {
            return a.CompareTo(b) > 0;
        }
    }

}

