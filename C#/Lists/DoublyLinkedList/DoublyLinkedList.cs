/*******************************************************
 *  DoublyLinkedList.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked List implementation in C#
 ********************************************************/

namespace DataStructures
{
    /// <summary>
    /// Doubly linked List Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedList<T>
    {
        /// <summary>
        /// Node class for Doubly linked list
        /// </summary>
        /// <typeparam name="T"Ggeneric type</typeparam>
        public class Node<T>
        {
           /// <summary>
           /// Private Member of the Node class
           /// </summary>
            private T data;
            private Node<T> next;
            private Node<T> previous;

            /// <summary>
            /// Public Property for private data member
            /// </summary>
            public T Data
            {
                set
                {
                    data = value;
                }
                get
                {
                    return data;
                }
            }
            /// <summary>
            /// Public property for private next member
            /// </summary>
            public Node<T> Next
            {
                set
                {
                    next = value;
                }
                get
                {
                    return next;
                }
            }
            /// <summary>
            /// Public Property for private previous member
            /// </summary>
            public Node<T> Previous
            {
                set
                {
                    previous = value;
                }
                get
                {
                    return previous;
                }
            }

            /// <summary>
            /// Node class constructor
            /// </summary>
            /// <param name="data">Generic type</param>
            public Node(T data)
            {
                this.data = data;
                next = previous = null;
            }

        }

        /// <summary>
        /// Private data members of the Doubly Linked List class
        /// </summary>
        private Node<T> head;
        private Node<T> tail;
        int count;

        /// <summary>
        /// Doubly Linked List class constructor
        /// </summary>
        public DoublyLinkedList()
        {
            head = tail = null;
            count = 0;
        }
        
        /// <summary>
        /// Adds a new node into the list with the given data
        /// </summary>
        /// <param name="data">Data to add into the list</param>
        /// <returns>ode added into the list</returns>
        public Node<T> Add(T data)
        {
            // No data to insert into list
            if (data == null)
                return null;

            Node<T> node = new Node<T>(data);

            // The Linked list is empty
            if (head == null)
            {
                head = node;
                tail = head;
                count++;
                return node;
            }

            // Add to the end of the list
            tail.Next = node;
            node.Previous = tail;
            tail = node;
            count++;
            return node;
        }
        
        /// <summary>
        /// Removes the first node in the list matching the data
        /// </summary>
        /// <param name="data">Data to remove from the list</param>
        /// <returns> Node removed from the list</returns>
        public Node<T> Remove(T data)
        {

            // List is empty or no data to remove
            if (head == null || data == null)
                return null;

            Node<T> tmp = head;
            // The data to remove what found in the first Node in the list
            if (tmp.Data.Equals(data))
            {
                head = head.Next;
                count--;
                return tmp;
            }

            // Try to find the node in the list
            while (tmp.Next != null)
            {
                // Node was found, Remove it from the list
                if (tmp.Next.Data.Equals(data))
                {
                    if (tmp.Next == tail)
                    {
                        tail = tmp;
                        tmp = tmp.Next;
                        tail.Next = null;
                        count--;
                        return tmp;
                    }
                    else
                    {
                        Node<T> node = tmp.Next;
                        tmp.Next = tmp.Next.Next;
                        tmp.Next.Next.Previous = tmp;
                        node.Next = node.Previous = null;
                        count--;
                        return node;
                    }
                }
            }
            // The data was not found in the list
            return null;
        }
        
        /// <summary>
        /// Gets the first node that has the given data
        /// </summary>
        /// <param name="data">Data to find in the list</param>
        /// <returns>First node with matching data or null if no node was found</returns>
        public Node<T> Find(T data)
        {
            // No list or data to find
            if (head == null || data == null)
                return null;

            Node<T> tmp = head;
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
        public Node<T> IndexAt(int index)
        {
            //Index was negative or larger then the amount of Nodes in the list
            if (index < 0 || index > Size())
                return null;

            Node<T> tmp = head;

            // Move to index
            for (int i = 0; i < index; i++)
            {
                tmp = tmp.Next;
            }
            // return the node at the index position
            return tmp;
        }
       
        /// <summary>
        /// Gets the current count of the array
        /// </summary>
        /// <returns>Number of items in the array</returns>
        public int Size()
        {
            return count;
        }
    }
}
