/*******************************************************
 *  ArrayedHeap.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Heap implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Heaps.LinkedHeap
{    
    /// <summary>
    /// Linked Heap class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class LinkedHeap<T> where T : IComparable
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
            public Node Left { set; get; }
            public Node Right { set; get; }
            public int Npl { set; get; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="data">data for node to hold</param>
            public Node(T data)
            {
                Data = data;
                Left = null;
                Right = null;
                Npl = 0;
            }
           
            /// <summary>
            /// ode Constructor
            /// </summary>
            /// <param name="data">data for node to hold</param>
            /// <param name="left">left child</param>
            /// <param name="right">right child</param>
            public Node(T data, Node left, Node right)
            {
                Data = data;
                Left = left;
                Right = right;
                Npl = 0;
            }

        }
        /// <summary>
        /// Private members
        /// </summary>
        private Node _root;
        
        /// <summary>
        /// Gets the root of the heap
        /// </summary>
        /// <returns>root node of the heap</returns>
        public Node Root() => _root;
        
        /// <summary>
        /// Constructor for linked heap.
        /// </summary>
        public LinkedHeap() => _root = null;
        
        /// <summary>
        /// Merges two heaps together
        /// </summary>
        /// <param name="heap">heap to merge with</param>
        public void Merge(LinkedHeap<T> heap)
        {
            if (!Equals(heap))
            {
                _root = Merge(_root, heap.Root());
                heap._root = null;
            }
        }
        
        /// <summary>
        /// Merges two roots together
        /// </summary>
        /// <param name="n1">first root</param>
        /// <param name="n2"> second root</param>
        /// <returns>merged roots</returns>
        private Node Merge(Node n1, Node n2)
        {
            if (n1 == null)
                return n2;
            if (n2 == null)
                return n1;
            return (n1.Data.CompareTo(n2.Data) < 0) ? MergeHelper(n1, n2) : MergeHelper(n2, n1);
        }
        
        /// <summary>
        /// Helper method to Merge()
        /// </summary>
        /// <param name="n1">first root</param>
        /// <param name="n2">second root</param>
        /// <returns>merged roots</returns>
        private Node MergeHelper(Node n1, Node n2)
        {
            if (n1.Left == null)
                n1.Left = n2;
            else
            {
                n1.Right = Merge(n1.Right, n2);
                if (n1.Left.Npl < n1.Right.Npl)
                    SwapChildren(n1);
                n1.Npl = n1.Right.Npl + 1;
            }
            return n1;
        }

        /// <summary>
        /// Swaps the children of the given node
        /// </summary>
        /// <param name="node">node with two children to swap</param>
        private void SwapChildren(Node node)
        {
            Node tmp = node.Left;
            node.Left = node.Right;
            node.Right = tmp;
        }
        
        /// <summary>
        /// Insert into the priority queue, maintaining heap order.
        /// </summary>
        /// <param name="data">the item to insert.</param>
        public void Insert(T data) => _root = Merge(new Node(data), _root);

        /// <summary>
        /// Find the smallest item in the priority queue.
        /// </summary>
        /// <returns>smallest item or default(T).</returns>
        public T FindMin() => IsEmpty() ? default(T) : _root.Data;

        /// <summary>
        ///  Remove the smallest item from the heap
        /// </summary>
        /// <returns>item removed or default(T)</returns>
        public T DeleteMin()
        {
            if (!IsEmpty())
            {
                T minItem = _root.Data;
                _root = Merge(_root.Left, _root.Right);
                return minItem;
            }
            return default(T);
        }

        /// <summary>
        /// Test if heap is empty.
        /// </summary>
        /// <returns> true|false.</returns>
        public bool IsEmpty() => _root == null;

        /// <summary>
        /// Test if the heap is full.
        /// </summary>
        /// <returns>false: Is never full</returns>
        public bool IsFull() => false;

        /// <summary>
        /// Clears the data in the heap
        /// </summary>
        public void MakeEmpty() => _root = null;
    }
}
