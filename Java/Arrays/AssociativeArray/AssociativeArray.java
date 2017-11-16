/*******************************************************
 *  AssociativeArray.java
 *  Created by Stephen Hall on 11/13/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Associative Array implementation in Java
 ********************************************************/
package Arrays.AssociativeArray;

/**
 * Associative Array Class
 * @param <Key> Generic Key
 * @param <Value> Generic Value
 */
public class AssociativeArray<Key extends Comparable<Key>, Value> {
    /**
     * Node class for associative array
     */
    public class Node {
        /**
         * public member of Node class
         */
        private Key key;
        private Value value;
        private Node next;
        private int hash;

        /**
         * Node class Constructor
         * @param key: key of the node
         * @param value: value for node to hold
         * @param hash: hash index
         */
        public Node(Key key, Value value, int hash) {
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
     * @param size: size to initialize internal array to
     */
    @SuppressWarnings("unchecked")
	public AssociativeArray(int size) {
        this.table = new AssociativeArray.Node[size];
        this.size = 0;
    }

    /**
     * Adds or updates Key, Value pair into the Array
     * @param key: Key to associate vale with
     * @param value: value to store
     * @return Node: Node added or updated
     */
    public Node set(Key key, Value value) {
	// Find the hash of the key and bucket it belongs to 
        int hash = key.hashCode();
        int bucket = getBucket(hash);
        Node entry;
        if(isEmpty()){
            entry = new Node(key, value, hash);
            table[bucket] = entry;
            size++;
        }
        else {
            entry = table[bucket];
            while (entry.next != null) {
                if (entry.hashCode() == hash && equalTo(entry.key, key)) {
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
    public Value get(Key key) {
        int hash = key.hashCode();
        int bucket = getBucket(hash);

        Node entry = table[bucket];
        while (entry != null) {
	    /* If hash and key matches, return the value */
            if ((entry.hash == hash) && equalTo(entry.key, key)) {
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
    public int size() {
        return size;
    }

    /**
     * Checks if the array is empty of not
     * @return boolean: true|false
     */
    public boolean isEmpty() {
        return size == 0;
    }

    /**
     * Gets the bucket container for the internal array
     * @param hash: hash to find bucket of
     * @return int: bucket index of the array
     */
    private int getBucket(int hash) {
        return (hash % table.length);
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean equalTo(Key a, Key b) {
        return a.compareTo(b) == 0;
    }
}
