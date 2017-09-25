/*******************************************************
 *  BinaryTree.cs
 *  Created by Stephen Hall on 9/25/17.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Binary Tree implementation in C#
 ********************************************************/

using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Binary Tree Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class BinaryTree<T>
    {
        /// <summary>
        /// Node class for the binary tree
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        public class Node<T>
        {
            /// <summary>
            /// Private members
            /// </summary>
            private Node<T> left;
            private Node<T> right;
            private Node<T> parent;
            private T data;

            /// <summary>
            /// Public Property for the Node data Member
            /// </summary>
            public T Data
            {
                set { data = value; }
                get { return data; }
            }

            /// <summary>
            /// Public Property for the Node left Member
            /// </summary>
            public Node<T> Left
            {
                set { left = value; }
                get { return left; }
            }

            /// <summary>
            /// Public Property for the Node right Member
            /// </summary>
            public Node<T> Right
            {
                set { right = value; }
                get { return right; }
            }

            /// <summary>
            /// Public Property for the Node parent Member
            /// </summary>
            public Node<T> Parent
            {
                set { parent = value; }
                get { return parent; }
            }

            /// <summary>
            /// Node class constructor             
            /// </summary>
            /// <param name="data">Data to be held in the Node</param>
            public Node(T data)
            {
                left = right = null;
                this.data = data;
            }
        }

        /// <summary>
        /// Private Binary Tree members
        /// </summary>
        private Node<T> root;
       
        /// <summary>
        /// Binary tree class constructor
        /// </summary>
        public BinaryTree()
        {
            root = null;
        }
        
        /// <summary>
        /// Compares the data in given node to the give data for equality
        /// </summary>
        /// <param name="data">data to compare</param>
        /// <param name="node">Node to compare to</param>
        /// <returns>1 if greater then, 0 if equal to, -1 if less then</returns>
        private int compare(T data, Node<T> node)
        {
            return ((IComparable)node).CompareTo(data);
        }
        
        /// <summary>
        /// Inserts a new node into the tree
        /// </summary>
        /// <param name="data">data to insert into the tree</param>
        /// <returns>Node inserted into the tree</returns>
        public Node<T> Insert(T data)
        {
            if (root == null)
            {
                return (root = new Node<T>(data));
            }
            return this.insertHelper(data, root);
        }
        
        /// <summary>
        /// Recursive helper function to inset a new node into the tree
        /// </summary>
        /// <param name="data">Data to insert into the tree</param>
        /// <param name="node">urrent node in the tree</param>
        /// <returns>Node inserted into the tree</returns>
        private Node<T> insertHelper(T data, Node<T> node)
        {
            try
            {
                int cmp = this.compare(data, node);
                switch (cmp)
                {
                    case 1:
                        if (node.Right == null)
                        {
                            Node<T> newNode = new Node<T>(data);
                            node.Right = newNode;
                            newNode.Parent = node;
                            return newNode;
                        }
                        else
                            return this.insertHelper(data, node.Right);

                    default:
                        if (node.Left == null)
                        {
                            Node<T> newNode = new Node<T>(data);
                            node.Left = newNode;
                            newNode.Parent = node;
                            return newNode;
                        }
                        else
                            return this.insertHelper(data, node.Left);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        /// <summary>
        /// Removes a Node from the tree
        /// </summary>
        /// <param name="data">Data to remove from the tree</param>
        /// <returns>Node removed from the tree</returns>
        public Node<T> Remove(T data)
        {
            if (root != null)
                return null;
            return this.removeHelper(data, root);
        }
        
        /// <summary>
        /// Recursive helper function to remove a node from the tree
        /// </summary>
        /// <param name="data">Data to remove</param>
        /// <param name="node">urrent node</param>
        /// <returns>Node removed from the tree</returns>
        private Node<T> removeHelper(T data, Node<T> node)
        {
            try
            {
                int cmp = this.compare(data, node);
                if (cmp == 1)
                    return this.removeHelper(data, node.Right);
                if (cmp == -1)
                    return this.removeHelper(data, node.Left);
                if (cmp == 0)
                {
                    //has no children
                    Node<T> tempNode;

                    if (node.Left == null && node.Right == null)
                    {
                        node = node.Parent;
                    }

                    //has one child
                    if (node.Parent != null)
                    {
                        tempNode = node.Parent;
                    }
                    else
                    { // this is the root node
                        if (node.Right != null)
                        {
                            tempNode = node.Right;
                            tempNode.Parent = null;
                            this.root.Right = null;
                            this.root = tempNode;
                            return node;
                        }
                        else
                        {
                            tempNode = node.Left;
                            tempNode.Parent = null;
                            this.root.Left = null;
                            this.root = tempNode;
                            return node;
                        }
                    }
                    if (tempNode.Left == node)
                    {
                        if (node.Left != null)
                        {
                            tempNode.Left = node.Left;
                            node.Left.Parent = tempNode;
                            return node;
                        }
                        else if (node.Right != null)
                        {
                            tempNode.Right = node.Right;
                            node.Right.Parent = tempNode;
                            return node;
                        }
                    }
                    if (tempNode.Right == node)
                    {
                        if (node.Left != null)
                        {
                            tempNode.Left = node.Left;
                            node.Left.Parent = tempNode;
                            return node;
                        }
                        else if (node.Right != null)
                        {
                            tempNode.Right = node.Right;
                            node.Right.Parent = tempNode;
                            return node;
                        }
                    }
                    else if (node.Left != null && node.Right != null)
                    { // test for 2 children
                        tempNode = node.Right; // find min value of right child tree to replace the node
                        while (tempNode.Left != null)
                            tempNode = tempNode.Left;
                        T temp = node.Data;
                        node.Data = tempNode.Data; // replace the node with the tempNode
                        tempNode.Data = temp;
                        tempNode.Parent.Left = null;
                        tempNode.Parent = null;
                        return tempNode;
                    }
                }
            }
            catch (Exception e) { }
            return null;
        }
        
        /// <summary>
        /// Returns the smallest node in the tree
        /// </summary>
        /// <returns>Smallest Node int he tree</returns>
        public Node<T> GetMin()
        {
            if (root == null)
                return null;

            Node<T> node = this.root;

            while (node.Left != null)
                node = node.Left;

            return node;
        }
        
        /// <summary>
        /// Return the largest node in the tree
        /// </summary>
        /// <returns>argest Node in the tree</returns>
        public Node<T> GetMax()
        {
            if (root == null)
                return null;

            Node<T> node = this.root;

            while (node.Right != null)
                node = node.Right;

            return node;
        }
       
        /// <summary>
        /// Prints out the tree using Pre Order Traversal
        /// </summary>
        /// <param name="node">Node to start the Pre Order Traversal at</param>
        public void PreOrederTraversal(Node<T> node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Data);
                PreOrederTraversal(node.Left);
                PreOrederTraversal(node.Right);
            }
        }
        
        /// <summary>
        /// Prints out the Tree using Post Order Traversal
        /// </summary>
        /// <param name="node">Node to start the Post Order Traversal at</param>
        public void PostPrderTraversal(Node<T> node)
        {
            if (node != null)
            {
                PostPrderTraversal(node.Left);
                PostPrderTraversal(node.Right);
                Console.WriteLine(node.Data);
            }
        }
        
        /// <summary>
        /// Pints out the tree using in order Traversal
        /// </summary>
        /// <param name="node">Node to start the In Order Traversal at</param>
        public void InOrderedTraversal(Node<T> node)
        {
            if (node != null)
            {
                InOrderedTraversal(node.Left);
                Console.WriteLine(node.Data);
                InOrderedTraversal(node.Right);
            }
        }
        
        /// <summary>
        /// Prints out the tree using Depth First Search
        /// </summary>
        /// <param name="node">Node to start the Depth First Search at</param>
        public void DepthFirstSearch(Node<T> node)
        {
            if (node == null)
                return;

            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                Console.WriteLine(node = stack.Pop());
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }
        
        /// <summary>
        /// Prints out the tree using breadth first search
        /// </summary>
        /// <param name="node">Node to start Breadth First Search at</param>
        public void BreadthFirstSearch(Node<T> node)
        {
            if (node == null)
                return;

            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                Console.WriteLine(node = queue.Dequeue());
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
    }

}
