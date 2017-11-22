/*******************************************************
 *  @file ArrayList.ccs
 *  @author Stephen Hall
 *  @date 11/17/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details ArrayList implementation in C#
 ********************************************************/

namespace DataStructures.Arrays.AssociativeArray
{
    /// <summary>
    /// Associative Array class declaration
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class AssociativeArray<TKey, TValue>
    {

        /// <summary>
        /// Node class declaration
        /// </summary>
        public class Node
        {
            public TKey Key;
            public TValue Value;
            public Node Next;
            public int Hash;

            /// <summary>
            /// Node class Constructor
            /// </summary>
            /// <param name="key">key of the node</param>
            /// <param name="value">value for node to hold</param>
            /// <param name="hash">hash index for index</param>
            public Node(TKey key, TValue value, int hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }

        }

        private Node[] _table;
        private int _size;

        /// <summary>
        /// AssociativeArray class Constructor
        /// </summary>
        public AssociativeArray()
        {
            _table = new Node[10];
            _size = 0;
        }

        /// <summary>
        ///  AssociativeArray class Constructo
        /// </summary>
        /// <param name="size">Size to initialize array to</param>
        public AssociativeArray(int size)
        {
            _table = new Node[size];
            _size = 0;
        }

        /// <summary>
        /// Adds or updates Key, Value pair into the Array
        /// </summary>
        /// <param name="key">Key to associate vale with</param>
        /// <param name="value">value to store</param>
        /// <returns>Node added or updated</returns>
        public Node Set(TKey key, TValue value)
        {
            // Find the hash of the key and bucket it belongs to 
            int hash = key.GetHashCode();
            int bucket = GetBucket(hash);
            Node entry;
            if (IsEmpty())
            {
                entry = new Node(key, value, hash);
                _table[bucket] = entry;
                _size++;
            }
            else
            {
                entry = _table[bucket];
                while (entry.Next != null)
                {
                    if (entry.GetHashCode() == hash && entry.Key.Equals(key))
                    {
                        entry.Value = value;
                        return entry;
                    }
                    entry = entry.Next;
                }

                Node node = new Node(key, value, hash);
                entry.Next = node;
                _size++;
                entry = node;
            }
            return entry;
        }

        /// <summary>
        /// Gets the value of the given key
        /// </summary>
        /// <param name="key">Key to get value of</param>
        /// <returns>value of the key</returns>
        public TValue Get(TKey key)
        {
            int hash = key.GetHashCode();
            int bucket = GetBucket(hash);

            Node entry = _table[bucket];
            while (entry != null)
            {
                /* If hash and key matches, return the value */
                if ((entry.Hash == hash) && entry.Key.Equals(key))
                {
                    return entry.Value;
                }
                entry = entry.Next;
            }
            return default(TValue);
        }

        /// <summary>
        /// Gets the size of the array
        /// </summary>
        /// <returns>number of elements in the array</returns>
        public int Size() => _size;

        /// <summary>
        /// Checks if the array is empty of not
        /// </summary>
        /// <returns>true|false</returns>
        public bool IsEmpty() => _size == 0;

        /// <summary>
        /// Gets the bucket container for the internal array
        /// </summary>
        /// <param name="hash">hash to find bucket of</param>
        /// <returns>bucket index of the array</returns>
        private int GetBucket(int hash) => (hash % _table.Length);
    }
}
