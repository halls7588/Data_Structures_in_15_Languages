/*******************************************************
 *  Linkedset.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Linked Set implementation in C#
 ********************************************************/
using System;


namespace DataStructures.Lists.LinkedSet
{    
    /// <summary>
    /// Linked set class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class LinkedSet<T> where T : IComparable
    {
        /// <summary>
        /// Node class for singly linked set
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Public accessors for node class
            /// </summary>
            public T Data { set; get; }
            public Node Next { set; get; }

            /// <summary>
            /// Node Class Constructor
            /// </summary>
            /// <param name="data">Data to be held in the Node</param>
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        /// <summary>
        /// Private Members
        /// </summary>
        private Node _head;
        private Node _tail;
        private int _count;

        /// <summary>
        /// Linked Set Constructor
        /// </summary>
        public LinkedSet()
        {
            _head = _tail = null;
            _count = 0;
        }
        
        /// <summary>
        /// Adds a new node into the list with the given data
        /// </summary>
        /// <param name="data">Data to add into the list</param>
        /// <returns>Node added into the list</returns>
        public Node Add(T data)
        {
            // No data to insert into list
            if (data == null || (Find(data) != null))
                return null;

            Node node = new Node(data);
            // The Linked list is empty
            if (_head == null)
            {
                _head = node;
                _tail = _head;
                _count++;
            }
            else
            {
                // Add to the end of the list
                _tail.Next = node;
                _tail = node;
                _count++;
            }
            return node;
        }

        /// <summary>
        /// Removes the first node in the list matching the data
        /// </summary>
        /// <param name="data">Data to remove from the list</param>
        /// <returns>Node removed from the list</returns>
        public Node Remove(T data)
        {
            // List is empty or no data to remove
            if (_head == null || data == null)
                return null;

            Node tmp = _head;
            // The data to remove what found in the first Node in the list
            if (tmp.Data.Equals(data))
            {
                _head = _head.Next;
                _count--;
                return tmp;
            }
            // Try to find the node in the list
            while (tmp.Next != null)
            {
                // Node was found, Remove it from the list
                if (tmp.Next.Data.Equals(data))
                {
                    Node node = tmp.Next;
                    tmp.Next = tmp.Next.Next;
                    _count--;
                    return node;
                }
                tmp = tmp.Next;
            }
            // The data was not found in the list
            return null;
        }

        /// <summary>
        /// Gets the first node that has the given data
        /// </summary>
        /// <param name="data">Data to find in the list</param>
        /// <returns>First node with matching data or null if no node was found</returns>
        public Node Find(T data)
        {
            // No list or data to find
            if (_head == null || data == null)
                return null;

            Node tmp = _head;
            // Try to find the data in the list
            while (tmp != null)
            {
                // Data was found
                if (tmp.Data.Equals(data))
                    return tmp;
                tmp = tmp.Next;
            }
            // Data was not found in the list
            return null;
        }

        /// <summary>
        /// Gets the node at the given index
        /// </summary>
        /// <param name="index">Index of the Node to get</param>
        /// <returns>Node at passed in index</returns>
        public Node IndexAt(int index)
        {
            //Index was negative or larger then the amount of Nodes in the list
            if (index < 0 || index > Size())
                return null;

            Node tmp = _head;
            // Move to index
            for (int i = 0; i < index; i++)
                tmp = tmp.Next;
            // return the node at the index position
            return tmp;
        }

        /// <summary>
        /// Gets the current size of the list
        /// </summary>
        /// <returns>Number of items in the list</returns>
        public int Size() => _count;
    }
}
