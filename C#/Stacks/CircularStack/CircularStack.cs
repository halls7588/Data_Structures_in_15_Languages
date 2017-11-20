/*******************************************************
 *  CircularStack.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Circular Stack implementation in C#
 ********************************************************/

namespace DataStructures.Stacks.CircularStack
{ 
    /// <summary>
    /// Circular Stack Class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class CircularStack<T>
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
        public CircularStack()
        {
            _array = new T[_size = 10];
            _count = _zeroIndex = 0;
        }

        /// <summary>
        /// Circular Stack Constructor
        /// </summary>
        /// <param name="size">Size to initialize the stack to</param>
        public CircularStack(int size)
        {
            _array = new T[_size = size];
            _count = _zeroIndex = 0;
        }

        /// <summary>
        /// Pushes given data onto the stack if space is available
        /// </summary>
        /// <param name="data">Data to be added to the stack</param>
        /// <returns>item added to the stack</returns>
        public T Push(T data)
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
        /// Pops item off the stack
        /// </summary>
        /// <returns>item popped off of the stack</returns>
        public T Pop()
        {
            if (IsEmpty())
                return default(T);
            T data = _array[(_count + _zeroIndex % _size) - 1];
            _array[(_count + _zeroIndex % _size) - 1] = _array[_zeroIndex];
            _array[_zeroIndex] = default(T);
            _count--;
            _zeroIndex = (_zeroIndex + 1) % _size;
            return data;
        }

        /// <summary>
        /// Gets the item onto of the stack
        /// </summary>
        /// <returns>item on top of the stack</returns>
        public T Top() => _array[((_zeroIndex + _count) % _size) - 1];

        /// <summary>
        /// Returns a value indicating if the stack is empty
        /// </summary>
        /// <returns>true if empty, false if not</returns>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Returns a value indicating if the stack is full
        /// </summary>
        /// <returns>True if full, false if not</returns>
        public bool IsFull() => _count == _size;
    }
    
}
