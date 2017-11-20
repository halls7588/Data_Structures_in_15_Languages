/*******************************************************
 *  SelfBalancingBinaryTree.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Self Balancing Binary Tree implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Trees.SelfBalancingBinaryTree
{
    /// <summary>
    /// Self Balancing Binary Tree Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class SelfBalancingBinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// Node class for SelfBalancingBinaryTree
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public members of the node class
            /// </summary>
            public Node Left { set; get; }
            public Node Right { set; get; }
            public T Data { set; get; }
            public int Height { set; get; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            public Node()
            {
                Left = null;
                Right = null;
                Data = default(T);
                Height = 0;
            }

            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="data">Data for the node to hold</param>
            public Node(T data)
            {
                Left = null;
                Right = null;
                Data = data;
                Height = 0;
            }
        }

        /// <summary>
        /// Private members
        /// </summary>
        private Node _root;
        private int _count;

        /// <summary>
        /// SelfBalancingBinaryTree Constructor
        /// </summary>
        public SelfBalancingBinaryTree()
        {
            _root = null;
            _count = 0;
        }

        /// <summary>
        /// Insert data into the tree
        /// </summary>
        /// <param name="data">data to insert</param>
        public void Insert(T data) => _root = Insert(data, _root);

        /// <summary>
        /// Inserts data into the tree
        /// </summary>
        /// <param name="data">data to insert</param>
        /// <param name="node">node to start at</param>
        /// <returns>new root</returns>
        private Node Insert(T data, Node node)
        {
            if (node == null)
                node = new Node(data);
            else if (LessThan(data, node.Data))
            {
                node.Left = Insert(data, node.Left);
                if (Height(node.Left) - Height(node.Right) == 2)
                    node = LessThan(data, node.Left.Data) ? RotateLeft(node) : DoubleLeft(node);
            }
            else if (GreaterThan(data, node.Data))
            {
                node.Right = Insert(data, node.Right);
                if (Height(node.Right) - Height(node.Left) == 2)
                    node = GreaterThan(data, node.Right.Data) ? RotateRight(node) : DoubleRight(node);
            }
            node.Height = Max(Height(node.Left), Height(node.Right)) + 1;
            _count++;
            return node;
        }

        /// <summary>
        /// Rotates the given root node left
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root</returns>
        private Node RotateLeft(Node node)
        {
            Node tmp = node.Left;
            node.Left = tmp.Right;
            tmp.Right = node;
            node.Height = Max(Height(node.Left), Height(node.Right)) + 1;
            tmp.Height = Max(Height(tmp.Left), node.Height) + 1;
            return tmp;
        }

        /// <summary>
        /// Rotates the given root node right
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root</returns>
        private Node RotateRight(Node node)
        {
            Node tmp = node.Right;
            node.Right = tmp.Left;
            tmp.Left = node;
            node.Height = Max(Height(node.Left), Height(node.Right)) + 1;
            tmp.Height = Max(Height(tmp.Right), node.Height) + 1;
            return tmp;
        }

        /// <summary>
        /// Rotates the left child right than the root node left
        /// </summary>
        /// <param name="node">root node to rotate</param>
        /// <returns>new root</returns>
        private Node DoubleLeft(Node node)
        {
            node.Left = RotateRight(node.Left);
            return RotateLeft(node);
        }

        /// <summary>
        /// rotates the right child left than the root right
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root</returns>
        private Node DoubleRight(Node node)
        {
            node.Right = RotateLeft(node.Right);
            return RotateRight(node);
        }

        /// <summary>
        /// Gets the size of the tree
        /// </summary>
        /// <returns>number of node in the tree</returns>
        public int Size() => _count;

        /// <summary>
        /// Gets the height of the node
        /// </summary>
        /// <param name="node">node to test</param>
        /// <returns>height of the node</returns>
        private int Height(Node node) => node == null ? -1 : node.Height;

        /// <summary>
        /// Function to get the max of left/right node
        /// </summary>
        /// <param name="left">left element to test</param>
        /// <param name="right">right element to test</param>
        /// <returns>the max of given left and right</returns>
        private static int Max(int left, int right) => left > right ? left : right;

        /// <summary>
        /// check if tree is empty
        /// </summary>
        /// <returns>rue|false</returns>
        public bool IsEmpty() => _root == null;

        /// <summary>
        /// Clears the data from the tree
        /// </summary>
        public void Clear() => _root = null;

        /// <summary>
        /// Determines if a is less than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool LessThan(T a, T b) => a.CompareTo(b) < 0;

        /// <summary>
        /// Determines if a is greater than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool GreaterThan(T a, T b) => a.CompareTo(b) > 0;
    }
}