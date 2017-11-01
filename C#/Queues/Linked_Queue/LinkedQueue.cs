/*******************************************************
 *  LinkedQueue.cs
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Queue implementation in C#
 ********************************************************/
namespace DataStructures
{
    /// <summary>
    /// Linked Queue Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    class LinkedQueue<T>
    {
        /// <summary>
        /// Node Class For Linked Queue
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        public class Node<T>
        {
            /// <summary>
            /// Private members of the Node Class
            /// </summary>
            private T data;
            private Node<T> next;

            /// <summary>
            /// Public Property for the Node data Member
            /// </summary>
            public T Data
            {
                set { data = value; }
                get { return data; }
            }

            /// <summary>
            /// Public property for the Node Next Member
            /// </summary>
            public Node<T> Next
            {
                set { next = value; }
                get { return next; }
            }

            /// <summary>
            /// ode Class Constructor
            /// </summary>
            /// <param name="data">Data to be held in the node</param>
            public Node(T data)
            {
                this.data = data;
                next = null;
            }
        }

        /// <summary>
        /// Private members of the Linked Queue Class
        /// </summary>
        private int count;
        private Node<T> head;
        private Node<T> tail;

        /// <summary>
        /// Linked Queue class constructor
        /// </summary>
        public LinkedQueue()
        {
            count = 0;
            head = null;
        }

        /// <summary>
        /// Adds given data onto the Queue
        /// </summary>
        /// <param name="data">Data to be added to the queue</param>
        /// <returns>Node added to the queue</returns>
        public Node<T> Enqueue(T data)
        {
            if (IsEmpty())
            {
                Node<T> node = new Node<T>(data);
                head = tail = node;
                count++;
                return node;
            }
            else
            {
                Node<T> node = new Node<T>(data);
                node.Next = tail;
                tail = node;
                count++;
                return node;
            }
        }

        /// <summary>
        /// removes item off the queue
        /// </summary>
        /// <returns>Node removed off of the queue</returns>
        public Node<T> Dequeue()
        {
            Node<T> node = head;
            head = head.Next;
            node.Next = null;
            count--;
            return node;
        }

        /// <summary>
        /// Gets the first Node onto of the queue without removing it
        /// </summary>
        /// <returns>Node on top of the queue</returns>
        public Node<T> Top()
        {
            return head;
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
        /// <returns>False, Linked queue is never full</returns>
        public bool IsFull()
        {
            return false;
        }
    }
}
