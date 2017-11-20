/*******************************************************
 *  SkipList.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Skip List implementation in C#
 ********************************************************/
using System;
using System.Collections.Generic;

namespace DataStructures.Lists.SkipList
{ 
    /// <summary>
    /// Skip list class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class SkipList<T> where T : IComparable
    {
        /// <summary>
        /// Node for Skip List class
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public members of the node class
            /// </summary>
            public T Data;
            public List<Node> NodeList;
            
            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="data">data for the node to hold</param>
            public Node(T data)
            {
                Data = data;
                NodeList = new List<Node>();
            }
           
            /// <summary>
            /// Gets the level of the Node
            /// </summary>
            /// <returns>level of the node</returns>
            public int Level() => NodeList.Count - 1;
        }

        /// <summary>
        /// Private members of the Skip list class
        /// </summary>
        private Node _head;
        private int _max;
        private int _size;
        private const double PROBABILITY = 0.5;
        private readonly Random _rnd;

        /// <summary>
        /// Skip List Constructor
        /// </summary>
        public SkipList()
        {
            _size = 0;
            _max = 0;
            _rnd = new Random(DateTime.Now.Second);
            _head = new Node(default(T)); // a Node with value null marks the beginning
            _head.NodeList.Add(null); // null marks the end
        }

        /// <summary>
        /// Adds a node into the Skip List
        /// </summary>
        /// <param name="data">data to add into the list</param>
        /// <returns>success|fail</returns>
        public bool Add(T data)
        {
            if (Contains(data))
                return false;

            _size++;
            int level = 0;
            // random number from 0 to max + 1 (inclusive)
            while (_rnd.NextDouble() < PROBABILITY)
                level++;
            while (level > _max)
            {
                // should only happen once
                _head.NodeList.Add(null);
                _max++;
            }

            Node node = new Node(data);
            Node current = _head;

            do
            {
                current = FindNext(data, current, level);
                node.NodeList[0] = current.NodeList[level];
                current.NodeList[level] = node;
            } while (level-- > 0);
            return true;
        }

        /// <summary>
        /// Finds a node in the list with the same data
        /// </summary>
        /// <param name="data">data to find</param>
        /// <returns>Node found</returns>
        public Node Find(T data) => Find(data, _head, _max);

        /// <summary>
        /// Returns node with the greatest value
        /// </summary>
        /// <param name="data">data to find</param>
        /// <param name="current">current Node</param>
        /// <param name="level">level to start form</param>
        /// <returns>current node</returns>
        private Node Find(T data, Node current, int level)
        {
            do
            {
                current = FindNext(data, current, level);
            } while (level-- > 0);
            return current;
        }

        /// <summary>
        /// Returns the node at a given level with highest value less than data
        /// </summary>
        /// <param name="data">data to find</param>
        /// <param name="current">current node</param>
        /// <param name="level">current level</param>
        /// <returns>highest node</returns>
        private Node FindNext(T data, Node current, int level)
        {
            Node next = current.NodeList[level];

            while (next != null)
            {
                T value = next.Data;
                if (LessThan(data, value))
                    break;
                current = next;
                next = current.NodeList[level];
            }
            return current;
        }

        /// <summary>
        /// gets the size of the list
        /// </summary>
        /// <returns>size of the list</returns>
        public int Size() => _size;

        /// <summary>
        /// Determines if the object is in the list or not
        /// </summary>
        /// <param name="data">object to test</param>
        /// <returns>true|false</returns>
        public bool Contains(T data)
        {
            Node node = Find(data);
            return node != null && !node.Data.Equals(default(T)) && EqualTo(node.Data, data);
        }

        /// <summary>
        /// Determines if a is less than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool LessThan(T a, T b) => a.CompareTo(b) < 0;

        /// <summary>
        /// Determines if a is equal to b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool EqualTo(T a, T b) => a.CompareTo(b) == 0;
    }
}
