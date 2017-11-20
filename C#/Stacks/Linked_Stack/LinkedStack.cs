/*******************************************************
 *  LinkedStack.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Stack implementation in C#
 ********************************************************/

namespace DataStructures.Stacks.LinkedStack
{
    /// <summary>
    /// Linked Stack Class
    /// </summary>
    /// <typeparam name="T"> Generic Type</typeparam>
    public class LinkedStack<T>
    {
        /// <summary>
        /// Node Class for Linked Stack
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public accessors for Node class
            /// </summary>
            public T Data { set; get; }

            public Node Next { set; get; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="data">Data for the node to hold</param>
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

        /// <summary>
        /// Linked Stack Constructor
        /// </summary>
        public LinkedStack()
        {
            _count = 0;
            _head = null;
        }

        /// <summary>
        /// Pushes given data onto the stack
        /// </summary>
        /// <param name="data">Data to be added to the stack</param>
        /// <returns>Node added to the stack</returns>
        public Node Push(T data)
        {
            Node node = new Node(data);
            node.Next = _head;
            _head = node;
            _count++;
            return Top();
        }

        /// <summary>
        /// Pops item off the stack
        /// </summary>
        /// <returns>Node popped off of the stack</returns>
        public Node Pop()
        {
            Node node = _head;
            _head = _head.Next;
            node.Next = null;
            _count--;
            return node;
        }

        /// <summary>
        /// Gets the Node onto of the stack
        /// </summary>
        /// <returns>Node on top of the stack</returns>
        public Node Top() => _head;

        /// <summary>
        /// Returns a value indicating if the stack is empty
        /// </summary>
        /// <returns>true if empty, false if not</returns>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Returns a value indicating if the stack is full
        /// </summary>
        /// <returns>False, Linked stack is never full</returns>
        public bool IsFull() => false;
    }
}
