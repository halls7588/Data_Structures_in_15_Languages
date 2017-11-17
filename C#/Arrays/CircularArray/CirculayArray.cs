/*******************************************************
*  CircularArray.cs
*  Created by Stephen Hall on 9/22/17.
*  Copyright (c) 2017 Stephen Hall. All rights reserved.
*  A Circular Array implementation in C#.Net
********************************************************/

namespace DataStructures.Arrays.CircularArray
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
        private T[] _array;
	    private int _size;
	    private int _zeroIndex;
	    private int _count;

        /// <summary>
        /// Default Circulay Array class constructor
        /// </summary>
        public CirculayArray()
        {
            _size = 10;
            _array = new T[_size];
            _zeroIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Circulay Array class constructor
        /// </summary>
        /// <param name="size">Size to initialize array to</param>
        public CirculayArray(int size)
        {
            _size = size;
            _array = new T[size];
            _zeroIndex = 0;
            _count = 0;
        }

        /// <summary>
        /// Adds new item into the array
        /// </summary>
        /// <param name="data">Data to add into the array</param>
        /// <returns>Data added into the array</returns>
        public T Add(T data)
        {
            int tmp = (_zeroIndex + _count) % _size;
            _array[tmp] = data;
            if (((_count + 1) / _size) >= 1)
                Resize();
            _count++;
            return _array[tmp];
        }

        /// <summary>
        /// Gets the data at the arrays given index
        /// </summary>
        /// <param name="index">Index to get data at</param>
        /// <returns>Data at the given index or default value of T if index does not exist</returns>
        public T DataAt(int index)
        {
            if ((index + _zeroIndex) % _size < _count && _array[(index + _zeroIndex) % _size] != null)
            {
                return (_array[index + _zeroIndex % _size]);
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
            if (index > _size)
                return default(T);

            T tmp = _array[(index + _zeroIndex % _size)];
            _array[(index + _zeroIndex % _size)] = _array[_zeroIndex];
            _array[_zeroIndex] =  default(T);
            _count--;
            _zeroIndex = (_zeroIndex + 1) % _size;
            return tmp;
        }

        /// <summary>
        /// Gets the current count of the array
        /// </summary>
        /// <returns>Number of items in the array</returns>
        public int Count()
        {
            return _count;
        }

        /// <summary>
        /// Private method to resize the array if capacity has been reached
        /// </summary>
        private void Resize()
        {
            _size = _size * 2;
            T[] arr = new T[_size];
            for(int i = 0; i < _array.Length; i++)
            {
                arr[i] = _array[i];
            }
            _array = arr;
        }
    }
}
