/*******************************************************
 *  AssociativeArray.java
 *  Created by Stephen Hall on 11/13/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Associative Array implementation in Java
 ********************************************************/

/**
 * Associative Array Class
 * @param <K> Generic Type
 * @param <V> Generic Type
 */
public class AssociativeArray<K extends Comparable<K>, V extends Comparable<V>> {
    /**
     * Node class for associative array
     * @param <K>
     * @param <V>
     */
    public class Node {

        /**
         * public member of Node class
         */
        public K key;
        public V value;
        public Node next;
        public int hash;

        /**
         * Node class Constructor
         * @param key
         * @param value
         * @param hash
         */
        public Node(K key, V value, int hash) {
            this.key = key;
            this.value = value;
            this.hash = hash;
        }
    }

    /**
     * private members of AssociativeArray class
     */
    private Node[] table;
    private int size;

    /**
     * AssociativeArray class Constructor
     */
    public AssociativeArray() {
        this(10);
    }

    /**
     * AssociativeArray class Constructor
     * @param size: size to initialize insternal array to
     */
    public AssociativeArray(int size) {
        this.table = new Node[size];
        this.size = 0;
    }

    /**
     * Adds or updates Key, Value pair into the Array
     * @param key: Key to associate vale with
     * @param value: value to store
     * @return Node: Node added or updated
     */
    public Node Set(K key, V value) {
		/* Find the hash of the key and bucket it belongs to */
        int hash = key.hashCode();
        int bucket = GetBucket(hash);
        Node entry;
        if(IsEmpty()){
            entry = new Node(key, value, hash);
            table[bucket] = entry;
            size++;
        }
        else {
            entry = table[bucket];
            while (entry.next != null) {
                if (entry.getHash() == hash && EqualTo(entry.key, key)) {
                    entry.value = value;
                    return entry;
                }
                entry = entry.next;
            }


            Node node = new Node(key, value, hash);
            entry.next = node;
            size++;
            entry = node;
        }

        return entry;
    }

    /**
     * Gets the value of the given key
     * @param key: Key to get value of
     * @return V: generic value type
     */
    public V Get(K key) {
        int hash = key.hashCode();
        int bucket = GetBucket(hash);

        Node entry = table[bucket];
        while (entry != null) {
			/* If hash and key matches, return the value */
            if (EqualTo(entry.hash, hash) && EqualTo(entry.key, key)) {
                return entry.value;
            }
            entry = entry.next;
        }
        return null;
    }

    /**
     * Gets the size of the array
     * @return int: number of elements in the array
     */
    public int Size() {
        return size;
    }

    /**
     * Checks if the array is empty of not
     * @return boolean: true|false
     */
    public boolean IsEmpty() {
        return size == 0;
    }

    /**
     * Gets the bucket container for the internal array
     * @param hash: hash to find bucket of
     * @return int: bucket index of the array
     */
    private int GetBucket(int hash) {
        return (hash % table.length);
    }

    /**
     * Determins if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: ture|false
     */
    private boolean LessThan(T a, T b) {
        return a.compareTo(b) < 0;
    }

    /**
     * Determins if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean EqualTo(T a, T b) {
        return a.compareTo(b) == 0;
    }

    /**
     * Determins if a is greater than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean GreaterThan(T a, T b) {
        return a.compareTo(b) > 0;
    }
}