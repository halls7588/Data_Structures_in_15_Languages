/*******************************************************
 * AssociativeArray.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Associative Array implementation in Kotlin
 ********************************************************/

/**
 * Associative Array Class
 * @param <Key> Generic Key
 * @param <Value> Generic Value
</Value></Key> */
class AssociativeArray<Key : Comparable<Key>, Value>
/**
 * AssociativeArray class Constructor
 * @param size: size to initialize internal array to
 */
(size: Int) {

    /**
     * private members of AssociativeArray class
     */
    private var table: Array<Node?>
    private var size: Int = 0

    /**
     * Checks if the array is empty of not
     * @return boolean: true|false
     */
    val isEmpty: Boolean
        get() = size == 0

    /**
     * Node class for associative array
     */
    inner class Node
    /**
     * Node class Constructor
     * @param key: key of the node
     * @param value: value for node to hold
     * @param hash: hash index
     */
    (
            /**
             * public member of Node class
             */
            var key: Key, var value: Value, var hash: Int) {
            var next: Node? = null

    }

    /**
     * AssociativeArray class Constructor
     */
    constructor() : this(10) {}

    init {
        this.table = arrayOfNulls<Node?>(size)
        this.size = 0
    }

    /**
     * Adds or updates Key, Value pair into the Array
     * @param key: Key to associate vale with
     * @param value: value to store
     * @return Node: Node added or updated
     */
    operator fun set(key: Key, value: Value): Node {
        // Find the hash of the key and bucket it belongs to
        var hash = key.hashCode()
        var bucket = getBucket(hash)
        var entry: Node?
        if (isEmpty) {
            entry = Node(key, value, hash)
            table[bucket] = entry
            size++
        } else {
            entry = table[bucket]
            while (entry!!.next != null) {
                if (entry.hashCode() == hash && equalTo(entry.key, key)) {
                    entry.value = value
                    return entry
                }
                entry = entry.next
            }

            val node = Node(key, value, hash)
            entry.next = node
            size++
            entry = node
        }
        return entry
    }

    /**
     * Gets the value of the given key
     * @param key: Key to get value of
     * @return V: generic value type
     */
    operator fun get(key: Key): Value? {
        val hash = key.hashCode()
        val bucket = getBucket(hash)

        var entry: Node? = table[bucket]
        while (entry != null) {
            /* If hash and key matches, return the value */
            if (entry.hash == hash && equalTo(entry.key, key)) {
                return entry.value
            }
            entry = entry.next
        }
        return null
    }

    /**
     * Gets the size of the array
     * @return int: number of elements in the array
     */
    fun size(): Int {
        return size
    }

    /**
     * Gets the bucket container for the internal array
     * @param hash: hash to find bucket of
     * @return int: bucket index of the array
     */
    private fun getBucket(hash: Int): Int {
        return hash % table.size
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun equalTo(a: Key, b: Key): Boolean {
        return a.compareTo(b) == 0
    }
}
