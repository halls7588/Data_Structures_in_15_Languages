/*******************************************************
 *  ArrayedHeap.cs
 *  Created by Stephen Hall on 11/17/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Heap implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Heaps.ArrayedHeap
{

    /// <summary>
    /// ArrayedHeap class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class ArrayedHeap<T> where T : IComparable
    {
        /// <summary>
        /// private members to be used by the heap
        /// </summary>
        private T[] _array;
        private int _size;

        /// <summary>
        /// ArrayedHeap Constructor.
        /// </summary>
        public ArrayedHeap()
        {
            _array = new T[10];
            _size = 0;
        }

        /// <summary>
        /// Adds an item into the heap
        /// </summary>
        /// <param name="value">value added to the heap</param>
        public void Add(T value)
        {
            // grow array if needed
            if (_size >= _array.Length - 1)
                _array = Resize();
            // place element into heap at bottom
            _array[_size++] = value;
            BubbleUp();
        }

        /// <summary>
        /// checks if the heap is empty
        /// </summary>
        /// <returns>true|false</returns>
        public bool IsEmpty() => _size == 0;

        /// <summary>returns the first item on the heap
        /// 
        /// </summary>
        /// <returns>first element of the heap</returns>
        public T Peek() => IsEmpty() ? default(T) : _array[1];

        /// <summary>
        /// Removes and returns the minimum element in the heap.
        /// </summary>
        /// <returns>element removed</returns>
        public T Remove()
        {
            T result = Peek();
            // get rid of the last element
            _array[1] = _array[_size];
            _array[_size] = default(T);
            _size--;
            BubbleDown();
            return result;
        }
        
        /// <summary>
        /// Gets the size of the heap
        /// </summary>
        /// <returns>number of elements in the heap</returns>
        public int Size() => _size;

        /// <summary>
        /// Bubbles down the root element to the correct placement
        /// </summary>
        private void BubbleDown()
        {
            int index = 1;

            while (HasLeftChild(index))
            {
                int smallerChild = LeftIndex(index);
                if (HasRightChild(index) && _array[LeftIndex(index)].CompareTo(_array[RightIndex(index)]) > 0)
                    smallerChild = RightIndex(index);
                if (_array[index].CompareTo(_array[smallerChild]) > 0)
                    Swap(index, smallerChild);
                else
                    break;
                index = smallerChild;
            }
        }

        /// <summary>
        /// Performs bubble up to place new element in correct position
        /// </summary>
        private void BubbleUp()
        {
            int index = _size;
            while (HasParent(index) && Parent(index).CompareTo(_array[index]) > 0)
            {
                Swap(index, ParentIndex(index));
                index = ParentIndex(index);
            }
        }

        /// <summary>
        /// Determines if the given index has a parent
        /// </summary>
        /// <param name="i">index to test</param>
        /// <returns>true|false</returns>
        private static bool HasParent(int i) => i > 1;

        /// <summary>
        /// Gets the index of the left child
        /// </summary>
        /// <param name="i">index to test</param>
        /// <returns>left child index</returns>
        private static int LeftIndex(int i) => i * 2;

        /// <summary>
        /// Gets the index of the right child of given index
        /// </summary>
        /// <param name="i">index to test</param>
        /// <returns>index of right child</returns>
        private static int RightIndex(int i) => i * 2 + 1;

        /// <summary>
        /// Determines if element has a left child or
        /// </summary>
        /// <param name="i">index to test</param>
        /// <returns>true|false</returns>
        private bool HasLeftChild(int i) => LeftIndex(i) <= _size;

        /// <summary>
        /// Determines if element has a right child or
        /// </summary>
        /// <param name="i">index to test</param>
        /// <returns>true|false</returns>
        private bool HasRightChild(int i) => RightIndex(i) <= _size;

        /// <summary>
        ///  gets the data of the parent
        /// </summary>
        /// <param name="i">child index</param>
        /// <returns>data of the parent index</returns>
        private T Parent(int i) => _array[ParentIndex(i)];

        /**
         * Gets the parent index of i
         * @param i: index to get parent of
         * @return int: parent index
         */
        private static int ParentIndex(int i) => i / 2;
        
        /// <summary>
        /// Doubles the size of the internal array
        /// </summary>
        /// <returns>new resized array with copy of the data</returns>
        private T[] Resize()
        {
            T[] arr = new T[_array.Length * 2];
            Array.Copy(_array, arr, _array.Length * 2);
            return _array = arr;
        }

        /// <summary>
        /// Swaps the data at index a with b
        /// </summary>
        /// <param name="a">first index to swap</param>
        /// <param name="b">second index to swap</param>
        private void Swap(int a, int b)
        {
            T tmp = _array[a];
            _array[a] = _array[b];
            _array[b] = tmp;
        }
    }
}
