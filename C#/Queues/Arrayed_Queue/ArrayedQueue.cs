/*******************************************************
 *  ArrayedQueue.cs
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Queue implementation in C#
 ********************************************************/

namespace DataStructures
{
    class ArrayedQueue<T>
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
        public ArrayedQueue()
        {
            array = new T[(size = 10)];
            count = 0;
        }

        /// <summary>
        /// Arrayed queue Constructor
        /// </summary>
        /// <param name="size">Size to initialize the queue to</param>
        public ArrayedQueue(int size)
        {
            array = new T[(this.size = size)];
            count = 0;
        }

        /// <summary>
        /// Add given data onto the queue if space is available
        /// </summary>
        /// <param name="data">Data to be added to the queue</param>
        /// <returns>item added to the queue</returns>
        public T Enqueue(T data)
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
        /// Removed the first item off the queue
        /// </summary>
        /// <returns>item removed off of the queue</returns>
        public T Dequeue()
        {
            if (IsEmpty())
                return default(T);

            T data = array[0];
            T[] tmp = new T[(this.size)];
            for (int i = 1; i < size - 1; i++)
            {
                tmp[i - 1] = array[i];
            }
            array = tmp;
            count--;
            return data;
        }

        /// <summary>
        /// Gets the item onto of the queue without removing it
        /// </summary>
        /// <returns>Node on top of the queue</returns>
        public T Top()
        {
            if (IsEmpty())
                return default(T);
            return array[0];
        }

        /// <summary>
        /// Returns a value indicating if the queue is empty
        /// </summary>
        /// <returns>True if empty, false if not</returns>
        public bool IsEmpty()
        {
            return (count == 0);
        }

        /// <summary>
        /// Returns a value indicating if the queue is full
        /// </summary>
        /// <returns>True if full, false if not</returns>
        public bool IsFull()
        {
            return (count == size);
        }
    }
}
}
