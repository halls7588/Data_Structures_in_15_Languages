/*******************************************************
 *  LinkedStack.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Stack implementation in C#
 ********************************************************/

namespace DataStructures
{
    /// <summary>
    /// Linked Stack Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class LinkedStack<T>
    {
        /// <summary>
        /// Node Class For Linked Stack
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
        /// Private members of the Linked Stack Class
        /// </summary>
        private int count;
        private Node<T> head;

        /// <summary>
        /// Linked Stack class constructor
        /// </summary>
        public LinkedStack()
        {
            count = 0;
            head = null;
        }

        /// <summary>
        /// Pushes given data onto the stack
        /// </summary>
        /// <param name="data">Data to be added to the stack</param>
        /// <returns>Node added to the stack</returns>
        public Node<T> Push(T data)
        {
            Node<T> node = new Node<T>(data);
            node.Next = head;
            head = node;
            count++;
            return Top();
        }

        /// <summary>
        /// Pops item off the stack
        /// </summary>
        /// <returns>Node popped off of the stack</returns>
        public Node<T> Pop()
        {
            Node<T> node = head;
            head = head.Next;
            node.Next = null;
            count--;
            return node;
        }

        /// <summary>
        /// Gets the first Node onto of the stack without removing it
        /// </summary>
        /// <returns>Node on top of the stack</returns>
        public Node<T> Top()
        {
            return head;
        }

        /// <summary>
        /// Returns a value indicating if the stack is empty
        /// </summary>
        /// <returns>True if empty, false if not</returns>
        public bool IsEmpty()
        {
            return (count == 0);
        }

        /// <summary>
        /// Returns a value indicating if the stack is full
        /// </summary>
        /// <returns>False, Linked stack is never full</returns>
        public bool IsFull()
        {
            return false;
        }
    }
}
