/*******************************************************
 *  Hashtable.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Hashtable implementation in C#
 ********************************************************/

namespace DataStructures.HashTables.Hashtable
{
    /// <summary>
    /// Hashtable class declaration
    /// </summary>
    /// <typeparam name="TKey">Generic type</typeparam>
    /// <typeparam name="TValue">Generic type</typeparam>
    public class Hashtable<TKey, TValue>
    {
        /// <summary>
        /// Node class declaration
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Public properties for the node class private members
            /// </summary>
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Next { get; set; }
            public int Hash { get; set; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            /// <param name="key">key of the node</param>
            /// <param name="value">value of the key</param>
            /// <param name="next">next node</param>
            /// <param name="hash">nodes hash</param>
            public Node(TKey key, TValue value, Node next, int hash)
            {
                Key = key;
                Value = value;
                Next = next;
                Hash = hash;
            }
        }

        /// <summary>
        /// private members of the Hashtable class
        /// </summary>
        private Node[] _nodes;

        /// <summary>
        /// Hashtable Constructor
        /// </summary>
        /// <param name="size">size of the Hashtable</param>
        public Hashtable(int size) => _nodes = new Node[size];


        /// <summary>
        /// Gets the hashed index of the give key
        /// </summary>
        /// <param name="key">key to find</param>
        /// <returns>hashed index of the key</returns>
        private int GetIndex(TKey key)
        {
            int hash = key.GetHashCode() % _nodes.Length;
            if (hash < 0)
            {
                hash += _nodes.Length;
            }
            return hash;
        }

        /// <summary>
        /// Inserts Key-Value pair into the table or updates new value
        /// </summary>
        /// <param name="key">key to insert</param>
        /// <param name="value">value of the key</param>
        /// <returns>old value of the key, or new value if not exists</returns>
        public TValue Insert(TKey key, TValue value)
        {
            int hash = GetIndex(key);
            Node node;
            // check if same key already exists and if so lets update it with the new value
            for (node = _nodes[hash]; node != null; node = node.Next)
            {
                if ((hash == node.Hash) && key.Equals(node.Key))
                {
                    TValue oldData = node.Value;
                    node.Value = value;
                    return oldData;
                }
            }
            node = new Node(key, value, _nodes[hash], hash);
            _nodes[hash] = node;
            return value;
        }


        /// <summary>
        /// Removes the key from the hashable
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <returns>success|fail</returns>
        public bool Remove(TKey key)
        {
            int hash = GetIndex(key);
            Node previous = null;
            for (Node node = _nodes[hash]; node != null; node = node.Next)
            {
                if ((hash == node.Hash) && key.Equals(node.Key))
                {
                    if (previous != null)
                        previous.Next = node.Next;
                    else
                        _nodes[hash] = node.Next;
                    return true;
                }
                previous = node;
            }
            return false;
        }

        /// <summary>
        /// Gets the value of the given key
        /// </summary>
        /// <param name="key">key to find</param>
        /// <returns>value of the key</returns>
        public TValue Get(TKey key)
        {
            int hash = GetIndex(key);

            Node node = _nodes[hash];
            while (node != null)
            {
                if (key.Equals(node.Key))
                    return node.Value;
                node = node.Next;
            }
            return default(TValue);
        }

        /// <summary>
        /// Resize the Hashtable
        /// </summary>
        /// <param name="size">size to make the table</param>
        public void Resize(int size)
        {
            Hashtable<TKey, TValue> tbl = new Hashtable<TKey, TValue>(size);
            foreach (Node node in _nodes)
            {
                Node n = node;
                while (n != null)
                {
                    tbl.Insert(node.Key, node.Value);
                    Remove(node.Key);
                    n = node.Next;
                }
            }
            _nodes = tbl._nodes;
        }
    }
}
