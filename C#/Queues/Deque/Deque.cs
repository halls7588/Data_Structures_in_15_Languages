/*******************************************************
 *  Deque.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Deque implementation in C#
 ********************************************************/

namespace DataStructures.Queues.Deque
{   
    /// <summary>
    /// Linked Queue Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class Deque<T>
    {
        /// <summary>
        /// Node Class for Linked Queue
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Public accessors for the node class 
            /// </summary>
            public T Data { set; get; }
            public Node Next { set; get; }
            public Node Previous { set; get; }

            /// <summary>
            /// Node Class Constructor
            /// </summary>
            /// <param name="data">Data to be held in the node</param>
            public Node(T data)
            {
                Data = data;
                Next = null;
                Previous = null;
            }
        }

        /// <summary>
        /// Private Members
        /// </summary>
        private int _count;
        private Node _head;
        private Node _tail;

        /// <summary>
        /// Linked Queue Constructor
        /// </summary>
        public Deque()
        {
            _count = 0;
            _head = _tail = null;
        }

        /// <summary>
        /// Adds given data onto the front of the deque
        /// </summary>
        /// <param name="data">Data to be added to the deque</param>
        /// <returns>Node added to the deque</returns>
        public Node EnqueueFront(T data)
        {
            Node node;
            if (IsEmpty())
            {
                 node = new Node(data);
                _head = _tail = node;
                _count++;
                return node;
            }
            node = new Node(data);
            node.Next = _head;
            _head.Previous = node;
            _head = node;
            _count++;
            return node;
        }

        /// <summary>
        /// Adds given data onto the back of the deque
        /// </summary>
        /// <param name="data">Data to be added to the deque</param>
        /// <returns>Node added to the deque</returns>
        public Node EnqueueBack(T data)
        {
            Node node;
            if (IsEmpty())
            {
                node = new Node(data);
                _head = _tail = node;
                _count++;
                return node;
            }
            node = new Node(data);
            node.Next = _tail;
            _tail.Previous = node;
            _tail = node;
            _count++;
            return node;
        }

        /// <summary>
        /// Removes item off the front of the deque
        /// </summary>
        /// <returns>Node popped off of the deque</returns>
        public Node DequeueFront()
        {
            if (IsEmpty())
                return null;
            Node node = _head;
            _head = _head.Next;
            _head.Previous = null;
            node.Next = null;
            _count--;
            return node;
        }

        /// <summary>
        /// Removes item off the back of the deque
        /// </summary>
        /// <returns>Node removes from the back of the deque</returns>
        public Node DequeueBack()
        {
            if (IsEmpty())
                return null;
            Node node = _tail;
            _tail = _tail.Previous;
            _tail.Next = null;
            node.Previous = null;
            _count--;
            return node;
        }

        /// <summary>
        /// Gets the Node at the front of the deque
        /// </summary>
        /// <returns>Node on top of the deque</returns>
        public Node PeekFront() => _head;

        /// <summary>
        /// Gets the Node at the back of the deque
        /// </summary>
        /// <returns>Node on bottom of the deque</returns>
        public Node PeekBack() => _tail;

        /// <summary>
        /// Returns a value indicating if the deque is empty
        /// </summary>
        /// <returns>rue if empty, false if not</returns>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Returns a value indicating if the deque is full
        /// </summary>
        /// <returns>False, deque is never full</returns>
        public bool IsFull() => false; 
    }
}
