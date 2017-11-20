/*******************************************************
 *  ArrayedQueue.cs
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Queue implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Queues.ArrayedQueue
{   
    /// <summary>
    /// Arrayed Queue Class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class ArrayedQueue<T> where T : IComparable
    {
        /**
         * Private Members
         */
        private T[] _array;
        private int _count;
        private int _size;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ArrayedQueue()
        {
            _array = new T[_size = 10];
            _count = 0;
        }

        /// <summary>
        /// Arrayed Queue Constructor
        /// </summary>
        /// <param name="size">Size to initialize the queue to</param>
        public ArrayedQueue(int size)
        {
            _array = new T[_size = size];
            _count = 0;
        }
        /**
         * Pushes given data onto the queue if space is available
         * @param data Data to be added to the queue
         * @return Node added to the queue
         */
        public T Enqueue(T data)
        {
            if (!IsFull())
            {
                _array[_count] = data;
                _count++;
                return Top();
            }
            return default(T);
        }

        /// <summary>
        /// Removes item from the queue
        /// </summary>
        /// <returns>item removed from the queue</returns>
        public T Dequeue()
        {
            if (IsEmpty())
                return default(T);
            T data = _array[0];
            T[] tmp = new T[_size];
            Array.Copy(_array, 1, tmp, 0, _size - 1 - 1);
            _array = tmp;
            _count--;
            return data;
        }

        /// <summary>
        /// Gets the top item of the queue
        /// </summary>
        /// <returns>item on top of the queue</returns>
        public T Top() => (IsEmpty()) ? default(T) : _array[0];

        /// <summary>
        /// Returns a value indicating if the queue is empty
        /// </summary>
        /// <returns>true if empty, false if not</returns>
        public bool IsEmpty() => (_count == 0);

        /// <summary>
        /// Returns a value indicating if the queue is full
        /// </summary>
        /// <returns>True if full, false if not</returns>
        public bool IsFull() => _count == _size;
    }
}
