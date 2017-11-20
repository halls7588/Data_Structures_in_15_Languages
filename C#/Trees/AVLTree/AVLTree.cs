/*******************************************************
 *  AvlTree.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  AVL Tree implementation in C#
 ********************************************************/
using System;


namespace DataStructures.Trees.AVLTree
{
    /// <summary>
    /// AVL Tree Class
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    public class AvlTree<T> where T : IComparable
    {
        /// <summary>
        /// Node class for AVL Tree
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public accessors of the node class
            /// </summary>
            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            /// <summary>
            /// Default Node constructor
            /// </summary>
            /// <param name="data">data for the node</param>
            public Node(T data)
            {
                Data = data;
                Left = Right = null;
            }

            /// <summary>
            /// Node constructor with left and right leafs
            /// </summary>
            /// <param name="data">data to hold</param>
            /// <param name="left">left child</param>
            /// <param name="right">right child</param>
            public Node(T data, Node left, Node right)
            {
                Data = data;
                Left = left;
                Right = right;
            }
        }

        /// <summary>
        /// Private members of the AVL Tree class
        /// </summary>
        private Node _root;
        private int _count;

        /// <summary>
        /// AVLTree Constructor.
        /// </summary>
        public AvlTree()
        {
            _root = null;
            _count = 0;
        }

        /// <summary>
        ///  Gets the height of a node
        /// </summary>
        /// <param name="node">node to test</param>
        /// <returns>height of the node</returns>
        public int Height(Node node) => node == null ? -1 : node.Height;

        /// <summary>
        /// Find the max value among the given numbers.
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Maximum value</returns>
        public int Max(int a, int b) => a > b ? a : b;

        /// <summary>
        /// Insert an element into the tree.
        /// </summary>
        /// <param name="data">data to insert into the tree</param>
        /// <returns>success|fail</returns>
        public bool Insert(T data)
        {
            try
            {
                _root = Insert(data, _root);
                _count++;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Private Insert Helper
        /// </summary>
        /// <param name="data">data to add</param>
        /// <param name="node">Root of the tree</param>
        /// <returns>New root of the tree</returns>
        /// <exception cref="Exception">failure or duplicate value</exception>
        private Node Insert(T data, Node node)
        {
            if (node == null)
                node = new Node(data);
            else if (data.CompareTo(node.Data) < 0)
            {
                node.Left = Insert(data, node.Left);
                if (Height(node.Left) - Height(node.Right) == 2)
                {
                    if (data.CompareTo(node.Left.Data) < 0)
                    {
                        node = RotateLeft(node);
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.Right = Insert(data, node.Right);

                if (Height(node.Right) - Height(node.Left) == 2)
                    if (data.CompareTo(node.Right.Data) > 0)
                    {
                        node = RotateRight(node);
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
            }
            else
            {
                throw new Exception("Attempting to insert duplicate value");
            }
            node.Height = Max(Height(node.Left), Height(node.Right)) + 1;
            return node;
        }

        /// <summary>
        /// Rotates Node left
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root node</returns>
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
        /// Rotate left child Right then rotate left
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root node</returns>
        private Node RotateRightLeft(Node node)
        {
            node.Left = RotateRight(node.Left);
            return RotateLeft(node);
        }

        /// <summary>
        /// Rotate Right
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root node</returns>
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
        /// Rotate right child left then rotate right
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root node</returns>
        private Node RotateLeftRight(Node node)
        {
            node.Right = RotateLeft(node.Right);
            return RotateRight(node);
        }

        /// <summary>
        /// Deletes all nodes from the tree.
        /// </summary>
        public void MakeEmpty() => _root = null;

        /// <summary>
        /// Determine if the tree is empty.
        /// </summary>
        /// <returns>True if the tree is empty</returns>
        public bool IsEmpty() => _root == null;

        /// <summary>
        /// Find the smallest item in the tree.
        /// </summary>
        /// <returns>smallest item or null if empty.</returns>
        public T FindMin() => (IsEmpty()) ? default(T) : FindMin(_root).Data;

        /// <summary>
        /// Find the largest item in the tree.
        /// </summary>
        /// <returns>the largest item or null if empty.</returns>
        public T FindMax() => (IsEmpty()) ? default(T) : FindMax(_root).Data;

        /// <summary>
        /// Find min helper
        /// </summary>
        /// <param name="node">root to test</param>
        /// <returns>node containing the smallest item.</returns>
        private Node FindMin(Node node)
        {
            if (node == null)
                return null;

            while (node.Left != null)
                node = node.Left;

            return node;
        }

        /// <summary>
        /// Find max helper
        /// </summary>
        /// <param name="node">root to test.</param>
        /// <returns>node containing the largest item.</returns>
        private Node FindMax(Node node)
        {
            if (node == null)
                return null;

            while (node.Right != null)
                node = node.Right;

            return node;
        }

        /// <summary>
        /// Removes an item from the tree.
        /// </summary>
        /// <param name="data">item to remove.</param>
        public void Remove(T data) => _root = Remove(data, _root);

        /// <summary>
        /// Recursive Remove helper function
        /// </summary>
        /// <param name="data">data to remove</param>
        /// <param name="node">root to start at</param>
        /// <returns>new root node</returns>
        private Node Remove(T data, Node node)
        {
            if (node == null)
            {
                return null;
            }

            if (data.CompareTo(node.Data) < 0)
            {
                node.Left = Remove(data, node.Left);
                int l = node.Left != null ? node.Left.Height : 0;

                if ((node.Right != null) && (node.Right.Height - l >= 2))
                {
                    int rightHeight = node.Right.Right != null ? node.Right.Right.Height : 0;
                    int leftHeight = node.Right.Left != null ? node.Right.Left.Height : 0;

                    if (rightHeight >= leftHeight)
                        node = RotateLeft(node);
                    else
                        node = RotateLeftRight(node);
                }
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                node.Right = Remove(data, node.Right);
                int r = node.Right != null ? node.Right.Height : 0;
                if ((node.Left != null) && (node.Left.Height - r >= 2))
                {
                    int leftHeight = node.Left.Left != null ? node.Left.Left.Height : 0;
                    int rightHeight = node.Left.Right != null ? node.Left.Right.Height : 0;
                    if (leftHeight >= rightHeight)
                        node = RotateRight(node);
                    else
                        node = RotateRightLeft(node);
                }
            }
            else if (node.Left != null)
            {
                node.Data = FindMax(node.Left).Data;
                Remove(node.Data, node.Left);

                if ((node.Right != null) && (node.Right.Height - node.Left.Height >= 2))
                {
                    int rightHeight = node.Right.Right != null ? node.Right.Right.Height : 0;
                    int leftHeight = node.Right.Left != null ? node.Right.Left.Height : 0;

                    if (rightHeight >= leftHeight)
                        node = RotateLeft(node);
                    else
                        node = RotateLeftRight(node);
                }
            }
            else
                node = node.Right;

            if (node != null)
            {
                int leftHeight = node.Left != null ? node.Left.Height : 0;
                int rightHeight = node.Right != null ? node.Right.Height : 0;
                node.Height = Max(leftHeight, rightHeight) + 1;
            }
            return node;
        }

        /// <summary>
        /// Determines is the data exists in the tree
        /// </summary>
        /// <param name="data">data to find</param>
        /// <returns>success|fail</returns>
        public bool Contains(T data) => Contains(data, _root);

        /// <summary>
        /// Recursive Contains helper method
        /// </summary>
        /// <param name="data">data to find</param>
        /// <param name="node">node to test</param>
        /// <returns>success|fail</returns>
        private bool Contains(T data, Node node)
        {
            if (node == null)
                return false; // The node was not found
            if (data.CompareTo(node.Data) < 0)
                return Contains(data, node.Left);
            if (data.CompareTo(node.Data) > 0)
                return Contains(data, node.Right);

            return true;
        }
    }
}
