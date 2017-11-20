/*******************************************************
 *  LinkedQueue.cs
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Queue implementation in C#
 ********************************************************/

namespace DataStructures.Queues.LinkedQueue
{  
    /// <summary>
    /// Linked Queue Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class LinkedQueue<T>
    {
        /// <summary>
        /// Node Class
        /// </summary>
        public class Node
        {
           
            /// <summary>
            /// Public accessors for the node class
            /// </summary>
            public T Data { set; get; }
            public Node Next { set; get; }

            /// <summary>
            /// Node Class Constructor
            /// </summary>
            /// <param name="data">Data to be held in the node</param>
            public Node(T data)
            {
                Data = data;
                Next = null;
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
        public LinkedQueue()
        {
            _count = 0;
            _head = _tail = null;
        }

        /// <summary>
        /// Adds given data onto the queue
        /// </summary>
        /// <param name="data">Data to be added to the queue</param>
        /// <returns>Node added to the queue</returns>
        public Node Enqueue(T data)
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
            _tail = node;
            _count++;
            return node;
        }

        /// <summary>
        /// Removes item off the queue
        /// </summary>
        /// <returns>Node popped off of the queue</returns>
        public Node Dequeue()
        {
            Node node = _head;
            _head = _head.Next;
            node.Next = null;
            _count--;
            return node;
        }

        /// <summary>
        /// Gets the Node onto of the queue
        /// </summary>
        /// <returns>Node on top of the queue</returns>
        public Node Top() => _head;

        /// <summary>
        /// Returns a value indicating if the queue is empty
        /// </summary>
        /// <returns>true if empty, false if not</returns>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Returns a value indicating if the queue is full
        /// </summary>
        /// <returns>False, Linked queue is never full</returns>
        public bool IisFull() => false;
    }

}
