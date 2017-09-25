/*******************************************************
 *  ArrayedStack.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Stack implementation in C#
 ********************************************************/

namespace DataStructures
{
    /// <summary>
    /// Arrayed Stack Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class ArrayedStack<T>
    {
        /// <summary>
        /// Private members
        /// </summary>
        private T[] array;
        private int count;
        private int size;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ArrayedStack()
        {
            array = new T[(size = 10)];
            count = 0;
        }
       
        /// <summary>
        /// Arrayed Stack Constructor
        /// </summary>
        /// <param name="size">Size to initialize the stack to</param>
        public ArrayedStack(int size)
        {
            array = new T[(this.size = size)];
            count = 0;
        }
        
        /// <summary>
        /// Pushes given data onto the stack if space is available
        /// </summary>
        /// <param name="data">Data to be added to the stack</param>
        /// <returns>Node added to the stack</returns>
        public T Push(T data)
        {
            if (!IsFull())
            {
                array[count] = data;
                count++;
                return Top();
            }
            return default(T);
        }
        
        /// <summary>
        /// Pops the first item off the stack
        /// </summary>
        /// <returns>Node popped off of the stack</returns>
        public T Pop()
        {
            T data = array[count - 1];
            count--;
            return data;
        }
        
        /// <summary>
        /// Gets the Node onto of the stack without removing it
        /// </summary>
        /// <returns>Node on top of the stack</returns>
        public T Top()
        {
            return array[count - 1];
        }
       
        /// <summary>
        /// Returns a value indicating if the stack is empty
        /// </summary>
        /// <returns>True if empty, false if not</returns>
        public bool IsEmpty()
        {
            return (count == 0);
        }

        /// <summary>
        /// Returns a value indicating if the stack is full
        /// </summary>
        /// <returns>True if full, false if not</returns>
        public bool IsFull()
        {
            return (count == size);
        }
    }

}
