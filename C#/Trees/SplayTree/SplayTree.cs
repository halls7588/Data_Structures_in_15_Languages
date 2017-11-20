/*******************************************************
 *  SplayTree.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Splay Tree implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Trees.SplayTree
{
    /// <summary>
    /// Splay Tree Class
    /// </summary>
    /// <typeparam name="TKey">generic type</typeparam>
    /// <typeparam name="TValue">generic type</typeparam>
    public class SplayTree<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// Node class for splay tree
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public properties for the node class
            /// </summary>
            public TKey Key { set; get; }
            public TValue Value { set; get; }
            public Node Left { set; get; }
            public Node Right { set; get; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="key">key to the node</param>
            /// <param name="value">value of the node</param>
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        /// <summary>
        /// private members
        /// </summary>
        private Node _root;
        
        /// <summary>
        /// Tests to see if a key exist in the tree
        /// </summary>
        /// <param name="key">key to test</param>
        /// <returns>yes|no</returns>
        public bool Contains(TKey key) => Get(key) != null;

        /// <summary>
        /// Gets the value of the key if it exists
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>Value of the Key</returns>
        public TValue Get(TKey key)
        {
            _root = Splay(_root, key);
            return key.CompareTo(_root.Key) == 0 ? _root.Value : default(TValue);
        }

        /// <summary>
        /// adds or updates an item into the tree
        /// </summary>
        /// <param name="key">key to insert</param>
        /// <param name="value">value of the key</param>
        public void Put(TKey key, TValue value)
        {
            // splay key to root
            if (_root == null)
            {
                _root = new Node(key, value);
                return;
            }
            _root = Splay(_root, key);
            int tmp = key.CompareTo(_root.Key);
            // Insert new node at root
            if (tmp < 0)
            {
                Node node = new Node(key, value);
                node.Left = _root.Left;
                node.Right = _root;
                _root.Left = null;
                _root = node;
            }
            // Insert new node at root
            else if (tmp > 0)
            {
                Node node = new Node(key, value);
                node.Right = _root.Right;
                node.Left = _root;
                _root.Right = null;
                _root = node;
            }
            // Duplicate key. Update value
            else
            {
                _root.Value = value;
            }

        }

        /// <summary>
        /// Removes a node from the tree
        /// </summary>
        /// <param name="key">key to remove</param>
        public void Remove(TKey key)
        {
            if (_root == null)
                return;

            _root = Splay(_root, key);

            int tmp = key.CompareTo(_root.Key);

            if (tmp == 0)
            {
                if (_root.Left == null)
                {
                    _root = _root.Right;
                }
                else
                {
                    Node node = _root.Right;
                    _root = _root.Left;
                    Splay(_root, key);
                    _root.Right = node;
                }
            }
        }

        /// <summary>
        /// Rotates the given node to the right
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root of the rotate</returns>
        private static Node RotateRight(Node node)
        {
            Node tmp = node.Left;
            node.Left = tmp.Right;
            tmp.Right = node;
            return tmp;
        }

        /// <summary>
        /// Rotates the node to the left
        /// </summary>
        /// <param name="node">node to rotate</param>
        /// <returns>new root of the rotate</returns>
        private static Node RotateLeft(Node node)
        {
            Node tmp = node.Right;
            node.Right = tmp.Left;
            tmp.Left = node;
            return tmp;
        }

        /// <summary>
        /// Splays the node containing key to the root node given
        /// </summary>
        /// <param name="node">root to splay to</param>
        /// <param name="key">key to find</param>
        /// <returns>Node found or null</returns>
        private static Node Splay(Node node, TKey key)
        {
            if (node == null)
                return null;

            int tmp = key.CompareTo(node.Key);

            if (tmp < 0)
            {
                // key not in tree, so we're done
                if (node.Left == null)
                {
                    return node;
                }
                int tmp2 = key.CompareTo(node.Left.Key);
                if (tmp2 < 0)
                {
                    node.Left.Left = Splay(node.Left.Left, key);
                    node = RotateRight(node);
                }
                else if (tmp2 > 0)
                {
                    node.Left.Right = Splay(node.Left.Right, key);
                    if (node.Left.Right != null)
                        node.Left = RotateLeft(node.Left);
                }

                return node.Left == null ? node : RotateRight(node);
            }
            if (tmp > 0)
            {
                // key not in tree, so we're done
                if (node.Right == null)
                {
                    return node;
                }
                int tmp2 = key.CompareTo(node.Right.Key);
                if (tmp2 < 0)
                {
                    node.Right.Left = Splay(node.Right.Left, key);

                    if (node.Right.Left != null)
                        node.Right = RotateRight(node.Right);
                }
                else if (tmp2 > 0)
                {
                    node.Right.Right = Splay(node.Right.Right, key);
                    node = RotateLeft(node);
                }
                return node.Right == null ? node : RotateLeft(node);
            }

            return node;
        }

        /// <summary>
        /// Gets the height of the tree
        /// </summary>
        /// <returns>height of the tree</returns>
        public int Height() => Height(_root);

        /// <summary>
        /// Gets the height of the given node
        /// </summary>
        /// <param name="node">node to test</param>
        /// <returns>height of the node</returns>
        private static int Height(Node node) => (node == null) ? -1 : Math.Max(Height(node.Left), Height(node.Right)) + 1;

        /// <summary>
        /// Gets the size of the tree
        /// </summary>
        /// <returns>size of the tree</returns>
        public int Size() => Size(_root);

        /// <summary>
        /// Gets the size of the given node
        /// </summary>
        /// <param name="node">noDe to test</param>
        /// <returns>size of the node</returns>
        private static int Size(Node node) => (node == null) ? 0 : Size(node.Left) + Size(node.Right) + 1;
    }
}