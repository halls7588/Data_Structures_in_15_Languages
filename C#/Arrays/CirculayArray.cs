/*******************************************************
*  CircularArray.cs
*  Created by Stephen Hall on 9/22/17.
*  Copyright (c) 2017 Stephen Hall. All rights reserved.
*  A Circular Array implementation in C#.Net
********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// Circulay Array class 
    /// </summary>
    /// <typeparam name="T">Generis type to be used</typeparam>
    class CirculayArray<T>
    {
        /// <summary>
        /// Private members
        /// </summary>
        private T[] array;
	    private int size;
	    private int zeroIndex;
	    private int count;

        /// <summary>
        /// Default Circulay Array class constructor
        /// </summary>
        public CirculayArray()
        {
            size = 10;
            array = new T[size];
            zeroIndex = 0;
            count = 0;
        }

        /// <summary>
        /// Circulay Array class constructor
        /// </summary>
        /// <param name="size">Size to initialize array to</param>
        public CirculayArray(int size)
        {
            this.size = size;
            array = new T[size];
            zeroIndex = 0;
            count = 0;
        }

        /// <summary>
        /// Adds new item into the array
        /// </summary>
        /// <param name="data">Data to add into the array</param>
        /// <returns>Data added into the array</returns>
        public T Add(T data)
        {
            int tmp = (zeroIndex + count) % size;
            array[tmp] = data;
            if (((count + 1) / size) >= 1)
            {
                Resize();
            }
            count++;
            return array[tmp];
        }

        /// <summary>
        /// Gets the data at the arrays given index
        /// </summary>
        /// <param name="index">Index to get data at</param>
        /// <returns>Data at the given index or default value of T if index does not exist</returns>
        public T DataAt(int index)
        {
            if ((index + zeroIndex) % size < count && array[(index + zeroIndex) % size] != null)
            {
                return (array[index + zeroIndex % size]);
            }
            return default(T);
        }

        /// <summary>
        /// Removes the data at arrays given index
        /// </summary>
        /// <param name="index">Index to remove</param>
        /// <returns>Data removed from the array or default T value if index does not exist</returns>
        public T Remove(int index)
        {
            if (index > size)
                return default(T);

            T tmp = array[(index + zeroIndex % size)];
            array[(index + zeroIndex % size)] = array[zeroIndex];
            array[zeroIndex] =  default(T);
            count--;
            zeroIndex = (zeroIndex + 1) % size;
            return tmp;
        }

        /// <summary>
        /// Gets the current count of the array
        /// </summary>
        /// <returns>Number of items in the array</returns>
        public int Count()
        {
            return count;
        }

        /// <summary>
        /// Private method to resize the array if capacity has been reached
        /// </summary>
        private void Resize()
        {
            size = size * 2;
            T[] arr = new T[size];
            for(int i = 0; i < array.Length; i++)
            {
                arr[i] = array[i];
            }
            array = arr;
        }
    }
}
