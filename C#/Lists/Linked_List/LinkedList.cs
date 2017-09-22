/*************************************************************
*  LinkedList.cs
*  Created by Stephen Hall on 9/22/17.
*  Copyright (c) 2017 Stephen Hall. All rights reserved.
*  A singly linked list implementation in C#.NET 
*************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// Node class for singlely linked list
    /// </summary>
    /// <typeparam name="T">Generic type to be used</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Set and get property for private data member
        /// </summary>
        public T Data
        {
            set { this.data = value; }
            get { return this.data; }
        }
        /// <summary>
        /// Set and get property for private next member
        /// </summary>
        public Node<T> Next
        {
            set { this.next = value; }
            get { return this.next; }
        }

        /// <summary>
        /// Private members to be used 
        /// </summary>
        private T data;
        private Node<T> next;

        /// <summary>
        /// Node class constructor
        /// </summary>
        /// <param name="data">data for Node instance to hold</param>
        public Node(T data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
    /// <summary>
    /// Singlely linked list class 
    /// </summary>
    /// <typeparam name="T">Generic type to be used</typeparam>
    public class LinkedList<T>
    {
        /// <summary>
        /// Private members
        /// </summary>
        private int count;
        private Node<T> head;
        private Node<T> tail;
        
        /// <summary>
        /// LinkedList class constructor
        /// </summary>
        public LinkedList()
        {
            count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Adds a new node into the list with the given data
        /// </summary>
        /// <param name="data">Data to add into the list</param>
        /// <returns>Node added into the list</returns>
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
            tail = node;
            count++;
            return node;
        }

        /// <summary>
        /// Removes the first node in the list matching the data
        /// </summary>
        /// <param name="data">Data to remove from the list</param>
        /// <returns>Node removed from the list</returns>
        public Node<T> Remove(T data)
        {
            // List is empty or no data to remove
            if (head == null || data == null)
                return null;

            Node<T> tmp = head;
            // The data to remove what found in the first Node in the list
            if(tmp.Data.Equals(data))
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
                    Node<T> node = tmp.Next;
                    tmp.Next = tmp.Next.Next;
                    count--;
                    return node;
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
            while(tmp != null)
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
            //Index was negitive or larger then the amount of Nodes in the list
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
        /// Gets the first index matching the given data or -1 if data does not exist in the list
        /// </summary>
        /// <param name="data">Data to find in the list</param>
        /// <returns>First index that has data or -1</returns>
        public int IndexOf(T data)
        {
            int index = 0;
            Node<T> tmp = head;
            // Try to find the data in the list
            while (tmp != null)
            {
                // Data was found, return current index
                if (tmp.Data.Equals(data))
                    return index;
                index++;
                tmp = tmp.Next;
            }
            // Data was not found in the list
            return -1;
        }

        /// <summary>
        /// Returns the current size of the list
        /// </summary>
        /// <returns>Size of the list</returns>
        public int Size()
        {
            return count;
        }
    }
}

