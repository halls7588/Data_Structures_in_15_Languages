/*******************************************************
 *  ArrayedStack.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Stack implementation in C#
 ********************************************************/

namespace DataStructures.Stacks.ArrayedStack
{
    /// <summary>
    ///  Arrayed Stack Class
    /// </summary>
    /// <typeparam name="T"> Generic type</typeparam>
    public class ArrayedStack<T>
    {
        /// <summary>
        /// Private Members
        /// </summary>
        private T[] _array;
        private int _count;
        private int _size;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ArrayedStack()
        {
            _array = new T[_size = 10];
            _count = 0;
        }

        /// <summary>
        /// Arrayed Stack Constructor
        /// </summary>
        /// <param name="size">Size to initialize the stack to</param>
        public ArrayedStack(int size)
        {
            _array = new T[_size = size];
            _count = 0;
        }

        /// <summary>
        /// Pushes given data onto the stack if space is available
        /// </summary>
        /// <param name="data">Data to be added to the stack</param>
        /// <returns>Item added to the stack</returns>
        public T Push(T data)
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
        /// Pops item off the stack
        /// </summary>
        /// <returns>Item popped off of the stack</returns>
        public T Pop()
        {
            T data = _array[_count - 1];
            _count--;
            return data;
        }

        /// <summary>
        /// Gets the Item onto of the stack
        /// </summary>
        /// <returns>Item on top of the stack</returns>
        public T Top() =>  _array[_count - 1];

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
