/*******************************************************
 *  ArrayedSet.cs
 *  Created by Stephen Hall on 11/17/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Set implementation in C#
 ********************************************************/
using System;
using System.Linq;

namespace DataStructures.Arrays.ArrayedSet
{
    /// <summary>
    /// ArrayedSet Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class ArrayedSet<T>
    {
        private T[] _array;
        private int _count;
        private int _size;
        public int Count => _count;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArrayedSet()
        {
            _count = 0;
            _array = new T[_size = 4];
        }

        /// <summary>
        /// Constructor Initialized to given size
        /// </summary>
        /// <param name="size">Size to initialize array to</param>
        public ArrayedSet(int size)
        {
            _count = 0;
            _size = size > 0 ? size : 4;
            _array = new T[_size];
        }
        /// <summary>
        /// Doubles the size of the internal array
        /// </summary>
        private void Resize()
        {
            _size *= 2;
            T[] tmp = new T[_size];
            Array.Copy(_array, tmp, _count);
            _array = tmp;
        }

        /// <summary>
        /// Adds new item into the array
        /// </summary>
        /// <param name="data">Data to add into the arrayo</param>
        /// <returns>Data added into the array</returns>
        public T Add(T data)
        {
            if (data == null)
                return default(T);

            if (Contains(data))
                return default(T);

            if (_count == _size)
                Resize();

            _array[_count] = data;
            _count++;
            return data;
        }

        /// <summary>
        /// Appends the contents of an array to the ArrayedSet
        /// </summary>
        /// <param name="data">array to append</param>
        /// <returns>success|fail</returns>
        public bool Append(T[] data)
        {
            if (data == null)
                return false;
            foreach(T aData in data)
            {
                if (aData != null)
                    Add(aData);
            }
            return true;
        }

        /// <summary>
        /// Sets the data at the given index
        /// </summary>
        /// <param name="index">index to set</param>
        /// <param name="data">data to set index to</param>
        /// <returns>success|fail</returns>
        public bool Set(int index, T data)
        {
            if (!Contains(data) && (index >= 0 && index < _size))
            {
                _array[index] = data;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the data at the arrays given index
        /// </summary>
        /// <param name="index">Index to get data at</param>
        /// <returns>Data at the given index or default(T)</returns>
        public T Get(int index)
        {
            return index >= 0 && index < _size ? _array[index] : default(T);
        }

        /// <summary>
        /// Removes the data at arrays given index
        /// </summary>
        /// <param name="index">Index to remove</param>
        /// <returns> Data removed from the array or default(T)</returns>
        public T Remove(int index)
        {
            if (index < 0 || index > _count)
                return default(T);

            T tmp = _array[index];
            _array[index] = default(T);
            _count--;
            return tmp;
        }

        /// <summary>
        /// Resets the internal array to default size with no data
        /// </summary>
        public void Reset()
        {
            _count = 0;
            _size = 4;
            _array = new T[_size];
        }

        /// <summary>
        /// Clears all data in the array leaving size intact
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _count; i++)
            {
                _array[i] = default(T);
            }
            _count = 0;
        }

        /// <summary>
        /// Tests to see if the data exist in the list
        /// </summary>
        /// <param name="data">data to find</param>
        /// <returns>success|fail</returns>
        private bool Contains(T data) => _array.Contains(data);
    }
}
