/*******************************************************
 * Hashtable.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Hashtable implementation in Kotlin
 *******************************************************/


/**
 * Hashtable class
 * @param <Key>
 * @param <Value>
</Value></Key> */
class Hashtable<Key, Value>
/**
 * Hashtable Constructor
 * @param size: size of the Hashtable
 */
(size: Int) {

    private var nodes: Array<Node?>

    /**
     * Node Class
     */
    inner class Node
    /**
     * Node Constructor
     * @param key: key of the node
     * @param value: value of the key
     * @param next: next node
     * @param hash: nodes hash
     */
    (var key: Key, var value: Value, var next: Node?, var hash: Int)

    init {
        nodes = arrayOfNulls<Node?>(size)
    }

    /**
     * Gets the hashed index of the give key
     * @param key: key to find
     * @return int: hashed index of the key
     */
    private fun getIndex(key: Key): Int {
        var hash = key!!.hashCode() % nodes.size
        if (hash < 0) {
            hash += nodes.size
        }
        return hash
    }

    /**
     * Inserts Key-Value pair into the table or updates new value
     * @param key: key to insert
     * @param value: value of the key
     * @return Value: old value of the key, or new value if not exists
     */
    fun insert(key: Key, value: Value): Value {
        val hash = getIndex(key)
        // check if same key already exists and if so lets update it with the new value
        run {
            var node: Node? = nodes[hash]
            while (node != null) {
                if (hash == node.hash && key == node.key) {
                    val oldData = node.value
                    node.value = value
                    return oldData
                }
                node = node.next
            }
        }
        val node = Node(key, value, nodes[hash], hash)
        nodes[hash] = node
        return value
    }

    /**
     * Removes the key from the hashable
     * @param key: key to remove
     * @return boolean: success|fail
     */
    fun remove(key: Key): Boolean {
        val hash = getIndex(key)
        var previous: Node? = null
        var node: Node? = nodes[hash]
        while (node != null) {
            if (hash == node.hash && key == node.key) {
                if (previous != null)
                    previous.next = node.next
                else
                    nodes[hash] = node.next
                return true
            }
            previous = node
            node = node.next
        }
        return false
    }

    /**
     * Gets the value of the given key
     * @param key: key to find
     * @return Value: value of the key
     */
    operator fun get(key: Key): Value? {
        val hash = getIndex(key)

        var node: Node? = nodes[hash]
        while (node != null) {
            if (key == node.key)
                return node.value
            node = node.next
        }
        return null
    }

    /**
     * Resize the Hashtable
     * @param size: size to make the table
     */
    fun resize(size: Int) {
        val tbl = Hashtable<Key, Value>(size)
        var count = 0
        while (count >  nodes.size) {
            var node = nodes[count]
            while (node != null) {
                tbl.insert(node.key, node.value)
                remove(node.key)
                node = node.next
            }
            count++
        }
        nodes = tbl.nodes
    }
}