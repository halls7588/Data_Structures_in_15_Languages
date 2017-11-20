/*******************************************************
 *  BTree.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  B-Tree implementation in C#
 ********************************************************/
using System;

namespace DataStructures.Trees.BTree
{
    /// <summary>
    /// Btree class
    /// </summary>
    /// <typeparam name="TKey">Generic type</typeparam>
    /// <typeparam name="TValue">Generic type</typeparam>
    public class BTree<TKey, TValue> where TKey : IComparable where TValue : IComparable
    {
        /// <summary>
        /// Node class
        /// </summary>
        public class Node
        {
            /// <summary>
            /// public members of the node class
            /// </summary>
            public int Size;
            public Entry[] Children;
            
            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="size">number of children of the node</param>
            public Node(int size)
            {
                Children = new Entry[Max];
                Size = size;
            }
        }

        /// <summary>
        /// Helper class for BTree Node class
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// public member of the enrty class
            /// </summary>
            public TKey Key;
            public TValue Val;
            public Node Next;

            /// <summary>
            /// Entry class Constructor
            /// </summary>
            /// <param name="key">key to hold</param>
            /// <param name="val">value of the key</param>
            /// <param name="next">next node in the tree</param>
            public Entry(TKey key, TValue val, Node next)
            {
                Key = key;
                Val = val;
                Next = next;
            }
        }


        /// <summary>
        /// private member of the BTree class
        /// </summary>
        private const int Max = 4;// max children per B-tree node = M-1
        private Node _root;
        private int _height;
        private int _pairs; // number of key-value pairs in the B-tree

        /// <summary>
        /// B-tree constructor
        /// </summary>
        public BTree() => _root = new Node(0);

        /// <summary>
        /// Determines if the tree is empty
        /// </summary>
        /// <returns>true|false</returns>
        public bool IsEmpty() => Size() == 0;

        /// <summary>
        /// Returns the number number of pars in the tree
        /// </summary>
        /// <returns>the number of key-value pairs in the tree</returns>
        public int Size() => _pairs;

        /// <summary>
        /// Gets the height of the tree
        /// </summary>
        /// <returns>height of the tree</returns>
        public int Height() => _height;

        /// <summary>
        /// Gets the value of the given key
        /// </summary>
        /// <param name="key">key to find value of</param>
        /// <returns>value of the given key or null</returns>
        public TValue Get(TKey key) => key.CompareTo(default(TKey)) == 0 ? default(TValue) : Search(_root, key, _height);

        /// <summary>
        /// Searches the tree for the key
        /// </summary>
        /// <param name="node">node to start at</param>
        /// <param name="key">key to find</param>
        /// <param name="ht">height of the tree</param>
        /// <returns>Value of the key or null</returns>
        private static TValue Search(Node node, TKey key, int ht)
        {
            Entry[] children = node.Children;
            // external node
            if (ht == 0)
            {
                for (int j = 0; j < node.Size; j++)
                {
                    if (EqualTo(key, children[j].Key))
                        return children[j].Val;
                }
            }
            // internal node
            else
            {
                for (int j = 0; j < node.Size; j++)
                {
                    if (j + 1 == node.Size || LessThan(key, children[j + 1].Key))
                        return Search(children[j].Next, key, ht - 1);
                }
            }
            return default(TValue);
        }

        /// <summary>
        /// Adds a node into the tree or replaces value
        /// </summary>
        /// <param name="key">key of the node</param>
        /// <param name="val">value of the key</param>
        public void Put(TKey key, TValue val)
        {
            if (key.CompareTo(default(TKey)) == 0)
                return;
            Node node = Insert(_root, key, val, _height);
            _pairs++;
            if (node == null)
                return;
            // need to split root
            Node tmp = new Node(2)
            {
                Children =
                {
                    [0] = new Entry(_root.Children[0].Key, default(TValue), _root),
                    [1] = new Entry(node.Children[0].Key, default(TValue), node)
                }
            };
            _root = tmp;
            _height++;
        }
        
        /// <summary>
        /// Inserts a node into the tree and updates the tree
        /// </summary>
        /// <param name="node">root to start at</param>
        /// <param name="key">key of the new node</param>
        /// <param name="val">value of the key</param>
        /// <param name="ht">current height</param>
        /// <returns>node added into the tree</returns>
        private Node Insert(Node node, TKey key, TValue val, int ht)
        {
            int j;
            Entry entry = new Entry(key, val, null);
            // external node
            if (ht == 0)
            {
                for (j = 0; j < node.Size; j++)
                {
                    if (LessThan(key, node.Children[j].Key))
                        break;
                }
            }
            // internal node
            else
            {
                for (j = 0; j < node.Size; j++)
                {
                    if (j + 1 == node.Size || LessThan(key, node.Children[j + 1].Key))
                    {
                        Node tmp = Insert(node.Children[j++].Next, key, val, (ht - 1));

                        if (tmp == null)
                            return null;

                        entry.Key = tmp.Children[0].Key;
                        entry.Next = tmp;
                        break;
                    }
                }
            }

            Array.Copy(node.Children, j, node.Children, j + 1, node.Size - j);

            node.Children[j] = entry;
            node.Size++;

            if (node.Size < Max)
                return null;
            return Split(node);
        }

        /// <summary>
        /// Splits the given node in half
        /// </summary>
        /// <param name="node">node to split</param>
        /// <returns>split node</returns>
        private static Node Split(Node node)
        {
            Node tmp = new Node(Max / 2);
            node.Size = Max / 2;
            Array.Copy(node.Children, 2, tmp.Children, 0, Max / 2);
            return tmp;
        }

        /// <summary>
        /// Determines if a is less than b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool LessThan(TKey a, TKey b) => a.CompareTo(b) < 0;

        /// <summary>
        /// Determines if a is equal to b
        /// </summary>
        /// <param name="a">generic type to test</param>
        /// <param name="b">generic type to test</param>
        /// <returns>true|false</returns>
        private static bool EqualTo(TKey a, TKey b) => a.CompareTo(b) == 0;
    }
}
