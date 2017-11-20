/*******************************************************
 *  CircularQueue.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Circular Queue implementation in C#
 ********************************************************/

namespace DataStructures.Queues.CircularQueue
{
    /// <summary>
    /// Circular Queue Class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class CircularQueue<T>
    {
        /// <summary>
        /// Private Members
        /// </summary>
        private T[] _array;
        private int _count;
        private int _size;
        private int _zeroIndex;
       
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CircularQueue()
        {
            _array = new T[_size = 10];
            _count = _zeroIndex = 0;
        }

        /// <summary>
        /// Circular Queue Constructor
        /// </summary>
        /// <param name="size">Size to initialize the queue to</param>
        public CircularQueue(int size)
        {
            _array = new T[_size = size];
            _count = _zeroIndex = 0;
        }

        /// <summary>
        /// Pushes given data onto the queue if space is available
        /// </summary>
        /// <param name="data">Data to be added to the queue</param>
        /// <returns>Node added to the queue</returns>
        public T Enqueue(T data)
        {
            if (!IsFull())
            {
                _array[(_zeroIndex + _count) % _size] = data;
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

            T tmp = _array[_zeroIndex];
            _array[_zeroIndex] = default(T);
            _count--;
            _zeroIndex = (_zeroIndex + 1) % _size;
            return tmp;
        }

        /// <summary>
        /// Gets the top item of the queue
        /// </summary>
        /// <returns>item on top of the queue</returns>
        public T Top() => IsEmpty() ? default(T) : _array[_zeroIndex];

        /// <summary>
        /// Returns a value indicating if the queue is empty
        /// </summary>
        /// <returns>true if empty, false if not</returns>
        public bool IsEmpty() => _count == 0; 

        /// <summary>
        ///  * Returns a value indicating if the queue is full
        /// </summary>
        /// <returns>True if full, false if not</returns>
        public bool IsFull() => _count == _size;
    }
}
