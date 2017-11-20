/*******************************************************
 *  PriorityQueue.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Priority Queue implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Queues.PriorityQueue
{
    /// <summary>
    /// Priority Queue class
    /// </summary>
    /// <typeparam name="T">generic type</typeparam>
    public class PriorityQueue<T> where T : IComparable
    {
        /// <summary>
        /// private members
        /// </summary>
        private T[] _arr;
        private int _count;

        /// <summary>
        /// PriorityQueue class Constructor
        /// </summary>
        public PriorityQueue()
        {
            _arr = new T[4];
            _count = 0;
        }

        /// <summary>
        /// Adds an item into the Queue
        /// </summary>
        /// <param name="data">Data to add to the queue</param>
        /// <returns>Data added into the Queue</returns>
        public T Enqueue(T data)
        {
            if (_count == _arr.Length - 1)
                Resize();
            _arr[_count] = data;
            _count++;
            Swim(_count);
            return data;
        }

        /// <summary>
        /// Removes an item from the queue
        /// </summary>
        /// <returns>data removed from the queue</returns>
        public T Dequeue()
        {
            if (IsEmpty())
                return default(T);
            T data = _arr[1];
            Swap(1, _count--);
            _arr[_count + 1] = default(T);
            Sink(1);
            return data;
        }

        /// <summary>
        /// Determines if the Queue is empty or not
        /// </summary>
        /// <returns>true|false</returns>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Gets the size of the queue
        /// </summary>
        /// <returns>size of the queue</returns>
        public int Size() => _count;

        /// <summary>
        /// Doubles the capacity of the queue
        /// </summary>
        private void Resize()
        {
            T[] copy = new T[_count * 2 + 1];
            Array.Copy(_arr, 1, copy, 1, _count);
            _arr = copy;
        }
 
        /// <summary>
        /// Swims higher priority items up
        /// </summary>
        /// <param name="k">index to start at</param>
        private void Swim(int k)
        {
            while (k > 1 && k / 2 < k)
            {
                Swap(k / 2, k);
                k = k / 2;
            }
        }

        /// <summary>
        /// Sinks lower priority items down
        /// </summary>
        /// <param name="index">index to start at</param>
        private void Sink(int index)
        {
            while (index * 2 < _count)
            {
                int j = 2 * index;
                if (j < _count && j < j + 1)
                    j = j + 1;
                if (LessThan(j, index))
                    break;
                Swap(index, j);
                index = j;
            }
        }

        /// <summary>
        /// Determines if arr[i] is less than arr[j]
        /// </summary>
        /// <param name="i">first index to test</param>
        /// <param name="j">second index to test</param>
        /// <returns>true|false</returns>
        private bool LessThan(int i, int j) =>  _arr[i].CompareTo(_arr[j]) < 0;

        /// <summary>
        /// Swaps the values at the given indices
        /// </summary>
        /// <param name="i">fist index</param>
        /// <param name="j">second index</param>
        private void Swap(int i, int j)
        {
            T temp = _arr[i];
            _arr[i] = _arr[j];
            _arr[j] = temp;
        }
    }
}
